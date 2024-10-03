using MusicDataModel.Helpers;
using NotationHelper.MVC;
using System.Windows.Controls;
using System.Windows.Input;
namespace MusicDataModel.MusicViews.MusicControls
{

    public partial class BarWithLine : UserControl
    {
        #region XAML ACTIONS
        private void DrawingCanvas_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (noteWasClicked == true)
                return;

            CommandResolver.ReportInput();


            GetTimeHolderDrawings().ForEach(th => th.Redraw(true, Scale));
            var c = true; 
            if (c)
                MainWindow.GetBarWithLines().Where(b => b != this)
                .ForEach(a => a.GetTimeHolderDrawings()
                                .Where(th => th.TimeHolder.IsSelected)
                                .ForEach(th => th.Redraw(false, BarWithLine.Scale)));
        }

        private void MyVisualHost_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            MarkForAMoment();
            this.BarWithLineMouseDown(e);
            CommandResolver.ReportInput();
        }


        #endregion
    }
}
