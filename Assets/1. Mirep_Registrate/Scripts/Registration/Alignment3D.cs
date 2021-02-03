using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;


public class Alignment3D
{
    private Vector3[] model_points;
    private Vector3[] placedTargets;
    Matrix4x4 tfModelToWorld;

    public Alignment3D(Vector3[] placedTargets)
    {
        this.placedTargets = placedTargets;
        //model_points = new Vector3[] { new Vector3(0, 0, 0), new Vector3(0.215f, 0, 0), new Vector3(0.215f, 0.279f, 0), new Vector3(0, 0.279f, 0) };
        model_points = new Vector3[] { new Vector3(1, 1, 0), new Vector3(-1, 1, 0), new Vector3(-1, -1, 0) , new Vector3(1, -1, 0) };

    }

    public (Matrix4x4, bool) computeRegistration()
    {
        bool successful = false;

        float[] X = VectorToArray1D(this.model_points);
        float[] Y = VectorToArray1D(this.placedTargets);

        int N = Constants.NUM_TARGETS;
        float[] R = new float[9];
        float[] t = new float[3];

        //float[,] w_array = new float[3,3];
        //float[] W = new float[9];// Array2DToArray1D(W);

        float error = EigenWrapper.registerIsotropic(X, Y, N, R, t);

        float[,] rotMatrix = Array1DToArray2D(R, 3, 3);

        Matrix4x4 Transform = buildTfMatrix(rotMatrix, t);

        Debug.Log("error is");
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
            }
        }

        for (int i = 0; i < 3; i++)
        {
            Tf[i, 3] = t[i];
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
    private static float[] VectorToArray1D(Vector3[] A)
    {
        int noOfColumns = A.Length;
        float[] Array1D = new float[noOfColumns * 3];
        int i = 0;

        for (int column = 0; column < noOfColumns; column++)
        {
            Array1D[i] = A[column].x;
            Array1D[i + 1] = A[column].y;
            Array1D[i + 2] = A[column].z;

            i += 3;
        }

        return Array1D;

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
