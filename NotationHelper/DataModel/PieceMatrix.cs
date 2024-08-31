namespace NotationHelper.DataModel.Elementary
{
    public class PieceMatrix
    {
        public PieceMatrix(int partCount, int barCount)
        {
            for (int partNo = 0; partNo < partCount; partNo++) 
            {
                var group = new Part() { PartNo = partNo };
                for (int barNo = 0; barNo < barCount; barNo++) 
                {
                    var bar = new Bar() { BarNo = barNo, PartNo = partNo};
                    bar.FirstVoice = new VoiceBar() { };
                    bar.FirstVoice.PartNo = partNo;
                    bar.FirstVoice.BarNo = barNo;
                    bar.FirstVoice.TimeGroups = new List<TimeGroup>();

                    for (int i = 0; i < 4; i++)
                    {
                        var vNoteGroup = new VNoteGroup();
                        vNoteGroup.PartNo = partNo;
                        vNoteGroup.BarNo = barNo;
                        vNoteGroup.Notes = new List<Note>();
                        Note note = new Note() 
                        { 
                            BarNo = barNo, PartNo = partNo ,
                        };
                        vNoteGroup.Notes.Add(note);
                        bar.FirstVoice.TimeGroups.Add(vNoteGroup);
                    }

                    group.Bars.Add(bar);
                }
                HBarGroups.Add(group);
            }
        }

        public List<Part> HBarGroups { get; set; } = new List<Part>();
        public Bar GetSingleBar(int partNo, int barNo)
        {
            var group = HBarGroups[partNo];
            var bar = group.Bars[barNo];
            return bar;
        }

        public List<TimeGroup> GetRangeByBarNo(int startBar, int barCount)
        {
            var resNotes = new List<TimeGroup>();

            foreach (var hgroup in HBarGroups)
            {
                var bars = hgroup.Bars.Skip(startBar).Take(barCount);
                var timeGroups = bars.SelectMany(b => b.FirstVoice.TimeGroups);
                resNotes.AddRange(timeGroups);
            }
            return resNotes;
        }

    }
}
