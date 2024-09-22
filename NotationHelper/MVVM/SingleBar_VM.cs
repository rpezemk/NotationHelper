using MusicDataModel.DataModel.Elementary;
using MusicDataModel.DataModel.Piece;
using MusicDataModel.MVVM.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicDataModel.MVVM
{
    public class SingleBar_VM : ViewModelBase
    {
        public SingleBar_VM(VoiceBar voiceBar)
        {
            VoiceBar = voiceBar;
        }

        private VoiceBar voiceBar;
        public VoiceBar VoiceBar {  get { return voiceBar; } set {  voiceBar = value; OnPropertyChanged(); } }
        public Meter Meter => voiceBar.Meter;
        public double GetLen() => voiceBar.Children.Select(t => t.Duration.GetLen()).Sum();
    }}
