using NAudioTest.Helpers;
using NAudioTest.Providers;
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

        private static void SinSourceTest()
        {
            if (AsyncAudio.Provider == null)
                Task.Run(() => AsyncAudio.InitOnce());
            if (AsyncAudio.Provider != null)
            {
                for (var a = 1; a < 10; a++)
                {
                    var N = 10;
                    var root = Math.Pow(2, 1 / (double)N);
                    var startFreq = 440;
                    var currCoeff = 1.0D;
                    for (var i = 0; i < N; i++)
                    {
                        currCoeff *= root;
                        new SinSource(startFreq * currCoeff, 0.00002F, 200.0).AttachTo(AsyncAudio.Provider);
                    }
                }
            }
        }

    }
}
