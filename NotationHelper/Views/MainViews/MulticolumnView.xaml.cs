using NotationHelper.Controls;
using NotationHelper.DataModel.Elementary;
using NotationHelper.Helpers;
using NotationHelper.MVVM.MusicVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace NotationHelper.Views.MainViews
{
    /// <summary>
    /// Logika interakcji dla klasy MulticolumnView.xaml
    /// </summary>
    public partial class MulticolumnView : UserControl
    {
        public MulticolumnView()
        {
            InitializeComponent();
            var dt = DataContext;
        }

        private void UserControl_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            FillBasic(new PieceMatrix(16, 30), 0, 4);
            var dt = DataContext;
        }


        public void RecalculateLayout()
        {
            if (DataContext is not VisualMusicContent_VM visualMusicContent)
                return;
            MultiColumnGrid.SubdivideLikeGrid(MainGrid.ActualWidth, MultiColumnGrid.ActualHeight, new HLayout().Height, visualMusicContent.PartContent_VMs.ToList(), new HLayout());
        }

        public void FillBasic(PieceMatrix matrix, int startBarNo, int barCount)
        {
            ClearMain();
            RecalculateLayout();
        }

        public void ClearMain()
        {
            MultiColumnGrid.Children.Clear();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            FillBasic(new PieceMatrix(16, 30), 0, 4);
        }

        private void MultiColumnDataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            var dt = DataContext;
        }
    }
}
