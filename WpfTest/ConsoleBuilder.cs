using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;

namespace WpfTest
{
    public static class ConsoleBuilder
    {
        public static FlowDocument Test2(int nCols, int nRows)
        {
            FlowDocument doc = new FlowDocument();
            Paragraph paragraph = new Paragraph();
            for (int i = 0;i < nRows; i++)
            {
                for (int j = 0;j < nCols; j++)
                {
                    Span hoverableText = new Span(new Run("X"));
                    hoverableText.Background = Brushes.Blue;
                    hoverableText.MouseEnter += (sender, e) =>
                    {
                        hoverableText.Background = Brushes.Yellow; // Change to yellow on hover
                    };

                    hoverableText.MouseLeave += (sender, e) =>
                    {
                        hoverableText.Background = Brushes.Transparent; // Revert background when mouse leaves
                    };
                    paragraph.Inlines.Add(hoverableText);
                }
                paragraph.Inlines.Add("\n");
            }
            doc.Blocks.Add(paragraph);
            return doc;
        }

        public static FlowDocument Test()
        {
            FlowDocument doc = new FlowDocument();
            doc.FontFamily = new FontFamily("Consolas"); 
            doc.Background = Brushes.Transparent;
            doc.Foreground = Brushes.White;
            doc.FontWeight = FontWeights.Regular;
            Paragraph paragraph = new Paragraph();
            paragraph.Inlines.Add("Hello world");
            paragraph.Inlines.Add("[CLOSE Hello CLICK]");
            paragraph.Inlines.Add("\n");
            paragraph.Inlines.Add("Hello world");
            paragraph.Inlines.Add("[CLOSE Hello CLICK]".ToHyperlink(new RelayCommand(() => { Application.Current.Shutdown(); })));
            doc.Blocks.Add(paragraph);
            return doc;
        }
        public static Hyperlink ToHyperlink(this string text, ICommand cmd)
        {
            var hyperlink = new Hyperlink(new Run(text))
            {
                FontWeight = FontWeights.Bold,
                Background = Brushes.Transparent,
                Foreground = Brushes.White,  // Set hyperlink text to black
                TextDecorations = null       // Remove underline
            }; ;
            hyperlink.Command = cmd;
            hyperlink.PreviewMouseDown += (sender, e) =>
            {
                if (hyperlink.Command != null && hyperlink.Command.CanExecute(null))
                {
                    hyperlink.Command.Execute(null);
                    e.Handled = true;  // Prevents further handling (Ctrl+Click behavior)
                }
            };

            return hyperlink;
        }

        private static void Hyperlink_Click1(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("test");
        }

    }
}
