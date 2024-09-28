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
        var pitches = new List<int>() { 0, };
        Thread.Sleep(1000);
        Task.Run(() =>
        {
            while (true)
            {
                foreach (var p in pitches)
                {
                    layeredViola.PlaySeparatedNote(p, 8);
                    Thread.Sleep(12 * 1000);
                }
            }
        });

        var dynamicsDelay = 2;//s
        var dynamics = 1;
        //Task.Run(() =>
        //{
        //    while (true)
        //    {
        //        //csEngine.Play(flatViola.EmitFromInstr(new FlatSampleEvent(0, 4, p * 4)));
        //        layeredViola.ApplyDynamics(dynamicsDelay, dynamics);
        //        Thread.Sleep(dynamicsDelay * 1000);
        //        dynamics = 1;
        //        layeredViola.ApplyDynamics(dynamicsDelay, dynamics);
        //        Thread.Sleep(dynamicsDelay * 1000);
        //        dynamics = 0;
        //    }
        //});


        Thread.Sleep(3600 * 1000);
    }
}
