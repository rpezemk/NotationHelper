using MusicDataModel.MVVM.MainVM;
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

namespace MusicDataModel.MusicViews.MainViews
{
    /// <summary>
    /// Logika interakcji dla klasy ToolbarView.xaml
    /// </summary>
    public partial class ToolbarView : UserControl
    {
        public ToolbarView()
        {
            InitializeComponent();
        }



        public ObservableCollection<ToolbarItem_VM> ToolbarItems
        {
            get { return (ObservableCollection<ToolbarItem_VM>)GetValue(ToolbarItemsProperty); }
            set { SetValue(ToolbarItemsProperty, value); }
        }

        public static readonly DependencyProperty ToolbarItemsProperty =
            DependencyProperty.Register("ToolbarItems", typeof(ObservableCollection<ToolbarItem_VM>), typeof(ToolbarView), new PropertyMetadata(default));
    }
}
