using System.Runtime.InteropServices;


public class EigenWrapper
{
    [DllImport("EigenWrapper", CallingConvention = CallingConvention.Cdecl)]
    public static extern void calculateLeastSquares([MarshalAs(UnmanagedType.LPArray)] float[] H, [MarshalAs(UnmanagedType.LPArray)] float[] X);
    
    [DllImport("EigenWrapper", CallingConvention = CallingConvention.Cdecl)]
    public static extern void calculateTransform([MarshalAs(UnmanagedType.LPArray)] float[] model_centroid, [MarshalAs(UnmanagedType.LPArray)] float[] detected_centroid, 
                                                    [MarshalAs(UnmanagedType.LPArray)] float[] R, [MarshalAs(UnmanagedType.LPArray)] float[] T);
}