using NotationHelper.DataModel.Elementary;
using NotationHelper.DataModel.Structure;

namespace NotationHelper.DataModel.Piece
{
    public class PieceMatrix : AObjectWithChildren<PieceMatrix, Part>
    {
        public override PieceMatrix ThisObj => this;
        public PieceMatrix(int partCount, int barCount)
        {
            for (int partNo = 0; partNo < partCount; partNo++)
            {
                var group = new Part() { PartNo = partNo };
                for (int barNo = 0; barNo < barCount; barNo++)
                {
                    var bar = new VoiceBar() { };
                    for (int i = 0; i < 16; i++)
                    {
                        var vNoteGroup = new VNoteGroup();
                        vNoteGroup.PartNo = partNo;
                        vNoteGroup.BarNo = barNo;
                        vNoteGroup.Notes = new List<Note>();
                        Note note = new Note()
                        {
                            BarNo = barNo,
                            PartNo = partNo,
                        };
                        vNoteGroup.Notes.Add(note);
                        bar.Timegroups.Add(vNoteGroup);
                    }

                    group.Bars.Add(bar);
                }
                Parts.Add(group);
            }
        }

        public List<Part> Parts { get; set; } = new List<Part>();


        public List<TimeGroup> GetRangeByBarNo(int startBar, int barCount)
        {
            var resNotes = new List<TimeGroup>();

            foreach (var hgroup in Parts)
            {
                var bars = hgroup.Bars.Skip(startBar).Take(barCount);
                var Timegroups = bars.SelectMany(b => b.Timegroups);
                resNotes.AddRange(Timegroups);
            }
            return resNotes;
        }

    }
}
