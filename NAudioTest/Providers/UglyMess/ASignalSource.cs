namespace NAudioTest.Providers.UglyMess
{
    public abstract class ASignalSource
    {
        public Guid Guid;
        public abstract bool TryGetSignal(int count, int rate, int nchannels, out float[] res);
        public void AttachTo(EternalSampleProvider eternal)
        {
            eternal.SignalsSources.Add(this);
        }

        public void DetachFrom(EternalSampleProvider eternal)
        {
            if(eternal.SignalsSources.Contains(this))
                eternal.SignalsSources.Remove(this);
        }

        public abstract SourceStateEnum SourceState { get; }
        public abstract ASignalSource Clone();
        public int SampleRate => GlobalData.SampleRate;
        public int NChannels => GlobalData.NChannels;
    }

}
