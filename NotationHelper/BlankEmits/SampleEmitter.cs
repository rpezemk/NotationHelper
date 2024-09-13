using NotationHelper.DataModel.Elementary;
using NotationHelper.DataModel.Piece;
using NotationHelper.Views.MusicViews;

namespace NotationHelper.BlankEmits
{
    public static class SampleEmitter
    {
        public static VisualNoteContainer GetVisualNoteContainer() 
        {
            List<VisualNote> visualNotes = new List<VisualNote>();
            for(int i = 0; i < 10; i++)
            {
                Note note = new Note();
                VisualNote visualNote = new VisualNote(note);
                visualNotes.Add(visualNote);
            }
            VisualNoteContainer visualNoteContainer = new VisualNoteContainer(visualNotes);
            return visualNoteContainer;
        }
        public static Note GetSampleNote(int partNo, int barNo, int abc)
        {
            var note = new Note();
            note.PartNo = partNo;
            note.BarNo = barNo;
            note.Pitch = new Pitch() { BaseNoteName = NoteName.C, Alter = 1, OctaveNo = 5 };
            return note;
        }
        public static VNoteGroup GetSampleVNoteGroup(int partNo, int barNo)
        {
            VNoteGroup vNoteGroup = new VNoteGroup();
            vNoteGroup.PartNo = partNo;
            vNoteGroup.BarNo = barNo;
            vNoteGroup.Duration = new Duration() { BaseDuration = DurationEnum.Querter };
            vNoteGroup.Notes = new List<Note>() { GetSampleNote(partNo, barNo, 4) };
            return vNoteGroup;
        }

        public static VoiceBar GetSampleVoiceBar()
        {
            var bar = new VoiceBar();
            bar.Meter = new Meter() { Numerator = 4, Denominator = DurationEnum.Querter };
            bar.Timegroups = new List<TimeGroup>();
            for (var i = 0; i < bar.Meter.Numerator; i++)
            {
                TimeGroup timeGroup = GetSampleVNoteGroup(0, i);
                timeGroup.Duration = new Duration() { BaseDuration = DurationEnum.Querter };
                bar.Timegroups.Add(timeGroup);
            }
                return new VoiceBar();
        }

        public static List<VoiceBar> GetSampleVoiceBars(int nBars)
        {
            var bars = new List<VoiceBar>();
            for(int i = 0; i < nBars;i++)
            {
                bars.Add(GetSampleVoiceBar());
            }
            return bars;
        }
    }
}
