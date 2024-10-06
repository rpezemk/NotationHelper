using AudioTool.CsEvents;
using AudioTool.CSoundWrapper;
using AudioTool.InstrumentBase;
using System.Collections.Concurrent;
using System.Diagnostics;

public class CsEngine
{
    public bool isCancelled;
    public bool IsCancelled => isCancelled;
    IntPtr csound = 0;
    ConcurrentQueue<ACsEvent> eventList = new ConcurrentQueue<ACsEvent>();
    public List<AScriptedInstrument> ScriptInstruments = new List<AScriptedInstrument>();
    public CsEngine(List<AScriptedInstrument> instruments)
    {
        foreach(var instr in instruments)
        {
            instr.Engine = this;
        }
        ScriptInstruments = instruments;
    }

    internal void Play(ACsEvent csEvent)
    {
        var pars = csEvent.GetParams();
        eventList.Enqueue(csEvent);
    }

    public void Play(string instrumentName, ACsEvent csEvent)
    {
        var instr = ScriptInstruments.FirstOrDefault(si => si.Name == instrumentName);
        if (instr == null)
            return;
        var pars = csEvent.GetParams();
        csEvent.SetInstrNo(instr.InstrNo);
        eventList.Enqueue(instr.EmitFromInstr(csEvent));
    }

    public void Enqueue(ACsEvent param)
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
            Wrapped.csoundSetOption(csound, "-odac -b512 -B4096");
            var script = CsdGenerator.GetScriptedInstruments(ScriptInstruments);
            Console.WriteLine(script);
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
                        var pars = e.GetParams();
                        Wrapped.csoundScoreEvent(csound, 'i', e.GetParams(), e.GetParams().Length);
                    }
                    if (isCancelled)
                        break;
                }
                Wrapped.csoundStop(csound);
            }
            Wrapped.csoundDestroy(csound);
        });

        Thread.Sleep(100000000);
    }


}
