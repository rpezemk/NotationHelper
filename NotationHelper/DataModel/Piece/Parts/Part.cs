using NotationHelper.DataModel.Piece.Parts.Bar;

namespace NotationHelper.DataModel.Piece.Parts
{
    public class Part
    {
        public int PartNo { get; set; }
        public List<VoiceBar> Bars { get; set; } = new List<VoiceBar>();
    }
}
