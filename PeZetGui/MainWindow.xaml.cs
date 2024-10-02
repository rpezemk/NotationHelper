using PeZetGui.Helpers;
using PeZetGui.TuiControls.MidiObjects;
using System.Numerics;
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

namespace PeZetGui
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

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Reload();

            //VisualHelper.SubdivideHorizontal
        }

        private void Reload()
        {
            MidiStackPanel.Children.Clear();
            var w = ActualWidth;
            var h = ActualHeight;
            var laneH = new MidiLaneControl().Height;

            var nLanes = h / laneH;
            Random rand = new Random();
            for (var laneNo = 0; laneNo < nLanes; laneNo++)
            {
                var lane = new MidiLaneControl();
                var currOffset = 0;
                currOffset += 10 + rand.Next(50); ;
                var prevOffset = 0;
                while (currOffset < w)
                {
                    lane.MidiLaneStackPanel.Children.Add(new NoteObjectControl() { Width = currOffset - prevOffset });
                    prevOffset = currOffset;
                    currOffset += 10 + rand.Next(50); ;
                }

                MidiStackPanel.Children.Add(lane);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Reload();
        }
    }
}