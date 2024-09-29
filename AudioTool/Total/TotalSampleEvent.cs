using AudioTool.CsEvents;

namespace AudioTool.Total
{
    public class TotalSampleEvent : ACsEvent
    {
        public TotalSampleEvent(double start, double noteDuration, double startPitchOffset, double sampleLen, double legato)
        {
            Start = start;
            NoteDuration = noteDuration;
            PitchOffset = startPitchOffset;
            SampleLen = sampleLen;
            Legato = legato;
        }
        public double Start { get; set; }
        public double NoteDuration { get; set; }
        public double PitchOffset { get; set; }
        public double SampleLen { get; set; }
        public double Legato {  get; set; }
        public TotalSampleEvent AsModulator()
        {
            TotalSampleEvent cloned = new TotalSampleEvent(0, NoteDuration, PitchOffset, SampleLen, Legato);
            cloned.InstrumentNo = InstrumentNo + 10;
            return cloned;
        }
    }

}
