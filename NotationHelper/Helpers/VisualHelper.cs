using MusicDataModel.MusicViews;
using MusicDataModel.MVVM.Base;
using MusicDataModel.MusicViews.MainViews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using MusicDataModel.MVVM;
using System.Runtime.CompilerServices;
using MusicDataModel.DataModel.Elementary;
using MusicDataModel.DataModel.Piece;

namespace MusicDataModel.Helpers
{
    public static class VisualHelper
    {
        public static void SubdivideLikeGrid<Tvm, TControl>(this StackPanel panel, double externalWidth, double gridHeight, 
            double subHeight, List<Tvm> values, TControl dummyGeneric) 
            where Tvm : ViewModelBase where TControl : UserControl, new()
        {
            var nPartsPerSide = (int)Math.Floor(gridHeight / (subHeight));
            values.ToList().DivideSet(nPartsPerSide, out var vmGroups, out var nResCount);
            var columnWidth = externalWidth / nResCount;

            foreach (var vmGroup in vmGroups)
            {
                StackPanel subPanel = new StackPanel() { Width = columnWidth };
                foreach (var vm in vmGroup)
                {
                    TControl subControl = new TControl();
                    subControl.DataContext = vm;
                    subPanel.Children.Add(subControl);
                }
                panel.Children.Add(subPanel);
            }
        }

        public static void SubdivideHorizontal<Tvm, TControl>
            (this StackPanel panel, 
            double externalWidth, List<Tvm> values, 
            TControl dummGeneric)
            where Tvm : ViewModelBase 
            where TControl : UserControl, new()
        {
            var columnWidth = externalWidth / values.Count();
            panel.Orientation = Orientation.Horizontal;
            foreach (var vm in values)
            {
                TControl subControl = new TControl() { Width = columnWidth };
                subControl.DataContext = vm;
                panel.Children.Add(subControl);
            }
        }

        public static SingleBar_VM CalculateMOffsets(this SingleBar_VM barVM)
        {
            double prevLen = 0;
            foreach(var timeGroup in barVM.VoiceBar.Children)
            {
                timeGroup.MOffset = prevLen;
                prevLen += timeGroup.Duration.GetLen();
            }
            return barVM;
        }

        public static SingleBar_VM CalculateXOffset(this SingleBar_VM barVM, double totalVisualWidth)
        {
            var scaleUp = totalVisualWidth/barVM.GetLen();
            foreach(var timeGroup in barVM.VoiceBar.Children)
            {
                timeGroup.XOffset = scaleUp * timeGroup.MOffset;
            }

            return barVM;
        }

        public static int NoteToVisualHeight(this Note note)
        {
            var namePart = note.Pitch.BaseNoteName.NoteToVisualHeight();
            var octPart = (note.Pitch.OctaveNo - 5) * 7;
            return namePart + octPart;
        }

        public static int NoteToVisualHeight(this NoteName noteName)
        {
            var res = noteName switch
            {
                NoteName.C => 0,
                NoteName.D => 1,
                NoteName.E => 2,
                NoteName.F => 3,
                NoteName.G => 4,
                NoteName.A => 5,
                NoteName.B => 6,
            };
            return res;
        }
    }
}
