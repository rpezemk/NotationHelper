using AudioTool.CsEvents;
using AudioTool.Instruments;
class Program
{
    static void Main(string[] args)
    {
        var noise = new NoiseInstrument("NOISE");
        var flatViola = new FlatSampleInstrument("VIOLA_01");
        var layeredViola = new LayerSampleInstrument("VIOLA_01", InstrumentFileHelper.ViolaLayers);
        CsEngine csEngine = new CsEngine([noise, flatViola, layeredViola]);
        csEngine.RunAsync();
        var pitches = new List<int>() { 0, 7, 12, 16 };
        Thread.Sleep(1000);
        var duration = 4;
        Task.Run(() =>
        {
            while (true)
            {
                foreach(var p in pitches)
                {
                    csEngine.Play(layeredViola.EmitFromInstr(new LayerSampleEvent(0, duration, p * 4, 1, 0, 4)));
                    Thread.Sleep(duration * 1000);
                }
            }
        });

        //var dynamicsDelay = 10;//s
        //var dynamics = 1;
        //Task.Run(() =>
        //{
        //    while (true)
        //    {
        //        foreach (var p in pitches)
        //        {
        //            //csEngine.Play(flatViola.EmitFromInstr(new FlatSampleEvent(0, 4, p * 4)));
        //            csEngine.Play(layeredViola.EmitFromInstr(new LayerSampleEvent(0, dynamicsDelay, dynamics, 0, 0)).AsModulator());
        //            Thread.Sleep(dynamicsDelay * 1000);
        //            csEngine.Play(layeredViola.EmitFromInstr(new LayerSampleEvent(0, dynamicsDelay, dynamics, 0, 0)).AsModulator());
        //            Thread.Sleep(dynamicsDelay * 1000);
        //        }
        //    }
        //});


        Thread.Sleep(3600 * 1000);
    }
}
