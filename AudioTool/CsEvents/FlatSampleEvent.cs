namespace AudioTool.CsEvents
{
    public class FlatSampleEvent : ACsEvent
    {
        public FlatSampleEvent(double start, double len, double startFrom)
        {
            Start = start;
            Length = len;
            StartSampleFrom = startFrom;
        }
        public double Start { get; set; }
        public double Length { get; set; }
        public double StartSampleFrom { get; set; }
    }

}
