using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NAudioTest.AudioInfo
{
    public static class AudioInfoHelper
    {
        public static string TranslateToReadable(SupportedWaveFormat format)
        {
            var asInt = (int)format;
            (int rate, int nChannels, int bitDepth) pars = asInt switch
            {
                //      11.025 kHz, Mono, 8-bit
                1 => (11025, 1, 8),
                //      11.025 kHz, Stereo, 8-bit
                2 => (11025, 2, 8),
                //      11.025 kHz, Mono, 16-bit
                4 => (11025, 1, 16),
                //      11.025 kHz, Stereo, 16-bit
                8 => (11025, 2, 16),
                //      22.05 kHz, Mono, 8-bit
                0x10 => (22050, 1, 8),
                //      22.05 kHz, Stereo, 8-bit
                0x20 => (22050, 2, 8),
                //      22.05 kHz, Mono, 16-bit
                0x40 => (22050, 1, 16),
                //      22.05 kHz, Stereo, 16-bit
                0x80 => (22050, 2, 16),
                //      44.1 kHz, Mono, 8-bit
                0x100 => (44100, 1, 8),
                //      44.1 kHz, Stereo, 8-bit
                0x200 => (44100, 2, 8),
                //      44.1 kHz, Mono, 16-bit
                0x400 => (44100, 1, 16),
                //      44.1 kHz, Stereo, 16-bit
                0x800 => (44100, 2, 16),
                //      48 kHz, Mono, 8-bit
                0x1000 => (48000, 1, 8),
                //      48 kHz, Stereo, 8-bit
                0x2000 => (48000, 2, 8),
                //      48 kHz, Mono, 16-bit
                0x4000 => (48000, 1, 16),
                //      48 kHz, Stereo, 16-bit
                0x8000 => (48000, 2, 16),
                //      96 kHz, Mono, 8-bit
                0x10000 => (96000, 1, 8),
                //     96 kHz, Stereo, 8-bit
                0x20000 => (96000, 2, 8),
                //     96 kHz, Mono, 16-bit
                0x40000 => (96000, 1, 16),
                //    96 kHz, Stereo, 16-bit
                0x80000 => (96000, 2, 16),
                _ => throw new Exception("non-managed format!"),
            };

            return new AudioParameters(pars.rate, pars.nChannels, pars.bitDepth).ToString();
        }
    }
}
