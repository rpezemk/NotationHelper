using NotationHelper.Helpers;
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

namespace NotationHelper.MusicViews.Tools
{
    /// <summary>
    /// Logika interakcji dla klasy ToolBarItem.xaml
    /// </summary>
    public partial class ToolBarItem : UserControl
    {
        public ToolBarItem()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            TestGlyph(MyTextBlock);
        }

        private static void TestGlyph(TextBlock textBlock)
        {
            textBlock.FontFamily = FontHelper.BravuraFont;
            textBlock.Text = ConstGlyphs.G_Clef;// Notehead_Half;
            
            textBlock.FontSize = 14;
        }
    }
}
