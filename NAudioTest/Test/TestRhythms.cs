using MusicDataModel.DataModel.Piece;
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
        public static Part GetSamplePart()
        {
            var barCount = 2;
            var partNo = 0;
            var part = new Part() { PartNo = partNo };
            for (int barNo = 0; barNo < barCount; barNo++)
            {
                var bar = new VoiceBar() { };
                bar.AppendChild(Note.C().Sharp().Eight().AsTimeGroup());
                bar.AppendChild(Note.D().Flat().Sixteen().AsTimeGroup());
                bar.AppendChild(Note.E().Sharp().Sixteen().AsTimeGroup());
                bar.AppendChild(Note.F().Flat().Eight().AsTimeGroup());
                bar.AppendChild(Note.G().Flat().Sixteen().AsTimeGroup());
                bar.AppendChild(Note.A().Flat().Sixteen().AsTimeGroup());
                bar.AppendChild(Note.B().Flat().Quarter().AsTimeGroup());
                bar.AppendChild(Note.C().UpOct().Flat().Quarter().AsTimeGroup());
                part.Children.Add(bar);
            }
            return part;
        }

        public static RhythmSequence GetSampleSequence()
        {
            var seq = GetSamplePart().ToSequence();
            return seq;
        }
    }
}
