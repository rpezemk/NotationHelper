using NotationHelper.DataModel.Elementary;
using NotationHelper.DataModel.Structure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotationHelper.DataModel.Piece
{
    public class Note : AObjectWithParent<TimeGroup, Note>
    {
        public int X { get; set; }
        public int Y { get; set; }
        public Pitch Pitch { get; set; } = new Pitch();
        public int BarNo { get; set; }
        public int PartNo { get; set; }

        public ObjectTypeEnum ObjectType => ObjectTypeEnum.Note;
        public override ObjectTypeEnum ParentType => ObjectTypeEnum.TimeGroup;
        public ObjectTypeEnum ChildType => ObjectTypeEnum.None;
    }
}
