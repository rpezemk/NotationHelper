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
            RhythmHolderViews = new List<RhythmHolderView>();
            SequencePanel.Children.Clear();
            foreach (var rh in sequence.RhythmHolders)
            {
                var rWid = rh.Duration * w / totalTime;
                var rhv = new RhythmHolderView() { Width = rWid, RhythmHolder = rh };
                SequencePanel.Children.Add(rhv);
                RhythmHolderViews.Add(rhv);
            }
        }

        public List<RhythmHolderView> RhythmHolderViews { get; set; } = new List<RhythmHolderView>();

        public void UnSelectAll()
        {
            foreach (var rh in RhythmHolderViews)
            {
                rh.SetUnselected();
            }
        }

        public void SelectOne(RhythmHolder rhythmHolder)
        {
            var find = RhythmHolderViews.Where(rhv => rhv.RhythmHolder != null).FirstOrDefault(rhv => rhv.RhythmHolder == rhythmHolder);
            if (find == null)
                return;

            find.SetSelected();
        }

    }
}
