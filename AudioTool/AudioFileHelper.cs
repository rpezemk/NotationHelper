using NWaves.Audio;
using NWaves.Signals;
using NAudio.Wave;

namespace AudioTool
{
    public class StereoWave
    {
        public float[] Left { get; set; } = [];
        public float[] Right { get; set; } = [];
        public int Len => Left.Length;
        public StereoWave(float[] left, float[] right)
        {
            this.Left = left;
            this.Right = right;
        }

        public StereoWave(int len)
        {
            Left = new float[len];
            Right = new float[len];
        }
    }
    public static class AudioFileHelper
    {
        public static bool TryLoadStereoAudio(string filePath, out StereoWave stereoWave)
        {
            stereoWave = null;
            using (AudioFileReader audioFileReader = new AudioFileReader(filePath))
            {
                if (audioFileReader.WaveFormat.Channels == 2)
                {
                    float[] stereoSamples = new float[audioFileReader.Length / sizeof(float)];

                    int numSamples = audioFileReader.Read(stereoSamples, 0, stereoSamples.Length);
                    var leftChannel = new float[numSamples / 2];
                    var rightChannel = new float[numSamples / 2];
                    for (int i = 0, j = 0; i < numSamples; i += 2, j++)
                    {
                        leftChannel[j] = stereoSamples[i];     
                        rightChannel[j] = stereoSamples[i + 1];
                    }

                    stereoWave = new StereoWave(leftChannel, rightChannel);
                    return true;
                }
            }
            return false;   
        }

        public static void SaveAudio(float[] leftData, float[] rightData, int sampleRate, string filePath)
        {
            if(File.Exists(filePath))
                File.Delete(filePath);
            var leftSingal = new DiscreteSignal(sampleRate, leftData);
            var rightSingal = new DiscreteSignal(sampleRate, rightData);
            var wavFile = new WaveFile(new List<DiscreteSignal>() { leftSingal, rightSingal });
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                wavFile.SaveTo(fileStream);
            }
        }
    }
}
