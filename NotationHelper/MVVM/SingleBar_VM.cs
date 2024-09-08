using NotationHelper.DataModel.Elementary;
using NotationHelper.DataModel.Piece.Parts.Bar;
using NotationHelper.DataModel.Piece.Parts.Bar.Timegroups;
using NotationHelper.MVVM.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotationHelper.MVVM
{
    public class SingleBar_VM : ViewModelBase
    {
        public SingleBar_VM() 
        {
            VoiceBar = BlankEmits.SampleEmitter.GetSampleVoiceBar();
            FillCells();    
        }

        public SingleBar_VM(VoiceBar voiceBar)
        {
            VoiceBar = voiceBar;
            FillCells();
        }

        private void FillCells()
        {
            foreach (var timeGroup in VoiceBar.Timegroups)
            {
                Cells.Add(new RhythmCell_VM(timeGroup));
            }
        }

        private VoiceBar voiceBar;
        public VoiceBar VoiceBar {  get { return voiceBar; } set {  voiceBar = value; OnPropertyChanged(); } }
        public Meter Meter => voiceBar.Meter;
        
        public ObservableCollection<RhythmCell_VM> Cells = new ObservableCollection<RhythmCell_VM>();
    }

    public class RhythmCell_VM : ViewModelBase
    {
        public RhythmCell_VM(TimeGroup timeGroup)
        {

        }
    }
}
