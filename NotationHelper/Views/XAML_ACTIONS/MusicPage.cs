using NotationHelper.MVC.Basics;
using System.Windows.Controls;
using System.Windows.Input;

namespace MusicDataModel.MusicViews.MainViews
{
    public partial class MusicPage : UserControl
    {
        #region XAML ACTIONS
        private void Grid_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
                SelectedBarsCollection.UnSelectAll();
        }

        private void Grid_KeyUp(object sender, KeyEventArgs e)
        {

        }
        #endregion
    }
}
