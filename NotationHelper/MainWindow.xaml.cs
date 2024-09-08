using NotationHelper.DataModel.Piece;
using NotationHelper.FlowTypes;
using NotationHelper.Helpers;
using NotationHelper.MVVM.MainVM;
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

namespace NotationHelper
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private bool lockLayout = true;
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
            MyMulticolumnView.RecalculateLayout();
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
            MyMulticolumnView.RecalculateLayout();
        }

        private void GridSplitter_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            MyMulticolumnView.RecalculateLayout();

        }

        private void GridSplitter_SizeChanged(object sender, SizeChangedEventArgs e)
        {

        }

        private void GridSplitter_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {

            MyMulticolumnView.RecalculateLayout();
        }

        private void MainWindowControl_LocationChanged(object sender, EventArgs e)
        {
            if (lockLayout)
                return;
            MyMulticolumnView.RecalculateLayout();
        }

        private void MainWindowControl_StateChanged(object sender, EventArgs e)
        {
            if (lockLayout)
                return;
            MyMulticolumnView.RecalculateLayout();
        }

        private void MainWindowControl_SizeChanged_1(object sender, SizeChangedEventArgs e)
        {

        }
    }
}