using NotationHelper.Converters;
using NotationHelper.DataModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace NotationHelper.Controls
{
    /// <summary>
    /// Logika interakcji dla klasy GeneralLayout.xaml
    /// </summary>
    public partial class GeneralLayout : UserControl
    {
        public GeneralLayout()
        {
            InitializeComponent();
        }

        private DrawingVisualHost drawingHost;
        public void AddControls(List<VerticalBarContainer> verticalBarContainters)
        {
            var nCols = MyStackGrid.ColumnDefinitions.Count();
            for (int i = 0; i < verticalBarContainters.Count; i++)
            {
                var vertical = verticalBarContainters[i];
                MyStackGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });
                Grid.SetColumn(vertical, i);
                MyStackGrid.Children.Add(vertical);
            }
        }

        public void ClearControls()
        {
            MyStackGrid.Children.Clear();
            MyStackGrid.ColumnDefinitions.Clear();
        }


        public void ShowNotes(List<Note> notes)
        {

            foreach (var note in notes)
            {
                Ellipse myRectangle = new Ellipse
                {
                    Width = 5,
                    Height = 5,
                    Fill = VisualConverters.FromHex("#99000000") // Brushes.Blue
                };

                // Positioning the Rectangle on the Canvas
                Canvas.SetLeft(myRectangle, note.X);
                Canvas.SetTop(myRectangle, note.Y);

                // Adding the Rectangle to the Canvas
                MyNoteCanvas.Children.Add(myRectangle);
            }



            //if (MyNoteCanvas.Children.Count == 0)
            //    MyNoteCanvas.Children.Add(drawingHost);
        }

        public void ShowNotes1(List<Note> notes)
        {
            if (drawingHost == null)
                drawingHost = new DrawingVisualHost();
            var drawingVisual = new DrawingVisual();
            using (var dc = drawingVisual.RenderOpen())
            {
                foreach (var note in notes)
                {
                    dc.DrawRectangle(Brushes.Blue, null, new Rect(note.X, note.Y, 5, 5));
                }

                drawingHost.AddVisual(drawingVisual);
            }

            if (MyNoteCanvas.Children.Count == 0)
                MyNoteCanvas.Children.Add(drawingHost);
        }

        public void ClearNotes()
        {
            MyNoteCanvas.Children.Clear();
        }
    }


}
