using NotationHelper.DataModel.Elementary;
using NotationHelper.DataModel.Piece.Parts.Bar.Timegroups;

namespace NotationHelper.DataModel.Piece.Parts.Bars.TimeGroups.Notes
{
    public class Rest : TimeGroup
    {
        public override TimeGroupTypeEnum GroupType => TimeGroupTypeEnum.Rest;
    }
}
