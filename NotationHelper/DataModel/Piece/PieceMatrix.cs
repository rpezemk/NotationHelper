using NotationHelper.DataModel.Elementary;
using NotationHelper.DataModel.Structure;
using NotationHelper.Helpers;

namespace NotationHelper.DataModel.Piece
{
    public class PieceMatrix : AObjectWithChildren<PieceMatrix, Part>
    {
        public override PieceMatrix ThisObj => this;
        public PieceMatrix() 
        {
            
        }

        public PieceMatrix(int partCount, int barCount)
        {
            for (int partNo = 0; partNo < partCount; partNo++)
            {
                var part = new Part() { PartNo = partNo };
                for (int barNo = 0; barNo < barCount; barNo++)
                {
                    var bar = new VoiceBar() { };
                    bar.AppendChild(Note.C().Sharp().Eight().AsTimeGroup());
                    bar.AppendChild(Note.D().Flat().Sixteen().AsTimeGroup());
                    bar.AppendChild(Note.E().Sharp().Sixteen().AsTimeGroup());
                    bar.AppendChild(Note.F().Flat().Eight().AsTimeGroup());
                    bar.AppendChild(Note.G().Flat().Sixteen().AsTimeGroup());
                    bar.AppendChild(Note.A().Flat().Sixteen().AsTimeGroup());
                    bar.AppendChild(Note.B().Flat().Quarter().AsTimeGroup());
                    bar.AppendChild(Note.C().UpOct().Flat().Quarter().AsTimeGroup());
                    part.Bars.Add(bar);
                }
                Parts.Add(part);
            }
        }


        public List<Part> Parts { get; set; } = new List<Part>();


        public List<TimeGroup> GetRangeByBarNo(int startBar, int barCount)
        {
            var resNotes = new List<TimeGroup>();

            foreach (var hgroup in Parts)
            {
                var bars = hgroup.Bars.Skip(startBar).Take(barCount);
                var Timegroups = bars.SelectMany(b => b.Children);
                resNotes.AddRange(Timegroups);
            }
            return resNotes;
        }

    }
}
