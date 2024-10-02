using MusicDataModel.DataModel.Elementary;
using MusicDataModel.DataModel.Structure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicDataModel.DataModel.Piece
{
    public class Note : TimeHolder
    {
        public int X { get; set; }
        public int Y { get; set; }
        public Pitch Pitch { get; set; } = new Pitch();
        public int BarNo { get; set; }
        public int PartNo { get; set; }
        public bool IsTied {  get; set; }
        public ObjectTypeEnum ObjectType => ObjectTypeEnum.Note;
        public override ObjectTypeEnum ParentType => ObjectTypeEnum.TimeGroup;
        public ObjectTypeEnum ChildType => ObjectTypeEnum.None;

        public override TimeGroupTypeEnum GroupType => TimeGroupTypeEnum.Note;

        public Note SetName(NoteName noteName) 
        {
            Pitch.BaseNoteName = noteName; 
            return this;
        }

        public Note AlterOctave(int nOctaves)
        {
            Pitch.OctaveNo += nOctaves;
            return this;
        }
        public Note SetAlter(int alter)
        {
            Pitch.Alter = alter;
            return this;
        }
        public static Note Emit() => new Note();

        public Note SetBaseDuration(DurationEnum baseDuration)
        {
            Duration.BaseDuration = baseDuration;
            return this;
        }

        public Note SetDotting(DottingEnum dotting)
        {
            Duration.Dotting = dotting;
            return this;
        }

        public Note Tied()
        {
            IsTied = true;
            return this;
        }
        

        public static Note C() => Emit().SetName(DataModel.Elementary.NoteName.C);
        public static Note D() => Emit().SetName(DataModel.Elementary.NoteName.D);
        public static Note E() => Emit().SetName(DataModel.Elementary.NoteName.E);
        public static Note F() => Emit().SetName(DataModel.Elementary.NoteName.F);
        public static Note G() => Emit().SetName(DataModel.Elementary.NoteName.G);
        public static Note A() => Emit().SetName(DataModel.Elementary.NoteName.A);
        public static Note B() => Emit().SetName(DataModel.Elementary.NoteName.B);
    }
}
