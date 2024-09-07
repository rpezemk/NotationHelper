using Microsoft.VisualBasic.Logging;
using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NAudioTest.Providers
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

    public class SingleSampleProvider : ISampleProvider
    {
        private readonly float[] leftChannel1;
        private readonly float[] rightChannel1;
        private int position;

        public WaveFormat WaveFormat { get; }

        public SingleSampleProvider()
        {
            leftChannel1 = new float[100000];
            rightChannel1 = new float[100000];
            WaveFormat = WaveFormat.CreateIeeeFloatWaveFormat(44100, 2);
        }

        public int Read(float[] buffer, int offset, int count)
        {
            int samplesToCopy = Math.Min(count / 2, leftChannel1.Length - position);

            for (int i = 0; i < samplesToCopy; i++)
            {
                buffer[offset + i * 2] = leftChannel1[position + i];
                buffer[offset + i * 2 + 1] = rightChannel1[position + i];
            }

            position += samplesToCopy;

            return samplesToCopy * 2;
        }
    }

    public class EternalSampleProvider : ISampleProvider
    {
        public List<ISignalSource> SignalsSources { get; set; } = new List<ISignalSource>();
        public WaveFormat WaveFormat { get; }
        public EternalSampleProvider()
        {
            WaveFormat = WaveFormat.CreateIeeeFloatWaveFormat(44100, 2);
        }
        public int Read(float[] buffer, int offset, int count)
        {
            var buff = new float[count];
            int samplesToCopy = count / 2;
            List<ISignalSource> toDetach = new List<ISignalSource>();

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

            foreach(var  src in toDetach)
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

    public enum SourceStateEnum
    {
        New, Reading, Done
    }

    public abstract class ISignalSource
    {
        public abstract bool TryGetSignal(int count, int rate, int nchannels, out float[] res);
        private EternalSampleProvider eternalSampleProvider;
        public void AttachTo(EternalSampleProvider eternal)
        {
            eternal.SignalsSources.Add(this);
            eternalSampleProvider = eternal;
        }

        public void DetachFrom(EternalSampleProvider eternal)
        {
            eternalSampleProvider.SignalsSources.Remove(this);
            eternalSampleProvider = null;
        }

        public abstract SourceStateEnum SourceState { get; }
        public abstract ISignalSource Clone();
        public int SampleRate => GlobalData.SampleRate;
        public int NChannels => GlobalData.NChannels;
    }

    public class SinSource : ISignalSource
    {
        private double freq = 440;
        private float amplitude = 1.0F;
        private int currIdx = 0;
        private SourceStateEnum state = SourceStateEnum.New;
        private double position;
        public double duration;

        public SinSource(double fr, float amp, double _duration)
        {
            freq = fr;
            amplitude = amp;
            this.duration = _duration;
        }
        public override SourceStateEnum SourceState => state;
        public override ISignalSource Clone()
        {
            return new SinSource(freq, amplitude, duration);
        }

        public override bool TryGetSignal(int count, int rate, int nchannels, out float[] res)
        {
            state = SourceStateEnum.Reading;
            res = Enumerable.Range(currIdx, count).Select(i => amplitude * (float)Math.Sin((2 * Math.PI * freq * i) / (float)rate)).ToArray();
            currIdx += count;
            position += count / (double)rate;
            return position <= duration;
        }
    }

    public class AudioParameters
    {
        public AudioParameters(int rate,  int nChannels, int bitDepth)
        {
            SampleRate = rate;
            NChannels = nChannels;
            BitDepth = bitDepth;
        }
        public int NChannels;
        public int SampleRate;
        public int BitDepth;

        public override string ToString()
        {
            return $"{SampleRate} Hz, {NChannels} channels, {BitDepth} bits";
        }
    }

}
