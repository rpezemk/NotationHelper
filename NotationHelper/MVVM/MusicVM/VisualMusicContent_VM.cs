using NotationHelper.DataModel.Range;
using NotationHelper.MVVM.Base;
using System.Collections.ObjectModel;

namespace NotationHelper.MVVM.MusicVM
{
    public class VisualMusicContent_VM : ViewModelBase
    {
        public VisualMusicContent_VM() { }
        public VisualMusicContent_VM(MatrixRange matrixRange) 
        {
            foreach (var item in matrixRange.Parts)
            {
                partContent_VMs.Add(new HContent_VM(item.Bars));
            }
        }
        private ObservableCollection<HContent_VM> partContent_VMs = new ObservableCollection<HContent_VM>();
        public ObservableCollection<HContent_VM> PartContent_VMs { get { return partContent_VMs; } set { partContent_VMs = value; OnPropertyChanged(); } }
    }

}
