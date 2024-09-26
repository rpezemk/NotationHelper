using AudioTool.CSoundWrapper;
using AudioTool.Instruments;
using System.Runtime.Intrinsics.Arm;

class Program
{
    static void Main(string[] args)
    {
        CsEngine csEngine = new CsEngine([new BowedInstrument("viola")]);
        csEngine.RunAsync();

        Thread.Sleep(1000);
        Task.Run(() =>
        {
            var counter = 0;
            while (true)
            {
                while (true)
                {
                    Thread.Sleep(1);
                    csEngine.Enqueue(new SampleEvent(3, 0, 4, counter * 4));
                    counter++;
                    if (counter >= 12 * 2)
                        break;
                }
                Thread.Sleep(1000);
                counter = 0;
            }
        });

        Thread.Sleep(3600*1000);
    }
}
