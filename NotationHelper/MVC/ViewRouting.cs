using MusicDataModel.DataModel.Piece;
using MusicDataModel.Helpers;
using MusicDataModel.MusicViews.MusicControls;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace NotationHelper.MVC
{

    public static class SelectedBarsCollection
    {
        public static List<BarWithLine> BarsWithSelectedNotes = new List<BarWithLine>();
        public static void UnSelectAll()
        {
            BarsWithSelectedNotes.ForEach(bc => bc.GetTimeHolders()
                        .Where(th => th.TimeHolder.IsSelected)
                        .ForEach(b => b.Redraw(false, BarWithLine.Scale)));
            BarsWithSelectedNotes.Clear();
        }

        public static void UnSelectExceptOf(BarWithLine barWithLine)
        {
            BarsWithSelectedNotes.Where(b => b != barWithLine)
                .ForEach(a => a.GetTimeHolders()
                                .Where(th => th.TimeHolder.IsSelected)
                                .ForEach(th => th.Redraw(false, BarWithLine.Scale)));
            BarsWithSelectedNotes.Clear();
        }

        internal static void Add(BarWithLine barWithLine)
        {
            BarsWithSelectedNotes.Add(barWithLine);
        }
    }

    public static class ViewRouting
    {
        public static void AnyEvent(params object[] objects)
        {
            AllRoutings.Instance
                .AllActions
                .Where(a => a.CanAccept(objects))
                .ForEach(a => a.RunAction(objects));

            var mode = RoutingCommands.GetCurrMode();
            if (mode == null)
                return;

            if (!mode.CanAccept(objects))
                return;

            mode.RunAction(objects);
        }

        public static void BarWithLineMouseDown(BarWithLine barWithLine, MouseButtonEventArgs e)
        {
            List<TimeHolderDrawing> visuals = barWithLine.GetTimeHolders();
            Point mousePos = e.GetPosition(barWithLine.MyVisualHost);
            var nowClicked = visuals.FilterByHitTest<TimeHolderDrawing, DrawingVisual>(mousePos);
            var prevSelected = visuals.Where(v => v.TimeHolder.IsSelected).ToList();

            if (!RoutingCommands.SelectMeasures.IsCurrentAction())
                prevSelected.ButNotIn(nowClicked).ForEach(v => v.Redraw(false, BarWithLine.Scale));

            nowClicked.ButNotIn(prevSelected).ToList().ForEach(v => v.Redraw(true, BarWithLine.Scale));
            if (!RoutingCommands.SelectMeasures.IsCurrentAction())
                SelectedBarsCollection.UnSelectExceptOf(barWithLine);
            SelectedBarsCollection.Add(barWithLine);
        }
    }


}
