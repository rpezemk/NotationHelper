using NWaves.Audio;
using NWaves.Signals;

namespace AudioTool
{
    public static class AudioFileHelper
    {
        public static void SaveAudio(float[] leftData, float[] rightData, int sampleRate, string filePath)
        {
            if(File.Exists(filePath))
                File.Delete(filePath);
            var leftSingal = new DiscreteSignal(sampleRate, leftData);
            var rightSingal = new DiscreteSignal(sampleRate, rightData);
            var sampleCount = rightData.Length;
            var wavFile = new WaveFile(new List<DiscreteSignal>() { leftSingal, rightSingal });
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                wavFile.SaveTo(fileStream);
            }
        }
    }
}
