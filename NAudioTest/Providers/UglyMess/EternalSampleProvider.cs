using NAudio.Wave;
using Serilog;

namespace NAudioTest.Providers.UglyMess
{
    public class EternalSampleProvider : ISampleProvider
    {
        public List<ASignalSource> SignalsSources { get; set; } = new List<ASignalSource>();
        public WaveFormat WaveFormat { get; }
        public int LittleSamples { get; set; } = 10;//SAMPLES
        public EternalSampleProvider()
        {
            WaveFormat = WaveFormat.CreateIeeeFloatWaveFormat(44100, 2);
        }
        public int Read(float[] buffer, int offset, int count)
        {
            var buff = new float[count];
            int samplesToCopy = count / 2;
            List<ASignalSource> toDetach = new List<ASignalSource>();


            var nLoops = samplesToCopy / LittleSamples;
            var lasting = samplesToCopy % LittleSamples;
            bool playing = false;
            bool playingPrev = false;

            for (int j = 0; j < nLoops; j++)
            {
                var subSrc = SignalsSources.ToList();
                foreach (var src in subSrc)
                {
                    if (src.TryGetSignal(LittleSamples, 44100, 1, out var srcBuff))
                    {

                        playing = true;
                        ////Log.Information($"got signal {DateTime.Now.Millisecond}");
                        srcBuff.Select((v, i) => buff[i + j * LittleSamples] + srcBuff[i]).ToArray().CopyTo(buff, j * LittleSamples);
                        if (playing && !playingPrev)
                            //Log.Information("started");
                        playingPrev = playing;
                        playing = false;
                    }
                }
            }
            

            var nDone = LittleSamples * nLoops;
            var subSrc2 = SignalsSources.ToList();
            foreach (var src in subSrc2)
            {
                if (src.TryGetSignal(samplesToCopy, 44100, 1, out var srcBuff))
                {
                    for (int i = 0; i < lasting; i++)
                    {
                        buff[i + nDone] += srcBuff[i];
                    }
                }
            }

            foreach (var src in toDetach)
            {
                src.DetachFrom(this);
            }



            for (int i = 0; i < samplesToCopy; i++)
            {
                buffer[offset + i * 2] = buff[i];
                buffer[offset + i * 2 + 1] = buff[i];
            }

            return count;
        }

    }

}
