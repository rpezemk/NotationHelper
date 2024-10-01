using MusicDataModel.DataModel.Piece;
using MusicDataModel.Helpers;
using MusicDataModel.MusicViews.MusicControls;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace NotationHelper.MVC
{

    public static class OperationBindings
    {
        public static List<TimeHolder> SelectedTimeHolders = new List<TimeHolder>();
        public static List<BarWithLine> BarsWithSelectedNotes = new List<BarWithLine>();


        public static void AnyEvent(params object[] objects)
        {
            var action = AllRoutings.Instance.AllActions.Where(a => a.CanAccept(objects));
            if(action.Any())
            {
                foreach(var a in action)
                {
                    a.RunAction(objects);
                }
                return;
            }

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
            if (RoutingCommands.SelectMeasures.IsCurrentAction() == false)
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

        public static void BarWithLineMouseDown(BarWithLine barWithLine, MouseButtonEventArgs e)
        {
            List<TimeHolderDrawing> visuals = barWithLine.GetTimeHolders();
            Point mousePosition2 = e.GetPosition(barWithLine.MyVisualHost);
            var nowClicked = visuals.Where(v => v is TimeHolderDrawing)
                .Select(vis => (vis, VisualTreeHelper.HitTest(vis, mousePosition2)))
                .Where(res => res.Item2 != null && res.Item2.VisualHit is DrawingVisual)
                .Select(t => t.vis).ToList();

            var prevSelected = visuals.Select(v => v).Where(v => v is not null)
                .Where(v => v.TimeHolder.IsSelected).ToList();
            if (!RoutingCommands.SelectMeasures.IsCurrentAction())
                prevSelected.Where(ps => !nowClicked.Contains(ps)).ToList().ForEach(v => barWithLine.RedrawUnselected(v));
            nowClicked.Where(nc => !prevSelected.Contains(nc)).ToList().ForEach(v => barWithLine.RedrawSelected(v));
            UnSelectOtherBars(barWithLine);
        }
        public static FormattedText GetFormattedText(UserControl userControl, string glyph, Brush brush)
        {
            FormattedText text = new FormattedText(
            glyph,
            System.Globalization.CultureInfo.InvariantCulture,
            FlowDirection.LeftToRight,
            new Typeface(FontHelper.BravuraFont, FontHelper.BravuraStyle, new FontWeight() { }, new FontStretch() { }),
            25, // Font size
            brush,
            VisualTreeHelper.GetDpi(userControl).PixelsPerDip);
            return text;
        }
    }

    
}
