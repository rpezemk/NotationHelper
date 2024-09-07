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

            var gridHeight = MultiColumnStackPanel.ActualHeight;
            var hLayoutHeight = new HLayout().Height;
            var fullWidth = MainGrid.ActualWidth;
            var nPartsPerSide = (int)Math.Floor(gridHeight / (hLayoutHeight));
            visualMusicContent.PartContent_VMs.ToList().DivideSet(nPartsPerSide, out var partGroups, out var nResCount);
            var columnWidth = fullWidth / nResCount;

            int groupId = 0;
            foreach (var partGroup in partGroups)
            {
                MusicPage musicPage = new MusicPage() { Width = columnWidth };
                foreach (var part in partGroup)
                {
                    musicPage.HorizontalParts.Add(part);
                }
                MultiColumnStackPanel.Children.Add(musicPage);
                groupId++;
            }

        }

        public void FillBasic(PieceMatrix matrix, int startBarNo, int barCount)
        {
            ClearMain();
            RecalculateLayout();
        }

        public void ClearMain()
        {
            MultiColumnStackPanel.Children.Clear();
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
