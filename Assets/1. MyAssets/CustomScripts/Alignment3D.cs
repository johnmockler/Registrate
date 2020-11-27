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

    GameObject[] placedTargets;
    Target[] targetList;
    Matrix4x4 tfModelToWorld;

    //check to see if this copies the game objects...if so maybe i should change it....but i think an array is just a reference to the first value right?
    public Alignment3D(GameObject[] placedTargets)
    {
        this.placedTargets = placedTargets;
        this.targetList = new Target[Constants.NUM_TARGETS];
        model_points = new Vector3[] { new Vector3(0, 0, 0), new Vector3(0.215f, 0, 0), new Vector3(0.215f, 0.279f, 0), new Vector3(0, 0.279f, 0) };
        //maybe get this matrix from constants?
        /*
        this.targetList = new Target[] { new Target(new Vector3(0, 0, 0), new Vector3 (0, 0, 0)),
                       new Target(new Vector3(0, -1, 0), new Vector3(0.215f, 0, 0)),
                       new Target(new Vector3(1, -1, 0), new Vector3(0.215f, 0.279f, 0)),
                       new Target(new Vector3(1, 0, 0), new Vector3(0, 0.279f, 0))};
        */

        for (int i = 0; i < Constants.NUM_TARGETS; i++)
        {
            this.targetList[i] = new Target(this.placedTargets[i].transform.position, model_points[i]);

        }
    }

    public (Matrix4x4, bool) computeRegistration()
    {
        //check if points are same length for robustness

        //Matrix4x4 tfMatrix = Matrix4x4.identity;
        Vector3[] model_points, detected_points;
        (model_points, detected_points) = reformatArray(this.targetList);
        bool successful = false;

        //uses method “Least-squares fitting of two 3 - D point sets" by arun
        float[] p1 = findCentroid(model_points);
        float[] p2 = findCentroid(detected_points);

        Vector3[] q1 = shiftCentroid(model_points, p1);
        Vector3[] q2 = shiftCentroid(detected_points, p2);

        float[,] H2D = calculateH(q1, q2);
        float[] H = Array2DToArray1D(H2D);
        float[] X = new float[9];
        EigenWrapper.calculateLeastSquares(H,X);
        //float[,] X2D = Array1DToArray2D(X, 3, 3);
        /*
        float[] X = new float[9];
        float[,] X2F = new float[3, 3];
        //float det;
        IntPtr result;

        unsafe
        {
            fixed (float* hPtr = H)
            {
                IntPtr  hIntPtr = new IntPtr(hPtr);
                result = EigenWrapper.calculateLeastSquares(hIntPtr);
            }
        }
        Marshal.Copy(result, X, 0, 9);
        System.Buffer.BlockCopy(X, 0, X2F, 0, sizeof(float) * 3 * 3);
        Marshal.DestroyStructure<IntPtr>(result);
        */


        //UnityEngine.Debug.Log(X.ToString());
        //Calculate Det Here to check if success.

        float[] T = new float[16];
        EigenWrapper.calculateTransform(p1, p2, X, T);
        float[,] T2D = Array1DToArray2D(T, 4, 4);
        this.tfModelToWorld = convertToMatrix4x4(T2D).transpose;
        //this.tfModelToWorld = matrix4X4toTransform(tfMatrix);
        //if (det > 0)
        //{

        /*
        IntPtr result1;
            float[] Tf = new float[16];
            float[,] Tf2F = new float[4, 4];
            unsafe
            {
                fixed(float* p1Ptr = p1, p2Ptr = p2, xPtr = X2F)
                {
                    IntPtr p1IntPtr = new IntPtr(p1Ptr);
                    IntPtr p2IntPtr = new IntPtr(p2Ptr);
                    IntPtr xIntPtr = new IntPtr(xPtr);


                    result1 = EigenWrapper.calculateTransform(p1IntPtr, p2IntPtr, xIntPtr);
                }
            }
            Marshal.Copy(result1, Tf, 0, 16);
            System.Buffer.BlockCopy(Tf, 0, Tf2F, 0, sizeof(float) * 4 * 4);
            Marshal.DestroyStructure<IntPtr>(result1);
            modelTransform = convertToMatrix4x4(Tf2F).transpose;

            UnityEngine.Debug.Log(modelTransform.ToString()) ;
            successful = true;

        //}
        //else
        //{
        //    modelTransform = Matrix4x4.identity;
        //    successful = false;
        //}

       




        //modelTransform = Matrix4x4.identity;
        //Debug.Log(det);
        /*
        //fails if det = -1, success if det = 1;
        if (det > 0)
        {
            float[,] Tf = new float[4, 4];
            EigenWrapper.calculateTransform(p1, p2, X, Tf);
            modelTransform = convertToMatrix4x4(Tf);
            successful = true;
        }
        else
        {
            modelTransform = Matrix4x4.identity;
            successful = false;
        }

        //Check whether this is right or left handed coordinate system.

    */
        return (tfModelToWorld, successful);
    }

    private static float[] findCentroid(Vector3[] point_set)
    {
        Vector3 centroid = new Vector3(0.0f, 0.0f, 0.0f);
        int num_points = point_set.Length;

        for (int i = 0; i < num_points; i++)
        {
            centroid.x += point_set[i].x;
            centroid.y += point_set[i].y;
            centroid.z += point_set[i].z;
        }
        centroid = centroid / num_points;

        float[] centroidF = new float[3];
        centroidF[0] = centroid.x;
        centroidF[1] = centroid.y;
        centroidF[2] = centroid.z;

        return centroidF;
    }

    private static Vector3[] shiftCentroid(Vector3[] point_set, float[] centroid)
    {
        int num_points = point_set.Length;

        Vector3[] shifted_set = new Vector3[num_points];

        for (int i = 0; i < num_points; i++)
        {
            shifted_set[i] = new Vector3(0.0f, 0.0f, 0.0f);
            shifted_set[i].x = point_set[i].x - centroid[0];
            shifted_set[i].y = point_set[i].y - centroid[1];
            shifted_set[i].z = point_set[i].z - centroid[2];
        }

        return shifted_set;
    }

    private static float[,] calculateH(Vector3[] q1, Vector3[] q2)
    {
        float[,] H = { { 0, 0, 0 }, { 0, 0, 0 }, { 0, 0, 0 } };

        for (int i = 0; i < q1.Length; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                for (int k = 0; k < 3; k++)
                {
                    //calculate weighted average on the fly
                    H[j, k] = H[j, k] + (q1[i][j] * q2[i][k] - H[j, k]) / (i + 1);
                }
            }
        }

        return H;
    }

    private static Matrix4x4 convertToMatrix4x4(float[,] A)
    {
        Matrix4x4 B = Matrix4x4.identity;
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                B[i, j] = A[i, j];
            }
        }

        return B;

    }

    private static (Vector3[], Vector3[]) reformatArray(Target[] targetList)
    {
        Vector3[] modelPoints = new Vector3[targetList.Length];
        Vector3[] detectedPoints = new Vector3[targetList.Length];

        for (int i = 0; i < targetList.Length; i++)
        {
            modelPoints[i] = targetList[i].actualCoord;
            detectedPoints[i] = targetList[i].detectedCoord;
        }

        return (modelPoints, detectedPoints);
    }

    //From Michael Brand - Conversions.cs
    public static float[,] Array1DToArray2D(float[] Array1D, int rowDim, int columnDim)
    {
        float[,] Array2D = new float[rowDim, columnDim];
        for (int i = 0; i < rowDim; i++)
        {
            for (int j = 0; j < columnDim; j++)
            {
                Array2D[i, j] = Array1D[i * rowDim + j];
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
        for (int row = 0; row < noOfRows; row++)
        {
            for (int column = 0; column < noOfColumns; column++)
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
