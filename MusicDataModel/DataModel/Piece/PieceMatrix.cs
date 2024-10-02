using MusicDataModel.DataModel.Structure;
using MusicDataModel.Helpers;

namespace MusicDataModel.DataModel.Piece
{
    public class PieceMatrix : AObjectWithChildren<PieceMatrix, MonoPart>
    {
        public override PieceMatrix ThisObj => this;
        public PieceMatrix()
        {

        }

        public PieceMatrix(int partCount, int barCount)
        {
            var random = new Random();
            for (int partNo = 0; partNo < partCount; partNo++)
            {
                var part = new MonoPart() { PartNo = partNo };
                for (int barNo = 0; barNo < barCount; barNo++)
                {
                    var barrOdd = barNo % 2 == 0;
                    if (barrOdd)
                    {
                        var test = random.Next(0, 2) == 0 ? false : true;
                        TimeHolder randomHolder = (test ? Rest.Emit().Eight() : Note.F().Sharp().Eight()).AsTimeGroup();
                        var bar = new VoiceBar() { };
                        bar.AppendChild(Note.C().Sharp().Eight().AsTimeGroup());
                        bar.AppendChild(Note.D().Flat().Sixteen().AsTimeGroup());
                        bar.AppendChild(Note.E().Sharp().Sixteen().AsTimeGroup());
                        bar.AppendChild(randomHolder);
                        bar.AppendChild(Note.G().Flat().Sixteen().AsTimeGroup());
                        bar.AppendChild(Note.A().Flat().Sixteen().AsTimeGroup());
                        bar.AppendChild(Note.B().Flat().Tied().Quarter().AsTimeGroup());
                        bar.AppendChild(Note.C().UpOct().Flat().Quarter().AsTimeGroup());
                        part.Children.Add(bar);
                    }
                    else
                    {
                        var test = random.Next(0, 2) == 0 ? false : true;
                        TimeHolder randomHolder = (test ? Rest.Emit().Eight() : Note.F().Sharp().Eight()).AsTimeGroup();
                        var bar = new VoiceBar() { };
                        bar.AppendChild(Note.C().UpOct().Flat().Quarter().AsTimeGroup());
                        bar.AppendChild(Note.B().Flat().Quarter().AsTimeGroup());
                        bar.AppendChild(Note.A().Flat().Sixteen().AsTimeGroup());
                        bar.AppendChild(Note.G().Flat().Sixteen().AsTimeGroup());
                        bar.AppendChild(Note.E().Sharp().Sixteen().AsTimeGroup());
                        bar.AppendChild(Note.D().Flat().Sixteen().AsTimeGroup());
                        bar.AppendChild(randomHolder);
                        bar.AppendChild(Note.C().Sharp().Eight().AsTimeGroup());
                        part.Children.Add(bar);
                    }
                }
                Parts.Add(part);
            }
        }


        public List<MonoPart> Parts { get; set; } = new List<MonoPart>();


        public List<TimeHolder> GetRangeByBarNo(int startBar, int barCount)
        {
            var resNotes = new List<TimeHolder>();

            foreach (var hgroup in Parts)
            {
                var bars = hgroup.Children.Skip(startBar).Take(barCount);
                var Timegroups = bars.SelectMany(b => b.Children);
                resNotes.AddRange(Timegroups);
            }
            return resNotes;
        }

    }
}
