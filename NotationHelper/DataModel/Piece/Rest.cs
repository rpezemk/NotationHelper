using NotationHelper.DataModel.Elementary;
using NotationHelper.DataModel.Structure;

namespace NotationHelper.DataModel.Piece
{
    public class Rest : TimeGroup
    {
        public override TimeGroupTypeEnum GroupType => TimeGroupTypeEnum.Rest;

        public override ObjectTypeEnum ParentType => ObjectTypeEnum.Rest;
    }
}
