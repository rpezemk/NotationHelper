namespace AudioTool.CsEvents
{
    public class LayerSampleEvent : ACsEvent
    {
        public LayerSampleEvent(double start, double len, double startFrom, double startDynamics, double endDynamics)
        {
            Start = start;
            Length = len;
            StartSampleFrom = startFrom;
            StartDynamics = startDynamics;
            EndDynamics = endDynamics;
        }
        public double Start { get; set; }
        public double Length { get; set; }
        public double StartSampleFrom { get; set; }
        public double StartDynamics { get; set; }
        public double EndDynamics { get; set; }
    }

}
