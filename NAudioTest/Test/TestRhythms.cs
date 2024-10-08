﻿using MusicDataModel.DataModel.Piece;
using MusicDataModel.MidiModel;
using MusicDataModel.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NAudioTest.Test
{
    public static class TestRhythms
    {
        public static MonoPart GetSamplePart()
        {
            var barCount = 1;
            var partNo = 0;
            var part = new MonoPart() { PartNo = partNo };
            for (int barNo = 0; barNo < barCount; barNo++)
            {
                var bar = new VoiceBar() { };
                bar.AppendChild(Note.D().Sixteen());
                bar.AppendChild(Note.D().Sixteen());
                bar.AppendChild(Note.D().Sixteen());
                bar.AppendChild(Note.D().Sixteen());
                bar.AppendChild(Note.D().Sixteen());
                bar.AppendChild(Note.D().Sixteen());
                bar.AppendChild(Note.D().Sixteen());
                bar.AppendChild(Note.D().Sixteen());
                bar.AppendChild(Note.D().Sixteen());
                bar.AppendChild(Note.D().Sixteen());
                bar.AppendChild(Note.D().Sixteen());
                bar.AppendChild(Note.D().Sixteen());
                bar.AppendChild(Note.D().Sixteen());
                bar.AppendChild(Note.D().Sixteen());
                bar.AppendChild(Note.D().Sixteen());
                bar.AppendChild(Note.D().Sixteen());

                part.Children.Add(bar);
            }
            return part;
        }
    }
}
