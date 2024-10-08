﻿using MusicDataModel.MidiModel;
using System;
using System.Collections.Generic;
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

namespace NAudioTest.Views
{
    /// <summary>
    /// Logika interakcji dla klasy RhythmHolderView.xaml
    /// </summary>
    public partial class RhythmHolderView : UserControl
    {
        public RhythmHolderView()
        {
            InitializeComponent();
        }

        public RhythmHolder RhythmHolder { get; set; }

        public void SetSelected()
        {
            this.Background = new SolidColorBrush(Colors.Black);
        }

        public void SetUnselected()
        {
            this.Background = new SolidColorBrush(Colors.White);
        }
    }
}
