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

namespace NotationHelper.Controls
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

        public void ShowNBars(int n)
        {
            for (int i = 0; i < n; i++) 
            { 
                BarWithLine barWithLine = new BarWithLine();
                MyMulticolumnGrid.ColumnDefinitions.Add(new ColumnDefinition());
                Grid.SetColumn(barWithLine, i);
                MyMulticolumnGrid.Children.Add(barWithLine);
            }
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            var dt = DataContext;
        }

        private void UserControl_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {

            var dt = DataContext;
        }
    }
}
