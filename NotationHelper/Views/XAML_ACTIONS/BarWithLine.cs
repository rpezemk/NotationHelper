using MusicDataModel.Helpers;
using NotationHelper.MVC;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
namespace MusicDataModel.MusicViews.MusicControls
{

    public partial class BarWithLine : UserControl
    {
        #region XAML ACTIONS
        private void DrawingCanvas_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            List<TimeHolderDrawing> visuals = this.GetTimeHolderDrawings();
            Point mousePos = e.GetPosition(this.MyVisualHost);
            var nowClicked = visuals.FilterByHitTest<TimeHolderDrawing, DrawingVisual>(mousePos);
            var prevSelected = visuals.Where(v => v.TimeHolder.IsSelected).ToList();

            if (nowClicked != null && nowClicked.Count > 0)
            {
                CommandResolver.ResolveKeyboardInput(nowClicked);
            }
            else
            {
                CommandResolver.ResolveKeyboardInput(this);
            }
        }

        #endregion
    }
}
