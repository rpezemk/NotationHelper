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
    }
}
