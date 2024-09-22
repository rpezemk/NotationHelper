using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicDataModel.MidiModel
{
    public class RhythmHolder
    {
        public double Duration;
        public int Pitch;
        public Guid Guid;
    }

    public class PitchHolder : RhythmHolder
    {
        public int Pitch;
    }
}
