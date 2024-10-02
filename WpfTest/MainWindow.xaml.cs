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

namespace WpfTest
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

        private void Hyperlink_Click1(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("You clicked part 2!");
            //e.Handled = true;  // Prevents default Ctrl+Click behavior
        }

        private void Hyperlink_Click2(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("You clicked part 2!");
            //e.Handled = true;  // Prevents default Ctrl+Click behavior
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private (int nCols, int nRows) CalculateRowsAndColumns()
        {
            // Create a DrawingVisual for measuring purposes
            DrawingVisual visual = new DrawingVisual();
            using (DrawingContext drawingContext = visual.RenderOpen())
            {
                // Measure the width and height of a single character (like 'W')
                FormattedText formattedText = new FormattedText(
                    "WWWWWWWWWW",
                    System.Globalization.CultureInfo.CurrentCulture,
                    FlowDirection.LeftToRight,
                    new Typeface("Consolas"),
                    14,
                    Brushes.Black,
                    new NumberSubstitution(),
                    VisualTreeHelper.GetDpi(visual).PixelsPerDip
                );

                double charWidth = formattedText.WidthIncludingTrailingWhitespace;
                double charHeight = formattedText.Height;

                // Get the actual size of the RichTextBox
                double richTextBoxWidth = MyGrid.ActualWidth;
                double richTextBoxHeight = MyGrid.ActualHeight;

                // Calculate how many columns and rows fit in the RichTextBox
                int ncolumns = (int)(richTextBoxWidth / charWidth) * 10;
                int nrows = (int)(richTextBoxHeight / charHeight);

                return (ncolumns, nrows);
            }
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            var t = CalculateRowsAndColumns();
            mouseMaskRichTextbox.Document = ConsoleBuilder.Test2(t.nCols, t.nRows);
            //richTextBox.Document = ConsoleBuilder.Test();
        }
    }
}