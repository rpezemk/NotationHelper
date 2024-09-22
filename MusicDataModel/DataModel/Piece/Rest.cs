using MusicDataModel.DataModel.Elementary;
using MusicDataModel.DataModel.Structure;

namespace MusicDataModel.DataModel.Piece
{
    public class Rest : TimeHolder
    {
        public override TimeGroupTypeEnum GroupType => TimeGroupTypeEnum.Rest;

        public override ObjectTypeEnum ParentType => ObjectTypeEnum.Rest;
    }
}
