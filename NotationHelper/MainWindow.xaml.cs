using MusicDataModel.DataModel.Piece;
using MusicDataModel.DataModel.Range;
using MusicDataModel.FlowTypes;
using MusicDataModel.Helpers;
using MusicDataModel.MusicViews;
using MusicDataModel.MusicViews.MusicControls;
using MusicDataModel.MVVM.MainVM;
using NotationHelper.MVC;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MusicDataModel
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static MainWindow Instance;
        public MainWindow()
        {
            Instance = this;
            InitializeComponent();
        }

        private bool lockLayout = true;

        public PieceMatrix PieceMatrix { get; internal set; }
        public MatrixRange MatrixRange { get; internal set; }

        public static List<BarWithLine> GetBarWithLines()
        {
            var res = Instance.MyMulticolumnView.MultiColumnGrid.Children.OfType<StackPanel>()
                .SelectMany(sp => sp.Children.OfType<HLayout>())
                .SelectMany(hl => hl.MyStackPanel.Children.OfType<BarWithLine>()).ToList();


            return res;
        }

        public static List<TimeHolderDrawing> GetTimeHolderDrawings()
        {
            var res = GetBarWithLines().SelectMany(b => b.GetTimeHolderDrawings()).ToList();
            return res;
        }

        private void MainWindowControl_Loaded(object sender, RoutedEventArgs e)
        {
            lockLayout = false;
        }

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void FillButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void MainWindowControl_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            Refresh();
        }

        private void MinimizeButton_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void MaximizeButton_Click(object sender, RoutedEventArgs e)
        {
            if (this.WindowState == WindowState.Maximized)
                this.WindowState = WindowState.Normal;
            else if (this.WindowState == WindowState.Normal)
                this.WindowState = WindowState.Maximized;
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void GridSplitter_TouchMove(object sender, TouchEventArgs e)
        {

        }

        private void Grid_SizeChanged(object sender, SizeChangedEventArgs e)
        {

        }

        private void GridSplitter_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Refresh();
        }

        private void GridSplitter_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Refresh();
        }

        private void GridSplitter_SizeChanged(object sender, SizeChangedEventArgs e)
        {

        }

        private void GridSplitter_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            Refresh();
        }


        private void MainWindowControl_StateChanged(object sender, EventArgs e)
        {
            if (lockLayout)
                return;
            Refresh();
        }

        public void Refresh()
        {
            MyMulticolumnView.RecalculateLayout();
        }

        private void MainWindowControl_SizeChanged_1(object sender, SizeChangedEventArgs e)
        {

        }

        private void PlayBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void StopBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void PageBackBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void MeasureBackBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void MeasureForwardBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void PageForwardBtn_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}