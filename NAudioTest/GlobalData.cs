using NAudioTest.Logger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NAudioTest
{
    public static class GlobalData
    {
        /// <summary>
        /// Stereo
        /// </summary>
        public static readonly int NChannels = 2;
        public static int SampleRate = 44100;
        public static MyLogger Logger = new MyLogger();


    }
}
