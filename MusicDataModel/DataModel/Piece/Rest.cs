using MusicDataModel.DataModel.Elementary;
using MusicDataModel.DataModel.Structure;

namespace MusicDataModel.DataModel.Piece
{
    public class Rest : TimeHolder
    {
        public override TimeGroupTypeEnum GroupType => TimeGroupTypeEnum.Rest;

        public override ObjectTypeEnum ParentType => ObjectTypeEnum.Rest;
        public static Rest Emit() => new Rest();
        public static TimeHolder Whole() => Emit().SetBaseDuration(DurationEnum.Whole);
        public static TimeHolder Half() => Emit().SetBaseDuration(DurationEnum.Half);
        public static TimeHolder Quarter() => Emit().SetBaseDuration(DurationEnum.Querter);
        public static TimeHolder Eight() => Emit().SetBaseDuration(DurationEnum.Eight);
        public static TimeHolder Sixteen() => Emit().SetBaseDuration(DurationEnum.Sixteen);
        
    }
}
