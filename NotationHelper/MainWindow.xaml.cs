using NotationHelper.DataModel.Elementary;
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
            //Program.FillBasic(this);
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
            Program.FillBasic(this, pieceMatrix, 0, 4);
        }
    }
}