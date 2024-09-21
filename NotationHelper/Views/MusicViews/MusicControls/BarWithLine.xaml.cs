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
using NotationHelper.DataModel.Elementary;
using NotationHelper.Helpers;
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

        public double Scale = 2.4;

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

        private void DrawTimeGroup(TimeGroup timeGroup)
        {
            if (timeGroup is VNoteGroup vNoteGroup)
                DrawNoteGroup(vNoteGroup);
            else if (timeGroup is Rest rest)
                DrawRest(rest);
        }

        private void DrawNoteGroup(VNoteGroup vNoteGroup) 
        {
            foreach(var note in vNoteGroup.Notes)
            {
                DrawNote(note);
            }
                   
        }

        private void DrawNote(Note note)
        {
            var glyph = note.ToGlyph();
            var xOffset = note.Parent.XOffset;
            var yOffset = note.Parent.YOffset - (note.NoteToVisualHeight() - 3) * Scale; 
            DrawGlyph(glyph, xOffset, yOffset);
        }

        private void DrawRest(Rest rest)
        {
            DrawGlyph(rest.ToGlyph(), rest.XOffset, rest.YOffset);
        }

        private void DrawGlyph(string glyph, double xOffset, double yOffset)
        {
            DrawingVisual textVisual = new DrawingVisual();
            using (DrawingContext dc = textVisual.RenderOpen())
            {
                FormattedText text = new FormattedText(
                   glyph,
                   System.Globalization.CultureInfo.InvariantCulture,
                   FlowDirection.LeftToRight,
                   new Typeface(FontHelper.BravuraFont, FontHelper.BravuraStyle, new FontWeight() { }, new FontStretch() { }),
                   17, // Font size
                   Brushes.White,
                   VisualTreeHelper.GetDpi(this).PixelsPerDip
                );
                dc.DrawText(text, new Point(xOffset, yOffset - 17));
            }
            MyVisualHost.AddVisual(textVisual);
        }



    }
}
