using System.Runtime.InteropServices;


public class EigenWrapper
{
    const string importName = "EigenAlignmentd";

    [DllImport(importName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void registerIsotropic([MarshalAs(UnmanagedType.LPArray, SizeConst = Constants.NUM_TARGETS * 3 )] float[] X, [MarshalAs(UnmanagedType.LPArray, SizeConst = Constants.NUM_TARGETS * 3)] float[] Y, int N, 
        [MarshalAs(UnmanagedType.LPArray, SizeConst = 9)] float[] outR, [MarshalAs(UnmanagedType.LPArray, SizeConst = 3)] float[] outT);

    [DllImport(importName, CallingConvention = CallingConvention.Cdecl)]
    public static extern float registerAnisotropic([MarshalAs(UnmanagedType.LPArray, SizeConst = Constants.NUM_TARGETS * 3)] float[] X, [MarshalAs(UnmanagedType.LPArray, SizeConst = Constants.NUM_TARGETS * 3)] float[] Y, 
        [MarshalAs(UnmanagedType.LPArray, SizeConst = 9)] float[] W, int N, float threshold, [MarshalAs(UnmanagedType.LPArray, SizeConst = 9)] float[] outR, [MarshalAs(UnmanagedType.LPArray, SizeConst = 3)] float[] outT);

}