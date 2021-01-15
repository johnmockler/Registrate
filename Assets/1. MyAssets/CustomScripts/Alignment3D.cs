using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;


public class Alignment3D
{
    public struct Target
    {
        public Vector3 actualCoord;
        public Vector3 detectedCoord;

        public Target(Vector3 detectedCoord, Vector3 actualCoord)
        {
            this.actualCoord = actualCoord;
            this.detectedCoord = detectedCoord;
        }
    }

    private Vector3[] model_points;

    //GameObject[] placedTargets;
    Vector3[] placedTargets;
    Target[] targetList;
    Matrix4x4 tfModelToWorld;

    //check to see if this copies the game objects...if so maybe i should change it....but i think an array is just a reference to the first value right?
    public Alignment3D(Vector3[] placedTargets)
    {
        this.placedTargets = placedTargets;
        this.targetList = new Target[Constants.NUM_TARGETS];
        //this.targetList = new Target[Constants.NUM_TARGETS];
        //model_points = new Vector3[] { new Vector3(0, 0, 0), new Vector3(0.215f, 0, 0), new Vector3(0.215f, 0.279f, 0), new Vector3(0, 0.279f, 0) };
        model_points = new Vector3[] { new Vector3(1, 1, 0), new Vector3(-1, 1, 0), new Vector3(-1, -1, 0) , new Vector3(1, -1, 0) };

        //maybe get this matrix from constants?

        /*+90 degree rotation for left handed system
        this.targetList = new Target[] { new Target(new Vector3(0, 0, 0), new Vector3 (0, 0, 0)),
                       new Target(new Vector3(0, -1, 0), new Vector3(1, 0, 0)),
                       new Target(new Vector3(1, -1, 0), new Vector3(1, 1, 0)),
                       new Target(new Vector3(1, 0, 0), new Vector3(0, 1, 0))};
        
        */

        for (int i = 0; i < Constants.NUM_TARGETS; i++)
        {
            this.targetList[i] = new Target(placedTargets[i], model_points[i]);

            //this.targetList[i] = new Target(this.placedTargets[i].transform.position, model_points[i]);

        }
    }

    public (Matrix4x4, bool) computeRegistration()
    {
        //check if points are same length for robustness

        //Matrix4x4 tfMatrix = Matrix4x4.identity;
        bool successful = false;

        float[,] x_array, y_array;
        (x_array, y_array) = reformatTargetList(this.targetList);

        float[] X = Array2DToArray1D(x_array);
        float[] Y = Array2DToArray1D(y_array);
        int N = targetList.Length;
        float[] R = new float[9];
        float[] t = new float[3];

        float[,] W = new float[3,3];

        for(int i = 0; i < 3; i++)
        {
            for(int j = 0; j < 3; j++)
            {
                if (i==j)
                {
                    W[i, j] = 1.0f;
                }
                else
                {
                    W[i, j] = 0.0f;
                }
            }
        }

        float[] w_resized = Array2DToArray1D(W);
        
        float error1 = EigenWrapper.registerIsotropic(X, Y, N, R, t);

        float error = EigenWrapper.registerAnisotropic(X, Y, w_resized, N, 0.05f, R, t);

        float[,] rotMatrix = Array1DToArray2D(R, 3, 3);

        Matrix4x4 Transform = buildTfMatrix(rotMatrix, t);
        //this.tfModelToWorld = convertToMatrix4x4(T2D).transpose;
        Debug.Log("Non Iterative error is");
        Debug.Log(error1);
        Debug.Log("Iterative error is");
        Debug.Log(error);


        return (Transform, successful);
    }

    private static Matrix4x4 buildTfMatrix(float[,] R, float[] t)
    {
        float[,] Tf = new float[4, 4];
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                Tf[i, j] = R[i, j];
                //Debug.Log(R[i, j]);
            }
        }
        for (int i = 0; i < 3; i++)
        {
            Tf[i, 3] = t[i];
            //Debug.Log(t[i]);

        }
        Tf[3, 0] = 0.0f;
        Tf[3, 1] = 0.0f;
        Tf[3, 2] = 0.0f;
        Tf[3, 3] = 1.0f;

        return convertToMatrix4x4(Tf);

    }

    private static Matrix4x4 convertToMatrix4x4(float[,] A)
    {
        Matrix4x4 B = Matrix4x4.identity;
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                B[i, j] = A[i,j];
            }
        }

        return B;

    }

    private static (float[,], float[,]) reformatTargetList(Target[] targetList)
    {
        float[,] modelPoints = new float[3,targetList.Length];
        float[,] detectedPoints = new float[3,targetList.Length];

        for (int i = 0; i < targetList.Length; i++)
        {
            modelPoints[0,i] = targetList[i].actualCoord.x;
            modelPoints[1,i] = targetList[i].actualCoord.y;
            modelPoints[2,i] = targetList[i].actualCoord.z;

            detectedPoints[0,i] = targetList[i].detectedCoord.x;
            detectedPoints[1,i] = targetList[i].detectedCoord.y;
            detectedPoints[2,i] = targetList[i].detectedCoord.z;
        }

        return (modelPoints, detectedPoints);
    }

    //From Michael Brand - Conversions.cs
    public static float[,] Array1DToArray2D(float[] Array1D, int rowDim, int columnDim)
    {
        float[,] Array2D = new float[rowDim, columnDim];
        for (int column = 0; column < columnDim; column++)
        {
            for (int row = 0; row < rowDim; row++)
            {
                Array2D[row, column] = Array1D[column * columnDim + row];
            }
        }
        return Array2D;
    }

    private static float[] Array2DToArray1D(float[,] Array2D)
    {   // row wise //
        int noOfRows = Array2D.GetLength(0);
        int noOfColumns = Array2D.GetLength(1);
        int noOfEntries = noOfRows * noOfColumns;
        float[] Array1D = new float[noOfEntries];
        int i = 0;
        for (int column = 0; column < noOfColumns; column++) 
        {
            for (int row = 0; row < noOfRows; row++)
            {
                Array1D[i] = Array2D[row, column];
                i++;
            }
        }

        return Array1D;
    }

    //updates the transform of a game object
    private static void matrix4X4toTransform(Matrix4x4 m, ref GameObject gameObject)
    {

        //Transform tf; //= new Transform();
        Quaternion rotation;

        Vector3 fwd;
        fwd.x = m.m02;
        fwd.y = m.m12;
        fwd.z = m.m22;

        Vector3 up;
        up.x = m.m01;
        up.y = m.m11;
        up.z = m.m21;

        rotation = Quaternion.LookRotation(fwd, up);

        Vector3 position;

        position.x = m.m03;
        position.y = m.m13;
        position.z = m.m23;

        gameObject.transform.SetPositionAndRotation(position, rotation);

        return;

    }
}
