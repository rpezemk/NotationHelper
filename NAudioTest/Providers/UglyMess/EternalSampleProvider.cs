using NAudio.Wave;

namespace NAudioTest.Providers.UglyMess
{
    public class EternalSampleProvider : ISampleProvider
    {
        public List<ASignalSource> SignalsSources { get; set; } = new List<ASignalSource>();
        public WaveFormat WaveFormat { get; }
        public int SamplePrecision { get; set; } = 200;//SAMPLES
        public EternalSampleProvider()
        {
            WaveFormat = WaveFormat.CreateIeeeFloatWaveFormat(44100, 2);
        }
        public int Read(float[] buffer, int offset, int count)
        {
            var buff = new float[count];
            int samplesToCopy = count / 2;
            List<ASignalSource> toDetach = new List<ASignalSource>();

            var cnt = SignalsSources.Count();

            for (int srcNo = 0; srcNo < cnt; srcNo++)
            {
                var src = SignalsSources[srcNo];
                if (!src.TryGetSignal(samplesToCopy, 44100, 1, out var srcBuff))
                {
                    toDetach.Add(src);
                    continue;
                }
                for (int i = 0; i < samplesToCopy; i++)
                {
                    buff[i] += srcBuff[i];
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
