using NotationHelper.DataModel.Structure;

namespace NotationHelper.DataModel.Piece
{
    public class Part : AObjectWithParentAndChildren<PieceMatrix, Part, VoiceBar>
    {
        public int PartNo { get; set; }
        public List<VoiceBar> Bars { get; set; } = new List<VoiceBar>();

        public override ObjectTypeEnum ParentType => ObjectTypeEnum.Part;
    }
}
