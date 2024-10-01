using MusicDataModel.DataModel.Piece;
using MusicDataModel.MusicViews.MusicControls;
using System.Windows.Input;

namespace NotationHelper.MVC
{

    public static class OperationBindings
    {
        public static List<TimeHolder> SelectedTimeHolders = new List<TimeHolder>();
        public static List<BarWithLine> BarsWithSelectedNotes = new List<BarWithLine>();


        public static void AnyEvent(params object[] objects)
        {
            var mode = RoutingCommands.GetCurrMode();
            if (mode == null)
                return;

            if(!mode.CanAccept(objects))
                return;

            mode.RunAction(objects);
        }



        public static bool IsControlPressed()
        {
            return Keyboard.IsKeyDown(Key.LeftCtrl) || Keyboard.IsKeyDown(Key.RightCtrl);
        }

        public static void UnSelectOtherBars(BarWithLine barWithLine)
        {
            if (RoutingCommands.SelectMeasures.IsCurrentMode() == false)
            {

                foreach (var barControl in BarsWithSelectedNotes.Where(b => b != barWithLine))
                {
                    var holders = barControl.GetTimeHolders();
                    foreach (var th in holders.Where(th => th.TimeHolder.IsSelected))
                    {
                        barControl.RedrawUnselected(th);
                    }
                }
                BarsWithSelectedNotes.Clear();
            }
            BarsWithSelectedNotes.Add(barWithLine);
        }

        public static void UnSelectAll()
        {
            foreach (var barControl in BarsWithSelectedNotes)
            {
                var holders = barControl.GetTimeHolders();
                foreach (var th in holders.Where(th => th.TimeHolder.IsSelected))
                {
                    barControl.RedrawUnselected(th);
                }
            }
            BarsWithSelectedNotes.Clear();
        }

        public static void SelectAllRange()
        {
            if (IsControlPressed() == false)
                return;
        }
    }

    
}
