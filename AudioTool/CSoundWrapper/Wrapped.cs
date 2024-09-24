using System.Runtime.InteropServices;

public static class Wrapped
{
    // Load the Csound shared library (csound64.dll)
    [DllImport("C:\\Program Files\\Csound6_x64\\bin\\csound64.dll", CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr csoundCreate(IntPtr hostData);

    [DllImport("C:\\Program Files\\Csound6_x64\\bin\\csound64.dll", CallingConvention = CallingConvention.Cdecl)]
    public static extern int csoundSetOption(IntPtr csound, string option);

    [DllImport("C:\\Program Files\\Csound6_x64\\bin\\csound64.dll", CallingConvention = CallingConvention.Cdecl)]
    public static extern int csoundCompileCsd(IntPtr csound, string csdFile);

    [DllImport("C:\\Program Files\\Csound6_x64\\bin\\csound64.dll", CallingConvention = CallingConvention.Cdecl)]
    public static extern int csoundStart(IntPtr csound);

    [DllImport("C:\\Program Files\\Csound6_x64\\bin\\csound64.dll", CallingConvention = CallingConvention.Cdecl)]
    public static extern int csoundPerformKsmps(IntPtr csound);

    [DllImport("C:\\Program Files\\Csound6_x64\\bin\\csound64.dll", CallingConvention = CallingConvention.Cdecl)]
    public static extern int csoundSetControlChannel(IntPtr csound, string channelName, double value);

    [DllImport("C:\\Program Files\\Csound6_x64\\bin\\csound64.dll", CallingConvention = CallingConvention.Cdecl)]
    public static extern void csoundStop(IntPtr csound);

    [DllImport("C:\\Program Files\\Csound6_x64\\bin\\csound64.dll", CallingConvention = CallingConvention.Cdecl)]
    public static extern void csoundDestroy(IntPtr csound);

    [DllImport("C:\\Program Files\\Csound6_x64\\bin\\csound64.dll", CallingConvention = CallingConvention.Cdecl)]
    public static extern int csoundCompileCsdText(IntPtr csound, string csdText);

    [DllImport("C:\\Program Files\\Csound6_x64\\bin\\csound64.dll", CallingConvention = CallingConvention.Cdecl)]
    public static extern int csoundSetChannel(IntPtr csound, string channelName, double value);

    [DllImport("csound64.dll", CallingConvention = CallingConvention.Cdecl)]
    public static extern int csoundScoreEvent(IntPtr csound, char eventType, double[] pFields, int numFields);

}
