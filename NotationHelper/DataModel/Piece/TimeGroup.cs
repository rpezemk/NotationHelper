using NotationHelper.DataModel.Elementary;
using NotationHelper.DataModel.Structure;

namespace NotationHelper.DataModel.Piece
{
    public abstract class TimeGroup : AObjectWithParent<VoiceBar, TimeGroup>
    {
        public Duration Duration { get; set; } = new Duration();
        public abstract TimeGroupTypeEnum GroupType { get; }

        public int BarNo { get; set; }
        public int PartNo { get; set; }

        public double MOffset { get; set; }
        public double XOffset { get; set; }
        public double YOffset { get; set; }
        public TimeGroup SetBaseDuration(DurationEnum duration)
        {
            Duration.BaseDuration = duration;
            return this;
        }

        public TimeGroup SetDotting(DottingEnum dotting)
        {
            Duration.Dotting = dotting;
            return this;
        }

        public VNoteGroup Note(params Note[] notes)
        {
            VNoteGroup vNoteGroup = new VNoteGroup();
            vNoteGroup.Duration = Duration;
            vNoteGroup.PartNo = PartNo;
            vNoteGroup.BarNo = BarNo;
            vNoteGroup.MOffset = MOffset;
            vNoteGroup.XOffset = XOffset;

            foreach (Note note in notes)
            {
                vNoteGroup.Notes.Add(note);
            }

            return vNoteGroup;
        }

        public static TimeGroup EmitNoteGroup() { return new VNoteGroup(); }
        public static TimeGroup EmitRest() { return new Rest(); }

    }
}
