﻿using MusicDataModel.DataModel.Piece;
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

        private void DrawAll()
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
            DrawGlyph(timeGroup, Brushes.LightGray);
        }

        private TimeHolderDrawing DrawGlyph(TimeHolder timeHolder, Brush brush)
        {
            var xOffset = timeHolder.XOffset;
            var glyph = timeHolder.ToGlyph();
            var yOffset = timeHolder.YOffset - (timeHolder.NoteToVisualHeight() - 3) * Scale;
            var textVisual =  DrawNormal(glyph, xOffset, yOffset, brush, timeHolder);
            return textVisual;
        }

        private TimeHolderDrawing DrawNormal(string glyph, double xOffset, double yOffset, Brush brush, TimeHolder timeHolder)
        {
            TimeHolderDrawing textVisual = new TimeHolderDrawing(timeHolder);
            using (DrawingContext dc = textVisual.RenderOpen())
            {
                FormattedText text = new FormattedText(
               glyph,
               System.Globalization.CultureInfo.InvariantCulture,
               FlowDirection.LeftToRight,
               new Typeface(FontHelper.BravuraFont, FontHelper.BravuraStyle, new FontWeight() { }, new FontStretch() { }),
               25, // Font size
               brush,
               VisualTreeHelper.GetDpi(this).PixelsPerDip
            );
                MyVisualHost.AddVisual(textVisual);
                dc.DrawText(text, new Point(xOffset, yOffset - 29));
            }
            return textVisual;
        }

        private bool noteWasClicked;
        private void MarkForAMoment()
        {
            noteWasClicked = true;
            Task.Run(() => { Thread.Sleep(50); noteWasClicked = false; });
        }
        private void MyVisualHost_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            MarkForAMoment();
            List<TimeHolderDrawing> visuals = GetTimeHolders();
            var cnt = visuals.Count();

            var testCnt = 0;
            var allSelected = visuals.Select(v => v as TimeHolderDrawing).Where(v => v is not null)
                .Where(v => v.IsSelected).ToList();
            allSelected.ForEach(v => v.IsSelected = false);


            Point mousePosition2 = e.GetPosition(MyVisualHost);
            var nowClicked = visuals.Where(v => v is TimeHolderDrawing)
                .Select(vis => (vis, VisualTreeHelper.HitTest(vis, mousePosition2)))
                .Where(res => res.Item2 != null && res.Item2.VisualHit is DrawingVisual)
                .Select(t => t.vis as TimeHolderDrawing).ToList();

            nowClicked.ForEach(v => v.IsSelected = true);

            foreach (var vis in visuals)
            {
                if (vis.IsSelected)
                    RedrawSelected(vis);
                else
                    RedrawUnselected(vis);
            }
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

        private void RedrawSelected(TimeHolderDrawing nd)
        {
            MyVisualHost.RemoveVisual(nd);
            DrawGlyph(nd.TimeHolder, Brushes.Red);
        }
        private void RedrawUnselected(TimeHolderDrawing nd)
        {
            MyVisualHost.RemoveVisual(nd);
            DrawGlyph(nd.TimeHolder, Brushes.LightGray);
        }

        private void DrawingCanvas_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (noteWasClicked == true)
                return;
            foreach (var th in GetTimeHolders())
            {
                RedrawSelected(th);
            }
        }
    }
}
