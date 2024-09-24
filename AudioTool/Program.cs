using System;
using System.Runtime.InteropServices;
using System.Threading;

class Program
{
    // Load the Csound shared library (csound64.dll)
    [DllImport("csound64.dll", CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr csoundCreate(IntPtr hostData);

    [DllImport("csound64.dll", CallingConvention = CallingConvention.Cdecl)]
    public static extern int csoundSetOption(IntPtr csound, string option);

    [DllImport("csound64.dll", CallingConvention = CallingConvention.Cdecl)]
    public static extern int csoundCompileCsd(IntPtr csound, string csdFile);

    [DllImport("csound64.dll", CallingConvention = CallingConvention.Cdecl)]
    public static extern int csoundStart(IntPtr csound);

    [DllImport("csound64.dll", CallingConvention = CallingConvention.Cdecl)]
    public static extern int csoundPerformKsmps(IntPtr csound);

    [DllImport("csound64.dll", CallingConvention = CallingConvention.Cdecl)]
    public static extern int csoundSetControlChannel(IntPtr csound, string channelName, double value);

    [DllImport("csound64.dll", CallingConvention = CallingConvention.Cdecl)]
    public static extern void csoundStop(IntPtr csound);

    [DllImport("csound64.dll", CallingConvention = CallingConvention.Cdecl)]
    public static extern void csoundDestroy(IntPtr csound);

    static void Main(string[] args)
    {
        // Create an instance of Csound
        IntPtr csound = csoundCreate(IntPtr.Zero);

        // Set the -odac option to output audio to speakers
        csoundSetOption(csound, "-odac");

        // Compile the csd file
        string csdFile = "example.csd";
        if (csoundCompileCsd(csound, csdFile) == 0)
        {
            // Start the Csound performance
            csoundStart(csound);

            // Create a thread to continuously update the "amplitude" control channel
            new Thread(() =>
            {
                double amplitude = 0.1;
                bool increasing = true;

                while (true)
                {
                    // Update the "amplitude" control channel
                    csoundSetControlChannel(csound, "amplitude", amplitude);

                    // Example logic: Slowly increase and decrease amplitude between 0.1 and 1.0
                    if (increasing)
                    {
                        amplitude += 0.11;
                        if (amplitude >= 1.0)
                            increasing = false;
                    }
                    else
                    {
                        amplitude -= 0.11;
                        if (amplitude <= 0.1)
                            increasing = true;
                    }

                    // Wait for 50ms before updating again (control rate ~20Hz)
                    Thread.Sleep(50);
                }
            }).Start();

            // Perform Csound in blocks until finished
            while (csoundPerformKsmps(csound) == 0) ;

            // Stop and clean up after the performance is complete
            csoundStop(csound);
        }

        // Clean up Csound resources
        csoundDestroy(csound);

        Console.WriteLine("Csound performance complete.");
    }
}
