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

            KeyboardRouting.ReportInput();


            GetTimeHolders().ForEach(th => th.Redraw(true, Scale));
            var c = true; //!RoutingCommands.SelectMeasures.IsCurrentAction()
            if (c)
                SelectedBarsCollection.UnSelectExceptOf(this);
            SelectedBarsCollection.Add(this);
        }

        private void MyVisualHost_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            MarkForAMoment();
            this.BarWithLineMouseDown(e);
            KeyboardRouting.ReportInput();
        }

        private void MyVisualHost_MouseUp(object sender, MouseButtonEventArgs e)
        {
            KeyboardRouting.ReportInput();
        }

        #endregion
    }
}
