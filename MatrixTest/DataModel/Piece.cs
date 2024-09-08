using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatrixTest.DataModel
{
    public class Piece
    {
        public Piece() { }
        public List<ScorePart> Parts { get; set; } = new List<ScorePart>() { };

        public int NoOfBars => Parts.Select(p => p.Bars.Count).Max();
    }


    public class ScorePart
    {
        public List<Bar> Bars { get; set; } = new List<Bar>();
    }

    public class  Note
    {
        public int BaseOctave { get; set; }
        public BaseNameEnum BaseName { get; set; }
        public int ChromaAlter { get; set; }
        public static Note A() => new Note() { BaseName = BaseNameEnum.A };
        public static Note B() => new Note() { BaseName = BaseNameEnum.B };
        public static Note C() => new Note() { BaseName = BaseNameEnum.C };
        public static Note D() => new Note() { BaseName = BaseNameEnum.D };
        public static Note E() => new Note() { BaseName = BaseNameEnum.E };
        public static Note F() => new Note() { BaseName = BaseNameEnum.F };
        public static Note G() => new Note() { BaseName = BaseNameEnum.G };
        public Note Flat(int flatSteps = 1)
        {
            ChromaAlter = - flatSteps;
            return this;
        }

        public Note Sharp(int sharpSteps = 1)
        {
            ChromaAlter = sharpSteps;
            return this;
        }
    }



}
