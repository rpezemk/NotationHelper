using NAudio.Wave;
using NAudioTest.Providers.UglyMess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NAudioTest.Helpers
{
    public static class AsyncAudio
    {
        public static EternalSampleProvider Provider { get; set; }
        public static WaveOutEvent WaveOutEvent { get; set; }
        public static void InitOnce()
        {
            WaveOutEvent = new WaveOutEvent();

            if (Provider == null)
            {
                Provider = new EternalSampleProvider();
                WaveOutEvent.Init(Provider);
                WaveOutEvent.Play();
                while (WaveOutEvent.PlaybackState == PlaybackState.Playing)
                {
                    Thread.Sleep(1000);
                }
            }
        }

        public static void Stop()
        {
            if (WaveOutEvent != null)
            {
                WaveOutEvent.Stop();
            }
        }

    }
}
