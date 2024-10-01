using NotationHelper.MVC;
using System.Windows;
using System.Windows.Input;

namespace MusicDataModel
{
    public partial class MainWindow : Window
    {
        #region XAML ACTIONS
        private void MainWindowControl_LocationChanged(object sender, EventArgs e)
        {
            if (lockLayout)
                return;
            MyMulticolumnView.RecalculateLayout();
        }
        private void MainWindowControl_KeyDown(object sender, KeyEventArgs e)
        {
            RoutingCommands.ReportInput(this, e);
        }

        private void MainWindowControl_KeyUp(object sender, KeyEventArgs e)
        {
            RoutingCommands.ReportInput(this, e);
        }
        #endregion
    }
}