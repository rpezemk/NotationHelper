using MusicDataModel.DataModel.Piece;
using MusicDataModel.MusicViews.MusicControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotationHelper.MVC
{
    public static class OperationBindings
    {
        public static List<TimeHolder> SelectedTimeHolders = new List<TimeHolder>();
        public static List<BarWithLine> BarsWithSelectedNotes = new List<BarWithLine>();
    }
}
