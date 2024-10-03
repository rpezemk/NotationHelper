using MusicDataModel.DataModel.Piece;
using MusicDataModel.MusicViews.MusicControls;
using NotationHelper.Commands.Base;
using NotationHelper.MVC;

namespace NotationHelper.Commands
{
    public class DeleteTimeHolderCommand : AEditCommand<TimeHolder>
    {
        public DeleteTimeHolderCommand() : base() { }
        public override void Execute()
        {
            if (Value == null)
                return;
            Value.Parent.ReplaceChild(Value, new Rest() { Duration = Value.Duration });
        }
    }


    public class DeleteManyTimeHoldersCommand : AEditCommand<List<TimeHolder>>
    {
        public override void Execute()
        {
            if (Value == null)
                return;
            foreach (var value in Value)
            {
                value.Parent.ReplaceChild(value, new Rest() { Duration = value.Duration });
            }
        }
    }

    public class SelectAllVisibleTimeHoldersCommand : AEditCommand<List<BarWithLine>>
    {
        public override void Execute()
        {
            if (Value == null)
                return;

            foreach(var bar in Value)
            {
                var drawings = bar.GetTimeHolderDrawings();
                foreach (var draw in drawings)
                {
                    draw.Redraw(true, BarWithLine.Scale);
                }
            }
        }
    }
}
