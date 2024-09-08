using NotationHelper.DataModel.Piece;
using NotationHelper.MVVM.Base;
using System.Collections.ObjectModel;

namespace NotationHelper.MVVM.MusicVM
{
    public class HContent_VM : ViewModelBase
    {
        public HContent_VM()
        {

        }

        public HContent_VM(List<VoiceBar> voiceBars)
        {
            foreach (VoiceBar voiceBar in voiceBars)
            {
                singleBars.Add(new SingleBar_VM(voiceBar));
            }
        }

        private ObservableCollection<SingleBar_VM> singleBars = new ObservableCollection<SingleBar_VM>();
        public ObservableCollection<SingleBar_VM> SingleBar_VMs { get { return singleBars; } set { singleBars = value; OnPropertyChanged(); } }
    }
}
