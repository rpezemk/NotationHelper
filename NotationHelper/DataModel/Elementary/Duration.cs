namespace NotationHelper.DataModel.Elementary
{
    public class Duration
    {
        public DurationEnum BaseDuration { get; set; } = DurationEnum.Querter;
        public DottingEnum Dotting { get; set; } = DottingEnum.NoDots;
        public float GetInLength() => 4 / (float)(int)BaseDuration;
    }

    public enum DurationEnum
    {
        Longa = 1,
        Breve = 2,
        Whole = 4,
        Half = 8,
        Querter = 16,
        Eight = 32,
        Sixteen = 64,
        ThirtyTwo = 128,
    }

    public enum DottingEnum
    {
        NoDots = 4,
        SingleDot = 6,
        DoubleDot = 7
    }

    public enum GroupTypeEnum
    {
        Rest,
        NoteGroup
    }

    public abstract class TimeGroup
    {
        public Duration Duration { get; set; } = new Duration();
        public abstract GroupTypeEnum GroupType { get; }

        public int BarNo { get; set; }
        public int PartNo { get; set; }
    }

    public class Rest : TimeGroup
    {
        public override GroupTypeEnum GroupType => GroupTypeEnum.Rest;
    }

    public class VNoteGroup : TimeGroup
    {
        public override GroupTypeEnum GroupType => GroupTypeEnum.NoteGroup;
        public List<Note> Notes { get; set; } = new List<Note>() { new Note(), new Note() { Pitch = new Pitch() { Alter = 1 } }, new Note() { Pitch = new Pitch() { Alter = -1 } }, new Note() };
    }

    public class VoiceBar
    {
        public int BarNo { get; set; }
        public int PartNo { get; set; }
        public List<TimeGroup> TimeGroups { get; set; } = new List<TimeGroup>() 
        {
            new VNoteGroup()
        };
    }

    public class Bar
    {
        public int BarNo { get; set; }
        public int PartNo { get; set; }
        public VoiceBar FirstVoice { get; set; } = new VoiceBar() {  };
        public VoiceBar SecondVoice {  get; set; }
    }


    public class Part
    {
        public int PartNo { get; set; }
        public List<Bar> Bars { get; set; } = new List<Bar>();
    }

    public class VBarGroup
    {
        public List<Bar> Bars { get; set; }
    }

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
                            X = barNo * 100,
                            Y = partNo * 30
                        };
                        note.X += i * 20;
                        note.Y += note.Pitch.ResultPitch + i*5;
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
