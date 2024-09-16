namespace NAudioTest.Providers.UglyMess
{
    public abstract class ASignalSource
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
        public abstract ASignalSource Clone();
        public int SampleRate => GlobalData.SampleRate;
        public int NChannels => GlobalData.NChannels;
    }

}
