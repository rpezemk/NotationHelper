using MusicDataModel.MVVM.Base;
using System.Collections.ObjectModel;

namespace MusicDataModel.MVVM.MainVM
{
    public class ToolBar_VM : ViewModelBase
    {
        public ToolBar_VM()
        {
            foreach(var tb in ProgramSettings.GetToolbarItems())
            {
                _toolbarItems.Add(tb);
            }
        }

        private ObservableCollection<ToolbarItem_VM> _toolbarItems = new ObservableCollection<ToolbarItem_VM>();
        public ObservableCollection<ToolbarItem_VM> ToolbarItems { get { return _toolbarItems; } set { _toolbarItems = value; OnPropertyChanged(); } }
    }
}
