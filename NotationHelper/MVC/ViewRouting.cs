using MusicDataModel.DataModel.Piece;
using MusicDataModel.Helpers;
using MusicDataModel.MusicViews.MusicControls;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace NotationHelper.MVC
{

    public static class ViewRouting
    {
        public static List<TimeHolder> SelectedTimeHolders = new List<TimeHolder>();
        public static List<BarWithLine> BarsWithSelectedNotes = new List<BarWithLine>();


        public static void AnyEvent(params object[] objects)
        {
            AllRoutings.Instance
                .AllActions
                .Where(a => a.CanAccept(objects))
                .ForEach(a => a.RunAction(objects));
            
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
            if (!RoutingCommands.SelectMeasures.IsCurrentAction())
            {
                BarsWithSelectedNotes.Where(b => b != barWithLine)
                    .ForEach(a => a.GetTimeHolders()
                                    .Where(th => th.TimeHolder.IsSelected)
                                    .ForEach(th => a.RedrawUnselected(th)));

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

        public static void BarWithLineMouseDown(BarWithLine barWithLine, MouseButtonEventArgs e)
        {
            List<TimeHolderDrawing> visuals = barWithLine.GetTimeHolders();
            Point mousePos = e.GetPosition(barWithLine.MyVisualHost);
            var nowClicked = visuals.Where(vis => vis.HitTest<DrawingVisual>(mousePos)).ToList();
            var prevSelected = visuals.Where(v => v.TimeHolder.IsSelected).ToList();

            if (!RoutingCommands.SelectMeasures.IsCurrentAction())
                prevSelected.ButNotIn(nowClicked).ForEach(v => barWithLine.RedrawUnselected(v));

            nowClicked.ButNotIn(prevSelected).ToList().ForEach(v => barWithLine.RedrawSelected(v));
            UnSelectOtherBars(barWithLine);
        }
    }

    
}
