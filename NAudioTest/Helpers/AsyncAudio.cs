using NAudio.Wave;
using NAudioTest.Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NAudioTest.Helpers
{
    public static class AsyncAudio
    {
        public static EternalSampleProvider Provider;
        public static WaveOutEvent WaveOutEvent;
        public static void Init()
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
