using MusicDataModel.Helpers;
using MusicDataModel.MusicViews.MusicControls;
using MusicDataModel.MVVM.MusicVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

namespace MusicDataModel.MusicViews
{
    /// <summary>
    /// Logika interakcji dla klasy HLayout.xaml
    /// </summary>
    public partial class HLayout : UserControl
    {
        public HLayout()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            ReloadVMs();
        }

        private void UserControl_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            ReloadVMs();
        }

        private void ReloadVMs()
        {
            if (DataContext is not HContent_VM hContentVM)
                return;
            MyStackPanel.SubdivideHorizontal(MyMulticolumnGrid.ActualWidth, hContentVM.SingleBar_VMs.ToList(), new BarWithLine());
        }
    }
}
