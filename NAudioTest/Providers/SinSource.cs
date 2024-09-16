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

        public SinSource(double fr, float amp, double _duration)
        {
            freq = fr;
            amplitude = amp;
            this.duration = _duration;
        }
        public override SourceStateEnum SourceState => state;
        public override ASignalSource Clone()
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

}
