using NotationHelper.DataModel.Piece;
using NotationHelper.DataModel.Structure;

namespace NotationHelper.DataModel.Elementary
{
    public class VNoteGroup : TimeGroup
    {
        public override TimeGroupTypeEnum GroupType => TimeGroupTypeEnum.NoteGroup;
        public List<Note> Notes { get; set; } = new List<Note>();

        public override ObjectTypeEnum ParentType => ObjectTypeEnum.VNoteGroup;
    }
}
