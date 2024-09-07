using NotationHelper.Controls;
using NotationHelper.MVVM;
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

namespace NotationHelper.Views
{
    /// <summary>
    /// Logika interakcji dla klasy VCellContainerCtrl.xaml
    /// </summary>
    public partial class VCellContainerCtrl : UserControl
    {
        public VCellContainerCtrl()
        {
            InitializeComponent();
            this.DataContextChanged += VCellContainerCtrl_DataContextChanged;
        }

        private void VCellContainerCtrl_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            DataContext = e.NewValue;
            if (DataContext is SingleBar_VM barVm) 
            {
                SetSth(barVm);
            }
        }

        public void SetSth(SingleBar_VM bar_VM)
        {
            var nCols = MyStackGrid.ColumnDefinitions.Count();
            MyStackGrid.Children.Clear();
            MyStackGrid.ColumnDefinitions.Clear();
            for (int i = 0; i < bar_VM.Cells.Count; i++)
            {
                var vertical = new RhythmCellCtrl();
                vertical.DataContext = bar_VM.Cells[i];
                MyStackGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });
                Grid.SetColumn(vertical, i);
                MyStackGrid.Children.Add(vertical);
            }
        }
    }
}
