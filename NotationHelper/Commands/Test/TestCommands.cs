using MusicDataModel.DataModel.Piece;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotationHelper.Commands.Test
{
    public static class TestCommands
    {
        public static void Test()
        {
            var dummyDelete = new DeleteTimeHolderCommand(null);
            Note note = new Note();
            var realDelete = dummyDelete.Emit(note);
            realDelete.Execute();
        }
    }
}
