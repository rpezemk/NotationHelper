using Microsoft.VisualBasic.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NAudioTest.AudioInfo
{

    public class AudioParameters
    {
        public AudioParameters(int rate, int nChannels, int bitDepth)
        {
            SampleRate = rate;
            NChannels = nChannels;
            BitDepth = bitDepth;
        }
        public int NChannels;
        public int SampleRate;
        public int BitDepth;

        public override string ToString()
        {
            return $"{SampleRate} Hz, {NChannels} channels, {BitDepth} bits";
        }
    }

}
