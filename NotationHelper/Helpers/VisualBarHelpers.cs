using MusicDataModel.DataModel.Piece;
using MusicDataModel.MusicViews.MusicControls;
using MusicDataModel.MusicViews.MusicViews.MusicControls;
using NotationHelper.MVC.Basics;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace MusicDataModel.Helpers
{
    internal static class VisualBarHelpers
    {

        public static TimeHolderDrawing DrawGlyph(this DrawingVisualHost host, TimeHolder timeHolder, Brush brush, double scale)
        {
            var xOffset = timeHolder.XOffset + (timeHolder is Note ? 0 : 3);
            var visHeignt = timeHolder.NoteToVisualHeight() - 3;
            var musicTxt = timeHolder.ToSMUFL().GetMusicText(brush);
            var yOffset =
                timeHolder is Note ? timeHolder.YOffset - visHeignt * scale
                : timeHolder is Rest ? -13
                : 0;
            var textVisual = host.DrawMusicText(musicTxt, xOffset, yOffset, brush, timeHolder);
            return textVisual;
        }

        public static TimeHolderDrawing DrawMusicText(this DrawingVisualHost host, FormattedText musicTxt, double xOffset, double yOffset, Brush brush, TimeHolder timeHolder)
        {
            TimeHolderDrawing textVisual = new TimeHolderDrawing(timeHolder, host);
            DrawingContext dc = textVisual.RenderOpen();
            dc.DrawText(musicTxt, new Point(xOffset, yOffset - 29));
            dc.Close();
            host.AddVisual(textVisual);
            return textVisual;
        }

        public static void DrawTimeGroup(this DrawingVisualHost host, TimeHolder timeGroup, double scale)
        {
            host.DrawGlyph(timeGroup, Brushes.LightGray, scale);
        }

        public static FormattedText GetMusicText(this string glyph, Brush brush)
        {
            FormattedText text = new FormattedText(
            glyph,
            System.Globalization.CultureInfo.InvariantCulture,
            FlowDirection.LeftToRight, GetFont(), 25, brush, 1);
            return text;
        }

        private static Typeface GetFont()
        {
            return new Typeface(FontHelper.BravuraFont, FontHelper.BravuraStyle, new FontWeight() { }, new FontStretch() { });
        }

        public static void BarWithLineMouseDown(this BarWithLine barWithLine, MouseButtonEventArgs e)
        {
            List<TimeHolderDrawing> visuals = barWithLine.GetTimeHolders();
            Point mousePos = e.GetPosition(barWithLine.MyVisualHost);
            var nowClicked = visuals.FilterByHitTest<TimeHolderDrawing, DrawingVisual>(mousePos);
            var prevSelected = visuals.Where(v => v.TimeHolder.IsSelected).ToList();

            var c = true;// !RoutingCommands.SelectMeasures.IsCurrentAction();
            if (c)
                prevSelected.ButNotIn(nowClicked).ForEach(v => v.Redraw(false, BarWithLine.Scale));

            nowClicked.ButNotIn(prevSelected).ToList().ForEach(v => v.Redraw(true, BarWithLine.Scale));
            if (c)
                SelectedBarsCollection.UnSelectExceptOf(barWithLine);
            SelectedBarsCollection.Add(barWithLine);
        }
    }
}