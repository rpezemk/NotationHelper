using NAudioTest.Helpers;
using NAudioTest.Providers;
using NAudioTest.TimeThings;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NAudioTest.Test
{
    public static class OldTests
    {
        //private static void GetDevicesInfo_Click(object sender, RoutedEventArgs e)
        //{
        //    for (int deviceNumber = 0; deviceNumber < WaveOut.DeviceCount; deviceNumber++)
        //    {
        //        // Get the capabilities of each output device
        //        var capabilities = WaveOut.GetCapabilities(deviceNumber);
        //        GlobalData.Logger.Log($"Device {deviceNumber}: {capabilities.ProductName}");
        //        var values = Enum.GetValues(typeof(SupportedWaveFormat)).Cast<SupportedWaveFormat>();
        //        foreach(var format in values)
        //        {
        //            GlobalData.Logger.Log($"    {AudioInfoHelper.TranslateToReadable(format)}");
        //        }
        //    }
        //}

        public static double SemitoneCoeff = Math.Pow(2, 1 / (double)12);

        public static void PlayEvent(TimeEvent rhythmEvent)
        {
            if(rhythmEvent is NoteOnEvent noteOnEvent)
            {
                //Log.Information("event on");
                var signal = new SinSource(10*Math.Pow(SemitoneCoeff, noteOnEvent.Pitch), 0.02F, 0.002, rhythmEvent.Guid);
                //Log.Information("event created");
                signal.AttachTo(AsyncAudio.Provider);
                //Log.Information("event attached");
            }
            else if(rhythmEvent is NoteOffEvent)
            {
                var find = AsyncAudio.Provider.SignalsSources.FirstOrDefault(s => s.Guid == rhythmEvent.Guid);
                if(find != null)
                {
                    find.DetachFrom(AsyncAudio.Provider);
                }
            }
        }
    }
}
