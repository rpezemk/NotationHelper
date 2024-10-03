using MusicDataModel.DataModel.Piece;
using NotationHelper.Commands.Base;

namespace NotationHelper.Commands
{
    public class DeleteManyTimeHolders : ANestedCommand<TimeHolder, DeleteTimeHolderCommand>
    {
        public override void Execute()
        {
            Execute(arguments);
        }

        protected override void Execute(List<TimeHolder> arguments)
        {
            foreach (var arg in arguments)
            {
                var cmd = new DeleteTimeHolderCommand(arg);
                subCommands.Add(cmd);
                cmd.Execute();
            }
        }
    }
}
