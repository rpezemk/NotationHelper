using NAudio.Wave;

namespace NAudioTest.Providers
{
    // Custom sample provider for sequencing notes
    public class SequenceSampleProvider : ISampleProvider
    {
        private readonly double[] frequencies;
        private readonly double duration;
        private int currentIndex;
        private double currentSample;

        public WaveFormat WaveFormat { get; } = WaveFormat.CreateIeeeFloatWaveFormat(44100, 1);

        public SequenceSampleProvider(double[] frequencies, double duration)
        {
            this.frequencies = frequencies;
            this.duration = duration;
            currentIndex = 0;
            currentSample = 0;
        }

        public int Read(float[] buffer, int offset, int count)
        {
            int samplesRead = 0;
            for (int i = 0; i < count; i++)
            {
                if (currentSample >= duration * 44100)
                {
                    currentSample = 0;
                    currentIndex = (currentIndex + 1) % frequencies.Length;
                }
                buffer[offset + i] = (float)Math.Sin(2 * Math.PI * frequencies[currentIndex] * currentSample / 44100.0);
                currentSample++;
                samplesRead++;
            }
            return samplesRead;
        }
    }


}