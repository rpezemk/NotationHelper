using NAudio.Wave;
using NAudio.Wave.SampleProviders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NAudioTest.Helpers
{
    public static class AudioFileHelper
    {
        public static float[][] LoadAudioFileToStereoArray(string filePath)
        {
            using var reader = new AudioFileReader(filePath);

            int sampleCount = (int)reader.Length / (reader.WaveFormat.BitsPerSample / 8);
            float[] leftChannel = new float[sampleCount / 2];
            float[] rightChannel = new float[sampleCount / 2];

            float[] buffer = new float[sampleCount];
            int samplesRead = reader.Read(buffer, 0, sampleCount);

            for (int i = 0; i < samplesRead; i += 2)
            {
                leftChannel[i / 2] = buffer[i];
                rightChannel[i / 2] = buffer[i + 1];
            }

            return [leftChannel, rightChannel];
        }

        public static void Test()
        {
            string inputFilePath = "yourfile.wav";
            string outputFilePath = "resampled.wav";
            int newSampleRate = 44100; // Set desired sample rate

            using (var reader = new AudioFileReader(inputFilePath))
            {
                // Resample to a new sample rate
                var resampler = new WdlResamplingSampleProvider(reader, newSampleRate);
                float[] buffer = new float[resampler.WaveFormat.SampleRate * reader.WaveFormat.Channels];
                int samplesRead = resampler.Read(buffer, 0, buffer.Length);
            
            }
        }
    }
}
