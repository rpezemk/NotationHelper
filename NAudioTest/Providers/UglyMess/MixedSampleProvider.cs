using NAudio.Wave;

namespace NAudioTest.Providers.UglyMess
{
    public class MixedSampleProvider : ISampleProvider
    {
        private readonly float[] leftChannel1;
        private readonly float[] rightChannel1;
        private readonly float[] leftChannel2;
        private readonly float[] rightChannel2;
        private int position;

        public WaveFormat WaveFormat { get; }

        public MixedSampleProvider(float[][] audio1, float[][] audio2)
        {
            leftChannel1 = audio1[0];
            rightChannel1 = audio1[1];
            leftChannel2 = audio2[0];
            rightChannel2 = audio2[1];

            int maxLength = Math.Max(leftChannel1.Length, leftChannel2.Length);
            WaveFormat = WaveFormat.CreateIeeeFloatWaveFormat(44100, 2);
        }

        public int Read(float[] buffer, int offset, int count)
        {
            int samplesToCopy = Math.Min(count / 2, leftChannel1.Length - position);
            samplesToCopy = Math.Min(samplesToCopy, leftChannel2.Length - position);

            for (int i = 0; i < samplesToCopy; i++)
            {
                buffer[offset + i * 2] = leftChannel1[position + i] + leftChannel2[position + i];
                buffer[offset + i * 2 + 1] = rightChannel1[position + i] + rightChannel2[position + i];
            }

            position += samplesToCopy;

            return samplesToCopy * 2;
        }
    }

}
