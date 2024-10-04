using MusicDataModel.DataModel.Piece;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicDataModel.DataModel.Range
{
    public class MatrixRange
    {
        public MatrixRange() { }

        public MatrixRange(PieceMatrix pieceMatrix, int startBar, int count)
        {
            foreach (var barGroup in pieceMatrix.Children)
            {
                var partBars = barGroup.Children.GetRange(startBar, count);
                MonoPart part = new MonoPart();
                foreach(var partBar in partBars)
                    part.AppendChild(partBar);
                Parts.Add(part);
            }
        }

        public List<MonoPart> Parts = new List<MonoPart>();
    }


}
