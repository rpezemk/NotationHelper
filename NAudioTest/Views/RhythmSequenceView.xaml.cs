using MusicDataModel.MidiModel;
using NAudioTest.Test;
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

namespace NAudioTest.Views
{
    /// <summary>
    /// Logika interakcji dla klasy RhythmSequenceView.xaml
    /// </summary>
    public partial class RhythmSequenceView : UserControl
    {
        public RhythmSequenceView()
        {
            InitializeComponent();
        }

        private void MyRhythmSequenceView_Loaded(object sender, RoutedEventArgs e)
        {

        }

        public void ShowSequence(RhythmSequence sequence)
        {
            var w = TimeGrid.ActualWidth;
            var totalTime = sequence.RhythmHolders.Sum(r => r.Duration);
            foreach(var rh in sequence.RhythmHolders)
            {
                var rWid = rh.Duration * w / totalTime;
                SequencePanel.Children.Add(new RhythmHolderView() { Width = rWid });
            }
        }


    }
}
