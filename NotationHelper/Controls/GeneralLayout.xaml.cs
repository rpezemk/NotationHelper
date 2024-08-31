using NotationHelper.Converters;
using NotationHelper.DataModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
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

        private DrawingVisualHost drawingHost;
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
    }


}
