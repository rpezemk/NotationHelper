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

        public static void UnSelectOtherBars(BarWithLine barWithLine)
        {
            foreach (var barControl in OperationBindings.BarsWithSelectedNotes.Where(b => b != barWithLine))
            {
                var holders = barControl.GetTimeHolders();
                foreach (var th in holders.Where(th => th.TimeHolder.IsSelected))
                {
                    barControl.RedrawUnselected(th);
                }
            }
            OperationBindings.BarsWithSelectedNotes.Clear();
            OperationBindings.BarsWithSelectedNotes.Add(barWithLine);
        }

    }
}
