using MusicDataModel.DataModel.Structure;

namespace MusicDataModel.DataModel.Piece
{
    public class MonoPart : AObjectWithParentAndChildren<PieceMatrix, MonoPart, VoiceBar>
    {
        public TimeHolder GetFirstTimeHolder() => Children.SelectMany(vb => vb.Children).FirstOrDefault();
        public int PartNo { get; set; }
        public double BPM { get; set; } = 60;
        public Elementary.DurationEnum BeatBase { get; set; } = Elementary.DurationEnum.Querter;
        public override ObjectTypeEnum ParentType => ObjectTypeEnum.Part;
    }
}
