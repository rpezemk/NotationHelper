using MusicDataModel.DataModel.Piece;
using System.Windows.Media;
namespace MusicDataModel.MusicViews.MusicControls
{
    public class TimeHolderDrawing : DrawingVisual
    {
        public bool IsSelected {  get; set; }
        public TimeHolderDrawing(TimeHolder timeHolder)
        {
            TimeHolder = timeHolder;
        }

        public TimeHolder TimeHolder { get; set; }
    }
}
