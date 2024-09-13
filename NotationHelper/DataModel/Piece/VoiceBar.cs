using NotationHelper.DataModel.Elementary;
using NotationHelper.DataModel.Structure;

namespace NotationHelper.DataModel.Piece
{
    public class VoiceBar : AObjectWithParentAndChildren<Part, VoiceBar, TimeGroup>
    {
        public Meter Meter { get; set; } = new Meter() { Numerator = 4, Denominator = DurationEnum.Querter };
        public int BarNo { get; set; }
        public int PartNo { get; set; }
        public List<TimeGroup> Timegroups { get; set; } = new List<TimeGroup>();

        public override ObjectTypeEnum ParentType => ObjectTypeEnum.Bar;
    }
}
