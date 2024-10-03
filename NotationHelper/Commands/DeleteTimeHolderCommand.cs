using MusicDataModel.DataModel.Piece;
using NotationHelper.Commands.Base;

namespace NotationHelper.Commands
{
    public class DeleteTimeHolderCommand : AEditCommand<TimeHolder, DeleteTimeHolderCommand>
    {
        public DeleteTimeHolderCommand(TimeHolder value) : base(value) { }
        protected override void Execute(TimeHolder value)
        {
            value.Parent.ReplaceChild(value, new Rest() { Duration = value.Duration });
        }
        public override DeleteTimeHolderCommand Emit(TimeHolder o)
        {
            return new DeleteTimeHolderCommand(o);
        }
    }


    public class DeleteManyTimeHoldersCommand : AEditCommand<List<TimeHolder>, DeleteManyTimeHoldersCommand>
    {
        public DeleteManyTimeHoldersCommand(List<TimeHolder> value) : base(value) { }
        protected override void Execute(List<TimeHolder> values)
        {
            foreach(var value in values)
            {
                value.Parent.ReplaceChild(value, new Rest() { Duration = value.Duration });
            }
        }
        public override DeleteManyTimeHoldersCommand Emit(List<TimeHolder> o)
        {
            return new DeleteManyTimeHoldersCommand(o);
        }
    }

    public class DeleteTimeHolderCommand2 : AEditCommand<TimeHolder, DeleteTimeHolderCommand2>
    {
        public DeleteTimeHolderCommand2(TimeHolder value) : base(value) { }
        protected override void Execute(TimeHolder value)
        {
            value.Parent.ReplaceChild(value, new Rest() { Duration = value.Duration });
        }
        public override DeleteTimeHolderCommand2 Emit(TimeHolder o)
        {
            return new DeleteTimeHolderCommand2(o);
        }
    }

}
