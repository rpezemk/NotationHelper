using NotationHelper.MVVM.MusicVM;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace NotationHelper.MusicViews.MainViews
{
    /// <summary>
    /// Logika interakcji dla klasy MusicPage.xaml
    /// </summary>
    public partial class MusicPage : UserControl
    {
        public MusicPage()
        {
            InitializeComponent();
            HorizontalParts = new ObservableCollection<HContent_VM>();
        }



        public ObservableCollection<HContent_VM> HorizontalParts
        {
            get { return (ObservableCollection<HContent_VM>)GetValue(MyPropertyProperty); }
            set { SetValue(MyPropertyProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MyPropertyProperty =
            DependencyProperty.Register("HorizontalParts", typeof(ObservableCollection<HContent_VM>), typeof(MusicPage), new PropertyMetadata(default));


    }
}
