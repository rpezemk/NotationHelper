using MusicDataModel.MVVM.Base;
using System.Collections.ObjectModel;

namespace MusicDataModel.MVVM.MainVM
{
    public class ToolBar_VM : ViewModelBase
    {
        public ToolBar_VM()
        {
            for (int i = 0; i < 30; i++)
            {
                _toolbarItems.Add(new ToolbarItem_VM());
            }
        }

        private ObservableCollection<ToolbarItem_VM> _toolbarItems = new ObservableCollection<ToolbarItem_VM>();
        public ObservableCollection<ToolbarItem_VM> ToolbarItems { get { return _toolbarItems; } set { _toolbarItems = value; OnPropertyChanged(); } }
    }
}
