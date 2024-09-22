using MusicDataModel.DataModel.Piece;
using MusicDataModel.DataModel.Structure;
using MusicDataModel.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicDataModel.Views.MusicViews
{
    public class VisualNoteContainer
    {
        public List<VisualNote> VisualNotes { get; set; } = new List<VisualNote>();
        public VisualNoteContainer(List<VisualNote> visualNotes)
        {
            VisualNotes = visualNotes;
        }   

        public void Recalculate(double totalWidth)
        {
            VisualNotes.CalculateResult(totalWidth);
        }

    }

    public class VisualNote : IWidthable
    {
        public Note Note { get; set; }
        public double RightMargin {  get; set; }
        public double LeftMargin { get; set; }

        public double Height { get; set; }
        public double Weight { get => weight; set => weight = value; }
        public double ResValue { get => resValue; set => resValue = value; }

        public VisualNote(Note note)
        {
            Note = note;
        }
        private double weight;
        private double resValue;
    }
}
