using NotationHelper.DataModel.Elementary;
using NotationHelper.DataModel.Structure;

namespace NotationHelper.DataModel.Piece
{
    public abstract class TimeGroup : AObjectWithParent<VoiceBar, TimeGroup>
    {
        public Duration Duration { get; set; } = new Duration();
        public abstract TimeGroupTypeEnum GroupType { get; }

        public int BarNo { get; set; }
        public int PartNo { get; set; }
    }
}
