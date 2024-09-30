using MusicDataModel.DataModel.Structure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AudioTool.MidiModel
{
    public class MidiPiece : AObjectWithChildren<MidiPiece, MidiPart>
    {
        public override MidiPiece ThisObj => throw new NotImplementedException();
    }

    public class MidiPart : AObjectWithParentAndChildren<MidiPiece, MidiPart, MidiBar>
    {
        public List<MidiBar> bars = new List<MidiBar>();
        private MidiPiece parent;
        public MidiPiece Parent => parent;
        public override ObjectTypeEnum ParentType =>  ObjectTypeEnum.Piece;

        public void SetParent(MidiPiece obj)
        {
            parent = obj;
        }
    }
    public class MidiBar : AObjectWithParentAndChildren<MidiPart,  MidiBar, MidiTimeHolder> 
    {
        public List<MidiTimeHolder> MidiTimeHolders { get; set; } = new List<MidiTimeHolder>();
        public override ObjectTypeEnum ParentType => ObjectTypeEnum.Part;
    }

    public class MidiTimeHolder : AObjectWithParent<MidiBar, MidiTimeHolder>
    {
        public int BarOffset {  get; set; }
        public double Duration { get; set; }
        public override ObjectTypeEnum ParentType => ObjectTypeEnum.Bar;
    }

    public class MidiRest : MidiTimeHolder
    { }

    public class MidiNote : MidiTimeHolder
    {
        public int Pitch;
    }
}
