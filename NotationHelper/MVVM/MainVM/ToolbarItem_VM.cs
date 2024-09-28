using MusicDataModel.MVVM.Base;

namespace MusicDataModel.MVVM.MainVM
{
    public class ToolbarItem_VM: ViewModelBase
    {
        public string Description { get; set; }
        public Action NonArgAction { get; set; }
        public Action<object> ObjArgAction { get; set; }
        public ToolbarItem_VM() { }
        public ToolbarItem_VM(string desc, Action action) 
        {
            Description = desc;
            NonArgAction = action;
        }
        public ToolbarItem_VM(string desc, Action<object> action) 
        {
            Description = desc;
            ObjArgAction = action;
        }
    }
}
