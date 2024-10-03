using MusicDataModel.Helpers;
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
                var c = true;
                if (c)
                    prevSelected.ButNotIn(nowClicked).ForEach(v => v.Redraw(false, BarWithLine.Scale));

                nowClicked.ButNotIn(prevSelected).ToList().ForEach(v => v.Redraw(true, BarWithLine.Scale));
                if (c)
                    MainWindow.GetTimeHolderDrawings().Where(b => b.BarWithLine != this)
                                    .Where(th => th.TimeHolder.IsSelected)
                                    .ForEach(th => th.Redraw(false, BarWithLine.Scale));
            }
            else
            {
                GetTimeHolderDrawings().ForEach(th => th.Redraw(true, Scale));
                var c = true;
                if (c)
                    MainWindow.GetTimeHolderDrawings().Where(b => b.BarWithLine != this)
                                    .Where(th => th.TimeHolder.IsSelected)
                                    .ForEach(th => th.Redraw(false, BarWithLine.Scale));
            }
        }

        #endregion
    }
}
