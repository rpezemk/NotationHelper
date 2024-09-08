using NotationHelper.DataModel.Elementary;

namespace NotationHelper.DataModel.Piece.Parts.Bar.Timegroups
{
    public abstract class TimeGroup
    {
        public Duration Duration { get; set; } = new Duration();
        public abstract TimeGroupTypeEnum GroupType { get; }

        public int BarNo { get; set; }
        public int PartNo { get; set; }
    }
}
