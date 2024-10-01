using MusicDataModel.DataModel.Piece;
using MusicDataModel.MusicViews.MusicViews.MusicControls;
using System.Windows.Media;
namespace MusicDataModel.MusicViews.MusicControls
{
    public class TimeHolderDrawing : DrawingVisual
    {
        public TimeHolderDrawing(TimeHolder timeHolder, DrawingVisualHost host)
        {
            TimeHolder = timeHolder;
            Host = host;
        }
        public DrawingVisualHost Host { get; set; }
        public TimeHolder TimeHolder { get; set; }
    }
}
