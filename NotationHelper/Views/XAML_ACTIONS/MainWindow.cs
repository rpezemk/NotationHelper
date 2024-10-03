using MusicDataModel.DataModel.Piece;
using NotationHelper.Commands;
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
            CommandResolver.ReportInput();
            //if (e.Key == Key.Delete)
            //{
            //    foreach (var bar in SelectedBarsCollection.BarsWithSelectedNotes)
            //    {
            //        foreach (var th in bar.GetTimeHolders())
            //        {
            //            if (th.TimeHolder is Note note)
            //                CommandContainer.RemoveNote(note);
            //        };
            //    }
            //}
        }

        private void MainWindowControl_KeyUp(object sender, KeyEventArgs e)
        {

        }
        #endregion
    }
}