using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class Eigen : MonoBehaviour
{
    [DllImport("EigenWrapper")]
    public static extern void calculateLeastSquares([MarshalAs(UnmanagedType.LPArray)] float[,] H,
                                                    [MarshalAs(UnmanagedType.LPArray)] float[,] X,
                                                      float det);


    void Start()
    {
        float[,] H = { { 1.0f, 0, 0 }, { 0, 1.0f, 0 }, { 0, 0, 1.0f } };
        float[,] X = new float[3,3];
        float det = 0;
        calculateLeastSquares(H, X, det);

        Debug.Log("The trace value is: " + det);
    }
}
