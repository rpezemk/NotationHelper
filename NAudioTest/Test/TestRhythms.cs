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
        public static MonoPart GetSamplePart()
        {
            var barCount = 4;
            var partNo = 0;
            var part = new MonoPart() { PartNo = partNo };
            for (int barNo = 0; barNo < barCount; barNo++)
            {
                var bar = new VoiceBar() { };
                bar.AppendChild(Note.C().Sharp().Eight());
                bar.AppendChild(Note.D().Flat().Sixteen());
                bar.AppendChild(Note.E().Sharp().Sixteen());
                bar.AppendChild(Note.F().Flat().Eight().AsTimeGroup());
                bar.AppendChild(Note.G().Flat().Sixteen());
                bar.AppendChild(Note.A().Flat().Sixteen());
                bar.AppendChild(Note.B().Flat().Quarter());
                bar.AppendChild(Note.C().UpOct().Flat().Quarter());
                part.Children.Add(bar);
            }
            return part;
        }
    }
}
