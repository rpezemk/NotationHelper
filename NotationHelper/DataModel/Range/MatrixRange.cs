using NotationHelper.DataModel.Elementary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotationHelper.DataModel.Range
{
    public class MatrixRange
    {
        public MatrixRange() { }

        public MatrixRange(PieceMatrix pieceMatrix, int startBar, int count)
        {
            foreach (var barGroup in pieceMatrix.Parts)
            {
                var partBars = barGroup.Bars.GetRange(startBar, count);
                Part part = new Part() { Bars = partBars };
                Parts.Add(part);
            }
        }

        public List<Part> Parts = new List<Part>();
    }


}
