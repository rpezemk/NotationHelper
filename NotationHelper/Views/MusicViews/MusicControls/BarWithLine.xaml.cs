using NotationHelper.MusicViews.MusicViews.MusicControls;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NotationHelper.Helpers;
using NotationHelper.DataModel.Piece;
using NotationHelper.Views.MusicViews;
using NotationHelper.MVVM;

namespace NotationHelper.MusicViews.MusicControls
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

        private List<RhythmCell_VM> _cells = new List<RhythmCell_VM>();

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            MyVisualHost.Clear();
            Draw();
        }

        private void MyVisualHost_Loaded(object sender, RoutedEventArgs e)
        {
            //Draw();
        }

        private void Draw()
        {
            var sum = GridContainer.ActualWidth + ActualWidth + MyVisualHost.ActualHeight;
            if (sum == 0)
                return;
            var dt = DataContext;
            if (dt is not SingleBar_VM vm)
                return;
            var cnt = vm.Cells.Count;
            var step = GridContainer.ActualWidth / cnt;
            var curr = 0.0D;

            foreach (var item in vm.Cells)
            {
                if (item == null || item.TimeGroup == null)
                    continue;
                VisualNote vn = new VisualNote(new Note() { }) { Weight = 10 };


                DrawingVisual textVisual = new DrawingVisual();
                using (DrawingContext dc = textVisual.RenderOpen())
                {

                    FormattedText text = new FormattedText(
                       ConstGlyphs.SixteenthNote,
                       System.Globalization.CultureInfo.InvariantCulture,
                       FlowDirection.LeftToRight,
                       new Typeface(FontHelper.BravuraFont, FontHelper.BravuraStyle, new FontWeight() { }, new FontStretch() { }),
                       17, // Font size
                       Brushes.Black,
                       VisualTreeHelper.GetDpi(this).PixelsPerDip // DPI setting for clarity
                    );
                    dc.DrawText(text, new Point(curr, -17));
                }

                // Add the visual to the visualHost
                MyVisualHost.AddVisual(textVisual);
                curr += step;
            }
        }
        private void Rectangle_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            
        }
    }
}
