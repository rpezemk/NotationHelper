using MusicDataModel.DataModel.Piece;
using MusicDataModel.Helpers;
using MusicDataModel.MusicViews.MusicViews.MusicControls;
using MusicDataModel.MVVM;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
namespace MusicDataModel.MusicViews.MusicControls
{
    /// <summary>
    /// Logika interakcji dla klasy BarWithLine.xaml
    /// </summary>
    public partial class BarWithLine : UserControl
    {

        public BarWithLine()
        {
            InitializeComponent();
        } 

        public static double Scale = 3;

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            MyVisualHost.Clear();
            DrawAll();
        }

        public void DrawAll()
        {
            var sum = GridContainer.ActualWidth + ActualWidth + MyVisualHost.ActualHeight;
            if (sum == 0 || DataContext is not SingleBar_VM vm)
                return;

            vm.CalculateXOffsets(GridContainer.ActualWidth);
            foreach (var timeHolder in vm.VoiceBar.Children)
            {
                this.DrawGlyph(timeHolder, Brushes.White, Scale);
            }
             
            foreach (var timeHolder in vm.VoiceBar.Children)
            {
                if (timeHolder is Note note && note.IsTied && note.GetNextTimeHolder() is Note n2 && n2.Pitch.ResultPitch == note.Pitch.ResultPitch)
                    this.DrawTie(timeHolder);
            }
        }

        public bool noteWasClicked;
        public void MarkForAMoment()
        {
            noteWasClicked = true;
            Task.Run(() => { Thread.Sleep(50); noteWasClicked = false; });
        }


        public List<TimeHolderDrawing> GetTimeHolderDrawings()
        {
            var enu = MyVisualHost.Visuals.GetEnumerator();
            List<TimeHolderDrawing> visuals = new List<TimeHolderDrawing>();
            while (enu.MoveNext())
            {
                var abc = enu.Current;
                if (abc is TimeHolderDrawing thd)
                    visuals.Add(thd);
            }

            return visuals;
        }


        public void DrawTie(Note note)
        {

        }
    }
}
