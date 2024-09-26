using AudioTool.CSoundWrapper;
using AudioTool.Instruments.Articulations;

namespace AudioTool.Instruments
{
    public interface IScriptInstrument
    {
        public string GetInstrumentScript();
    }
    public class BowedInstrument : IScriptInstrument
    {
        public BowedInstrument(string name) 
        {
            Name = name;
        }
        public string Name { get; set; }
        public List<string> BowedPerDynamics { get; set; } = new List<string>();
        public List<string> PizzPerDynamics { get; set; } = new List<string>();
        public int PitchOffset { get; set; }

        public void SetDynamics() { }
        public void Play(int pitch, int dynamics) { }
        public void Articulation(ArticulationEnum articulation) { }
        public void PlayMethod(PlayMethodEnum method) { }

        public string GetInstrumentScript()
        {
            return "";
        }
    }
}
