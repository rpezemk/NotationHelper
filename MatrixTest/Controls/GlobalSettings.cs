using MatrixTest.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatrixTest.Controls
{
    public static class GlobalSettings
    {
        public static double Scaling { get; set; } = 0.2;
        public static double LineThickness { get; set; } = 2;
        public static double LineSpacing { get; set; } = 10;
        public static double StaffMargin { get; set; } = 50;
    }

    public static class SampleData
    {
        public static Piece GetSamplePiece()
        {
            Piece piece = new Piece();
            ScorePart part1 = new ScorePart();
            ScorePart part2 = new ScorePart();
            piece.Parts.Add(part1);
            piece.Parts.Add(part2);

            int noOfBars = 10;
            var noOfParts = piece.Parts.Count;

            for (int partNo = 0; partNo < noOfBars; partNo++)
            {
                for (int i = 0; i < noOfBars; i++)
                {
                    var bar = new Bar();
                    bar.Notes.Add(Note.A().Flat());

                    piece.Parts[i].Bars.Add(bar);
                }
            }

            return piece;
        }
    }
}
