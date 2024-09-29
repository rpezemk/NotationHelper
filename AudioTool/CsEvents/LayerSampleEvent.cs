namespace AudioTool.CsEvents
{
    public class LayerSampleEvent : ACsEvent
    {
        public LayerSampleEvent(double start, double len, double startFrom, double startDynamics, double endDynamics, double sampleLen)
        {
            Start = start;
            Length = len;
            StartSampleFrom = startFrom;
            StartDynamics = startDynamics;
            EndDynamics = endDynamics;
            SampleLen = sampleLen;
        }
        public double Start { get; set; }
        public double Length { get; set; }
        public double StartSampleFrom { get; set; }
        public double StartDynamics { get; set; }
        public double EndDynamics { get; set; }
        public double SampleLen { get; set; }
        public LayerSampleEvent AsModulator()
        {
            LayerSampleEvent cloned = new LayerSampleEvent(Start, Length, StartSampleFrom, StartDynamics, EndDynamics, SampleLen);
            cloned.InstrumentNo = InstrumentNo + 10;
            return cloned;
        }
    }


    public class LayerDynamicsEvent : ACsEvent
    {
        public LayerDynamicsEvent(double duration, double destinationPeriod, double destinationDynamics)
        {
            Duration = duration;
            DestinationPeriod = destinationPeriod;
            DestinationDynamics = destinationDynamics;
        }
        public double Duration { get; set; }
        public double DestinationPeriod { get ; set; }
        public double DestinationDynamics { get ; set; }
    }

}
