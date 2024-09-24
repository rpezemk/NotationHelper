using AudioTool.Instruments.Articulations;

namespace AudioTool.Instruments
{
    public class BowedInstrument
    {
        public string Name { get; set; }
        public List<string> BowedPerDynamics { get; set; } = new List<string>();
        public List<string> PizzPerDynamics { get; set; } = new List<string>();
        public int PitchOffset { get; set; }

        public void SetDynamics() { }
        public void Play(int pitch, int dynamics) { }
        public void Articulation(ArticulationEnum articulation) { }
        public void PlayMethod(PlayMethodEnum method) { }
    }
}
