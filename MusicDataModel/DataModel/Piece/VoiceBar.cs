using MusicDataModel.DataModel.Elementary;
using MusicDataModel.DataModel.Structure;

namespace MusicDataModel.DataModel.Piece
{
    public class VoiceBar : AObjectWithParentAndChildren<MonoPart, VoiceBar, TimeHolder>
    {
        public Meter Meter { get; set; } = new Meter() { Numerator = 4, Denominator = DurationEnum.Querter };
        public int BarNo { get; set; }
        public int PartNo { get; set; }
        public override ObjectTypeEnum ParentType => ObjectTypeEnum.Bar;
    }
}
