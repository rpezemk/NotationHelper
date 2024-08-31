using NotationHelper.DataModel.Elementary;
using NotationHelper.FlowTypes;
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

        private void MainWindowControl_Loaded(object sender, RoutedEventArgs e)
        {
            Program.FillBasic(this, pieceMatrix, 0, 4);
            var virtualMenu = Program.GetMainMenu();
            MenuHelper.CreateMenuGUI(MainMenu, virtualMenu);
        }

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            Program.ClearMain(this);
        }

        private void FillButton_Click(object sender, RoutedEventArgs e)
        {
            Program.FillBasic(this, pieceMatrix, 0, 4);
        }

        PieceMatrix pieceMatrix = new DataModel.Elementary.PieceMatrix(30, 4);

        private void MainWindowControl_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            //Program.FillBasic(this, pieceMatrix, 0, 4);
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
            Program.FillBasic(this, pieceMatrix, 0, 4);
        }

        private void Grid_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            Program.FillBasic(this, pieceMatrix, 0, 4);
        }
    }
}