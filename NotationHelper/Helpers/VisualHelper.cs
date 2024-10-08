﻿using MusicDataModel.MusicViews;
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
using System.Windows.Media;
using System.Windows;
using MusicDataModel.MusicViews.MusicViews.MusicControls;
using MusicDataModel.MusicViews.MusicControls;

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

        public static SingleBar_VM CalculateXOffsets(this SingleBar_VM barVM, double totalVisualWidth)
        {
            double prevLen = 0;
            foreach (var timeGroup in barVM.VoiceBar.Children)
            {
                timeGroup.MOffset = prevLen;
                prevLen += timeGroup.Duration.GetLen();
                
            }

            var scaleUp = totalVisualWidth/barVM.GetLen();
            foreach(var timeGroup in barVM.VoiceBar.Children)
            {
                timeGroup.XOffset = scaleUp * timeGroup.MOffset;
                timeGroup.VisualDuration = scaleUp * timeGroup.Duration.GetLen();
            }

            return barVM;
        }

        public static int NoteToVisualHeight(this TimeHolder th)
        {
            if (th is not Note n)
                return 0;
            return n.NoteToVisualHeight();
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

        public static bool HitTest<T>(this Visual vis, Point point) where T : class
        {
            var hitTest = VisualTreeHelper.HitTest(vis, point);
            if(hitTest == null || hitTest.VisualHit == null) 
                return false;
            var res = hitTest.VisualHit is T;
            return res;
        }

        public static List<T1> FilterByHitTest<T1, T2>(this IEnumerable<T1> vis, Point point) where T1 : Visual where T2 : class
        {
            var res = vis.Where(v => v.HitTest<T2>(point)).ToList();
            return res;
        }
    }
}
