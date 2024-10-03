using MusicDataModel.DataModel.Piece;
using MusicDataModel.Helpers;
using MusicDataModel.MusicViews.MusicViews.MusicControls;
using System.Windows.Media;
namespace MusicDataModel.MusicViews.MusicControls
{
    public class TimeHolderDrawing : DrawingVisual
    {
        public TimeHolderDrawing(TimeHolder timeHolder, BarWithLine barWithLine)
        {
            TimeHolder = timeHolder;
            Host = barWithLine.MyVisualHost;
            BarWithLine = barWithLine;
        }
        public DrawingVisualHost Host { get; set; }
        public TimeHolder TimeHolder { get; set; }
        public BarWithLine BarWithLine { get; set; }
        public void RemoveSelf()
        {
            Host.RemoveVisual(this);
        }
        public void Draw(Brush brush, double scale)
        {
            BarWithLine.DrawGlyph(this.TimeHolder, brush, scale);
        }

        public void Redraw(bool selected, double scale)
        {
            RemoveSelf();
            TimeHolder.IsSelected = selected;
            Draw(selected? Brushes.Red: Brushes.White, scale);
        }
    }
}
