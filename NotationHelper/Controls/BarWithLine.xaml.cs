using NotationHelper.Helpers;
using NotationHelper.MVVM;
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

namespace NotationHelper.Controls
{
    /// <summary>
    /// Logika interakcji dla klasy BarWithLine.xaml
    /// </summary>
    public partial class BarWithLine : UserControl
    {
        public BarWithLine()
        {
            InitializeComponent();
        }
        private void ReloadVMs()
        {
            if (DataContext is not SingleBar_VM hContentVM)
                return;
            //MyStackPanel.SubdivideHorizontal(GridContainer.ActualWidth, hContentVM..ToList(), new BarWithLine());
        }
    }
}
