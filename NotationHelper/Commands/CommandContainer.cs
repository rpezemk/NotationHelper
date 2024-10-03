using MusicDataModel.DataModel.Piece;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotationHelper.Commands
{
    public static class CommandContainer
    {
        public static void RemoveNote(Note note)
        {
            DeleteTimeHolderCommand command = new DeleteTimeHolderCommand(note);
            command.Execute();
        }
    }
}
