using Microsoft.Win32;
using NAudio.Wave;
using NAudioTest.AudioInfo;
using NAudioTest.Helpers;
using NAudioTest.Providers;
using NAudioTest.TimeThings;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static System.Windows.Forms.DataFormats;

namespace NAudioTest
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            GlobalData.Logger.ActionStr = (str) => LoggerTextBox.Text += str + "\n";
        }

        private string path1;
        private string path2;

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Thread backgroundThread = new Thread(UpdateTextBox);
            backgroundThread.Start();
           
        }

        private void UpdateTextBox(object? obj)
        {
            TimeMachine.Test(
           (te) =>
           {
               Dispatcher.Invoke(() =>
               {
                   LoggerTextBox.Text += $"timeEvent at {te.TickNo}" + "\n";
               });
           });
        }

        private void Button_Click2(object sender, RoutedEventArgs e)
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


        private void Read1stFile(object sender, RoutedEventArgs e)
        {
            string path;
            if (!FileHelper.TryOpenFile(out path))
                return;
            path1 = path;
        }


        

        private void Read2ndFile(object sender, RoutedEventArgs e)
        {
            string path;
            if (!FileHelper.TryOpenFile(out path))
                return;
            path2 = path;
        }

        private void GetDevicesInfo_Click(object sender, RoutedEventArgs e)
        {
            for (int deviceNumber = 0; deviceNumber < WaveOut.DeviceCount; deviceNumber++)
            {
                // Get the capabilities of each output device
                var capabilities = WaveOut.GetCapabilities(deviceNumber);
                GlobalData.Logger.Log($"Device {deviceNumber}: {capabilities.ProductName}");
                var values = Enum.GetValues(typeof(SupportedWaveFormat)).Cast<SupportedWaveFormat>();
                foreach(var format in values)
                {
                    GlobalData.Logger.Log($"    {AudioInfoHelper.TranslateToReadable(format)}");
                }
            }
        }
    }



    public class MyWaveProvider : IWaveProvider
    {
        public WaveFormat WaveFormat => throw new NotImplementedException();

        public int Read(byte[] buffer, int offset, int count)
        {
            throw new NotImplementedException();
        }
    }


}