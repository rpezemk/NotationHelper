using MusicDataModel.MusicViews;
using MusicDataModel.DataModel.Elementary;
using MusicDataModel.Helpers;
using MusicDataModel.MVVM.MusicVM;
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

namespace MusicDataModel.MusicViews.MainViews
{
    /// <summary>
    /// Logika interakcji dla klasy MulticolumnView.xaml
    /// </summary>
    public partial class MulticolumnView : UserControl
    {
        public MulticolumnView()
        {
            InitializeComponent();
        }

        private void UserControl_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            //FillBasic(new PieceMatrix(16, 30), 0, 4);
        }


        public void RecalculateLayout()
        {
            MultiColumnGrid.Children.Clear();
            if (DataContext is not VisualMusicContent_VM visualMusicContent)
                return;
            MultiColumnGrid.SubdivideLikeGrid(MainGrid.ActualWidth, MultiColumnGrid.ActualHeight, new HLayout().Height, visualMusicContent.PartContent_VMs.ToList(), new HLayout());
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void MultiColumnDataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
        }
    }
}
