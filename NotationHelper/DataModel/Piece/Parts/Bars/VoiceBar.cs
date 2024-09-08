using NotationHelper.DataModel.Elementary;
using NotationHelper.DataModel.Piece.Parts.Bar.Timegroups;

namespace NotationHelper.DataModel.Piece.Parts.Bar
{
    public class VoiceBar
    {
        public Meter Meter { get; set; } = new Meter() { Numerator = 4, Denominator = DurationEnum.Querter };
        public int BarNo { get; set; }
        public int PartNo { get; set; }
        public List<TimeGroup> Timegroups { get; set; } = new List<TimeGroup>()
        {
            new VNoteGroup()
        };
    }
}
