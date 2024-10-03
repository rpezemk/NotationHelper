using NotationHelper.MVC;
using System.Windows.Controls;
using System.Windows.Input;

namespace MusicDataModel.MusicViews.MainViews
{
    public partial class MusicPage : UserControl
    {
        #region XAML ACTIONS
        private void Grid_KeyDown(object sender, KeyEventArgs e)
        {
            CommandResolver.ReportInput(this, e);
        }

        private void Grid_KeyUp(object sender, KeyEventArgs e)
        {
        }
        #endregion
    }
}
