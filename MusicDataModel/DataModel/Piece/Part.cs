using MusicDataModel.DataModel.Structure;

namespace MusicDataModel.DataModel.Piece
{
    public class Part : AObjectWithParentAndChildren<PieceMatrix, Part, VoiceBar>
    {
        public int PartNo { get; set; }


        public override ObjectTypeEnum ParentType => ObjectTypeEnum.Part;
    }
}
