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

namespace NotationHelper.MusicViews
{
    /// <summary>
    /// Logika interakcji dla klasy NoteControl.xaml
    /// </summary>
    public partial class NoteControl : UserControl
    {
        public NoteControl()
        {
            InitializeComponent();
            Diameter = 5;
        }
        public double Diameter
        {
            get { return (double)GetValue(DiameterProperty); }
            set { SetValue(DiameterProperty, value); }
        }

        public static readonly DependencyProperty DiameterProperty =
            DependencyProperty.Register("Diameter", typeof(double), typeof(NoteControl), new PropertyMetadata(default));

        public int X;
        public int Y;
    }
}
