using MusicDataModel.DataModel.Piece;
using MusicDataModel.Helpers;
using MusicDataModel.MusicViews.MusicViews.MusicControls;
using MusicDataModel.MVVM;
using NotationHelper.MVC;
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

        public double Scale = 3;

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

            vm.CalculateMOffsets().CalculateXOffset(GridContainer.ActualWidth);
            foreach (var timeHolder in vm.VoiceBar.Children)
            {
                DrawTimeGroup(this, MyVisualHost, timeHolder, Scale);
            }
        }

        public static void DrawTimeGroup(UserControl control, DrawingVisualHost host, TimeHolder timeGroup, double scale)
        {
            DrawGlyph(control, host, timeGroup, Brushes.LightGray, scale);
        }

        public static TimeHolderDrawing DrawGlyph(UserControl control, DrawingVisualHost host, TimeHolder timeHolder, Brush brush, double scale)
        {
            var xOffset = timeHolder.XOffset + (timeHolder is Note ? 0: 3);
            var visHeignt = timeHolder.NoteToVisualHeight() - 3;
            var glyph = timeHolder.ToGlyph();
            var yOffset = 
                timeHolder is Note? timeHolder.YOffset - visHeignt * scale
                : timeHolder is Rest? -13 
                : 0;
            var textVisual = DrawNormal(host, glyph, xOffset, yOffset, brush, timeHolder);
            return textVisual;
        }

        public static TimeHolderDrawing DrawNormal(DrawingVisualHost host, string glyph, double xOffset, double yOffset, Brush brush, TimeHolder timeHolder)
        {
            TimeHolderDrawing textVisual = new TimeHolderDrawing(timeHolder);
            DrawingContext dc = textVisual.RenderOpen();
            FormattedText text = OperationBindings.GetFormattedText(glyph, brush);
            dc.DrawText(text, new Point(xOffset, yOffset - 29));
            dc.Close();
            host.AddVisual(textVisual);
            return textVisual;
        }

        public bool noteWasClicked;
        public void MarkForAMoment()
        {
            noteWasClicked = true;
            Task.Run(() => { Thread.Sleep(50); noteWasClicked = false; });
        }

        private void MyVisualHost_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            MarkForAMoment();
            OperationBindings.BarWithLineMouseDown(this, e);
        }



        public List<TimeHolderDrawing> GetTimeHolders()
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

        public void RedrawSelected(TimeHolderDrawing nd)
        {
            MyVisualHost.RemoveVisual(nd);
            nd.TimeHolder.IsSelected = true;
            DrawGlyph(this, MyVisualHost, nd.TimeHolder, Brushes.Red, Scale);
        }

        public void RedrawUnselected(TimeHolderDrawing nd)
        {
            MyVisualHost.RemoveVisual(nd);
            nd.TimeHolder.IsSelected = false;
            DrawGlyph(this, MyVisualHost, nd.TimeHolder, Brushes.LightGray, Scale);
        }

        private void DrawingCanvas_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (noteWasClicked == true)
                return;
            foreach (var th in GetTimeHolders())
            {
                RedrawSelected(th);
            }

            OperationBindings.UnSelectOtherBars(this);
        }
    }
}
