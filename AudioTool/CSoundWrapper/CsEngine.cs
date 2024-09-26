using AudioTool.CSoundWrapper;
using System.Collections.Concurrent;
using System.Diagnostics;

public class CsEngine
{
    public bool isCancelled;
    public bool IsCancelled => isCancelled;
    IntPtr csound = 0;
    ConcurrentQueue<ICsEvent> eventList = new ConcurrentQueue<ICsEvent>();

    public void Enqueue(ICsEvent param)
    {
        eventList.Enqueue(param);
    }

    public void Cancel() { isCancelled = true; }

    /// <summary>
    /// RUN IN ASYNCHRONOUS MODE
    /// </summary>
    public void RunAsync() => Task.Run(() => Run());

    /// <summary>
    /// SYNCHRONOUS
    /// </summary>
    public void Run()
    {
        var startTime = DateTime.Now;
        Task.Run(() =>
        {
            csound = Wrapped.csoundCreate(IntPtr.Zero);
            Wrapped.csoundSetOption(csound, "-odac"); // Output to audio device

            var script = CsdGenerator.GetSimpleProgram();
            Console.WriteLine(script);
            // Compile the CSD string
            if (Wrapped.csoundCompileCsdText(csound, script) == 0)
            {
                Wrapped.csoundStart(csound);
                Wrapped.csoundSetControlChannel(csound, "amplitude", 1);
                while (true)
                {
                    Wrapped.csoundPerformKsmps(csound);
                    List<double[]> toRemove = new List<double[]>();
                    while (eventList.TryDequeue(out var e))
                    {
                        Wrapped.csoundScoreEvent(csound, 'i', e.GetParams(), e.GetParams().Length);
                    }
                    if (isCancelled)
                        break;
                }
                Wrapped.csoundStop(csound);
            }
            Wrapped.csoundDestroy(csound);
            Console.WriteLine("Csound performance complete.");
        });

        Thread.Sleep(100000000);
    }
}
