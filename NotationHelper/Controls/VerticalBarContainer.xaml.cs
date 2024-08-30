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
    /// Logika interakcji dla klasy VerticalBarContainer.xaml
    /// </summary>
    public partial class VerticalBarContainer : UserControl
    {
        public VerticalBarContainer()
        {
            InitializeComponent();
        }

        public void ClearView()
        {
            this.MyStackPanel.Children.Clear();
        }
        public void AddBarControl(BarControl barControl)
        {
            this.MyStackPanel.Children.Add(barControl);
        }
    }
}
