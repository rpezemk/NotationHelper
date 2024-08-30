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
    /// Logika interakcji dla klasy GeneralLayout.xaml
    /// </summary>
    public partial class GeneralLayout : UserControl
    {
        public GeneralLayout()
        {
            InitializeComponent();
        }

        public void AddControls(List<VerticalBarContainer> verticalBarContainters)
        {
            var nCols = MyStackGrid.ColumnDefinitions.Count();
            for (int i = 0; i < verticalBarContainters.Count; i++)
            {
                var vertical = verticalBarContainters[i];
                MyStackGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });
                Grid.SetColumn(vertical, i);
                MyStackGrid.Children.Add(vertical);
            }
        }

        public void AddControl(VerticalBarContainer verticalBarContainter)
        {
            MyStackGrid.Children.Add(verticalBarContainter);
        }

        public void ClearBarControl()
        {
            MyStackGrid.Children.Clear();
            MyStackGrid.ColumnDefinitions.Clear();
        }
    }


}
