using NotationHelper.MusicViews;
using NotationHelper.DataModel.Elementary;
using NotationHelper.DataModel.Piece;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace NotationHelper.Helpers
{
    public static class TestHelper
    {
        public static List<NoteControl> GetRandomVisualNotes(int count, int maxX, int maxY)
        {
            var notes = new List<NoteControl>();
            Random rand = new Random();
            for(int i = 0; i < count; i++)
            {
                NoteControl noteControl = new NoteControl();
                var x = rand.Next(maxX);
                var y = rand.Next(maxY);
                noteControl.X = x;
                noteControl.Y = y;
                notes.Add(noteControl);
            }
            return notes;
        }

        public static List<Note> GetRandomNotes(int count, int maxX, int maxY)
        {
            var notes = new List<Note>();
            Random rand = new Random();
            for (int i = 0; i < count; i++)
            {
                Note noteControl = new Note();
                var x = rand.Next(maxX);
                var y = rand.Next(maxY);
                noteControl.X = x;
                noteControl.Y = y;
                notes.Add(noteControl);
            }
            return notes;
        }

        public static List<Note> GetRandomNotes(PieceMatrix pieceMatrix)
        {
            var notes = pieceMatrix.GetRangeByBarNo(0, 4);
            return notes.Where(tg => tg.GroupType == TimeGroupTypeEnum.NoteGroup)
                .Select(tg => tg as VNoteGroup)
                .SelectMany(vng => vng.Notes).ToList();
        }

    }

}
