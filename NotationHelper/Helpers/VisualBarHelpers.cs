using MusicDataModel.DataModel.Piece;
using MusicDataModel.MusicViews.MusicControls;
using MusicDataModel.MusicViews.MusicViews.MusicControls;
using NotationHelper.MVC;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using static System.Formats.Asn1.AsnWriter;

namespace MusicDataModel.Helpers
{
    internal static class VisualBarHelpers
    {
        public static TimeHolderDrawing DrawGlyph(this BarWithLine host, TimeHolder timeHolder, Brush brush, double scale)
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

        public static TimeHolderDrawing DrawMusicText(this BarWithLine barWithLine, FormattedText musicTxt, double xOffset, double yOffset, Brush brush, TimeHolder timeHolder)
        {
            TimeHolderDrawing textVisual = new TimeHolderDrawing(timeHolder, barWithLine);
            DrawingContext dc = textVisual.RenderOpen();
            dc.DrawText(musicTxt, new Point(xOffset, yOffset - 29));
            dc.Close();
            barWithLine.MyVisualHost.AddVisual(textVisual);
            return textVisual;
        }

        public static void DrawTie(this BarWithLine host, TimeHolder timeHolder, double x2Corr = 0)
        {
            var visHeignt = timeHolder.NoteToVisualHeight() - 3;
            var yOffset =
                timeHolder is Note ? timeHolder.YOffset - visHeignt * BarWithLine.Scale + 27
                : timeHolder is Rest ? -13
                : 0;
            host.DrawBow(timeHolder.XOffset, timeHolder.XOffset + timeHolder.VisualDuration + x2Corr, yOffset);
        }


        public static void DrawBow(this BarWithLine host, double x1, double x2, double y1)
        {
            DrawingVisual drawingVisual = new DrawingVisual();
            using (DrawingContext drawingContext = drawingVisual.RenderOpen())
            {
                var x1Corr = 4;
                var x2Corr = -1;
                var bowDepth = Math.Min(20, (x2-x1)/2);
                var startPt = new Point(x1 + x1Corr, y1);
                var c1 = new Point(x1 + (x2 - x1) * 0.20 + x1Corr, y1 + bowDepth);
                var c2 = new Point(x2 - (x2 - x1) * 0.20 + x2Corr, y1 + bowDepth);

                var c3 = new Point(x2 - (x2 - x1) * 0.12 + x2Corr, y1 + bowDepth + 3);
                var c4 = new Point(x1 + (x2 - x1) * 0.12 + x1Corr, y1 + bowDepth + 3); 
                
                var endPt = new Point(x2 + x2Corr, y1);

                var pathFigure = new PathFigure();
                pathFigure.StartPoint = startPt;
                pathFigure.Segments.Add(new BezierSegment(c1, c2, endPt, true));
                pathFigure.Segments.Add(new BezierSegment(c3, c4, startPt, true));


                pathFigure.IsClosed = true;
                var path = new PathGeometry();
                path.Figures.Add(pathFigure);
                drawingContext.DrawGeometry(Brushes.White, new Pen(Brushes.Transparent, 1), path);
                host.MyVisualHost.AddVisual(drawingVisual);
            }

            // Add the DrawingVisual to the Canvas
            return;
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

    }
}