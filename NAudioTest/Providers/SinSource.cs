using NAudioTest.Providers.UglyMess;

namespace NAudioTest.Providers
{
    public class SinSource : ASignalSource
    {
        private double freq = 440;
        private float amplitude = 1.0F;
        private int currIdx = 0;
        private SourceStateEnum state = SourceStateEnum.New;
        private double position;
        public double duration;
        public SinSource(double fr, float amp, double _duration, Guid guid)
        {
            freq = fr;
            amplitude = amp;
            duration = _duration;
            Guid = guid;
        }
        public override SourceStateEnum SourceState => state;
        public override ASignalSource Clone()
        {
            return new SinSource(freq, amplitude, duration, Guid);
        }

        public override bool TryGetSignal(int count, int rate, int nchannels, out float[] res)
        {
            state = SourceStateEnum.Reading;
            position += count / (double)rate;
            var thisRead = count / (double)rate;
            if(position <= duration)
            {
                res = Enumerable.Range(currIdx, count).Select(i => amplitude * (float)Math.Sin((2 * Math.PI * freq * i) / (float)rate)).ToArray();
                currIdx += count;
                return true;
            }

            else if (position <= duration + thisRead)
            {
                var last = (int)(duration * rate - currIdx);
                res = Enumerable.Range(currIdx, count).Select((sNo, i)=> i < last? amplitude * (float)Math.Sin((2 * Math.PI * freq * sNo) / (float)rate) : 0).ToArray();
                return true;
            }

            else 
            { 
                res = new float[count];
                return false; 
            }

        }
    }

}
