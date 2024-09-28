using MusicDataModel.DataModel.Piece;
using MusicDataModel.Helpers;
using MusicDataModel.MVVM;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
namespace MusicDataModel.MusicViews.MusicControls
{
    /// <summary>
    /// Logika interakcji dla klasy BarWithLine.xaml
    /// </summary>
    public partial class BarWithLine : UserControl
    {
        public List<TimeHolderDrawing> SelectedDrawingList { get; set; } = new List<TimeHolderDrawing>();
        public BarWithLine()
        {
            InitializeComponent();
        }

        public double Scale = 3;

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            MyVisualHost.Clear();
            Draw();
        }

        private void Draw()
        {
            var sum = GridContainer.ActualWidth + ActualWidth + MyVisualHost.ActualHeight;
            if (sum == 0 || DataContext is not SingleBar_VM vm)
                return;

            vm.CalculateMOffsets().CalculateXOffset(GridContainer.ActualWidth);
            foreach (var rtmCell in vm.VoiceBar.Children)
            {
                DrawTimeGroup(rtmCell);
            }
        }

        private void DrawTimeGroup(TimeHolder timeGroup)
        {
            DrawGlyph(timeGroup, Brushes.White);
        }

        private TimeHolderDrawing DrawGlyph(TimeHolder timeHolder, Brush brush)
        {
            var xOffset = timeHolder.XOffset;
            var glyph = timeHolder.ToGlyph();
            var yOffset = timeHolder.YOffset - (timeHolder.NoteToVisualHeight() - 3) * Scale;
            TimeHolderDrawing textVisual = new TimeHolderDrawing(timeHolder);
            using (DrawingContext dc = textVisual.RenderOpen())
            {
                DrawNormal(glyph, xOffset, yOffset, dc, brush);
            }
            MyVisualHost.AddVisual(textVisual);
            return textVisual;
        }

        private void DrawNormal(string glyph, double xOffset, double yOffset, DrawingContext dc, Brush brush)
        {
            FormattedText text = new FormattedText(
               glyph,
               System.Globalization.CultureInfo.InvariantCulture,
               FlowDirection.LeftToRight,
               new Typeface(FontHelper.BravuraFont, FontHelper.BravuraStyle, new FontWeight() { }, new FontStretch() { }),
               20, // Font size
               brush,
               VisualTreeHelper.GetDpi(this).PixelsPerDip
            );
            dc.DrawText(text, new Point(xOffset, yOffset - 17));
        }


        private void MyVisualHost_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            var enu = MyVisualHost.Visuals.GetEnumerator();
            List<Visual> visuals = new List<Visual>();
            while (enu.MoveNext())
            {
                var abc = enu.Current;
                visuals.Add(abc);
            }
            var cnt = visuals.Count();

            var testCnt = 0;
            Point mousePosition2 = e.GetPosition(MyVisualHost);
            var resVisuals = visuals.Where(v => v is TimeHolderDrawing)
                .Select(vis => (vis, VisualTreeHelper.HitTest(vis, mousePosition2)))
                .Where(res => res.Item2 != null && res.Item2.VisualHit is DrawingVisual)
                .Select(t => t.vis);
            foreach (var sel in SelectedDrawingList)
            {
                if (sel is not TimeHolderDrawing nd)
                    continue;
                RedrawUnselected(nd);
            }
            SelectedDrawingList.Clear();
            foreach (var vis in resVisuals)
            {
                if (vis is TimeHolderDrawing nd)
                {
                    RedrawSelected(nd);
                    SelectedDrawingList.Add(nd);
                }
            }

        }

        private void RedrawSelected(TimeHolderDrawing nd)
        {
            MyVisualHost.RemoveVisual(nd);
            DrawGlyph(nd.TimeHolder, Brushes.Red);
        }
        private void RedrawUnselected(TimeHolderDrawing nd)
        {
            MyVisualHost.RemoveVisual(nd);
            DrawGlyph(nd.TimeHolder, Brushes.White);
        }

    }

    public class TimeHolderDrawing : DrawingVisual
    {
        public TimeHolderDrawing(TimeHolder timeHolder)
        {
            TimeHolder = timeHolder;
        }

        public TimeHolder TimeHolder { get; set; }
    }
}
