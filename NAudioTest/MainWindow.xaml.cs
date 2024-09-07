using Microsoft.Win32;
using NAudio.Wave;
using NAudioTest.Helpers;
using NAudioTest.Providers;
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
            //if (!File.Exists(path1))
            //    return;
            if(AsyncAudio.Provider == null)
                Task.Run(() => AsyncAudio.Init());
            if(AsyncAudio.Provider != null)
            {
                var N = 1000;
                var root = Math.Pow(2, 1 / (double)N);
                var startFreq = 440;
                var currCoeff = 1.0D;
                for(var i = 0;i < N; i++)
                {
                    currCoeff *= root;
                    new SinSource(startFreq * currCoeff, 0.0002F, 200.0).AttachTo(AsyncAudio.Provider);
                }
            }
                

            //    return;
            //// Load the first audio file into stereo arrays
            //float[][] audio1 = AudioFileHelper.LoadAudioFileToStereoArray(path1);
            //// Load the second audio file into stereo arrays
            //float[][] audio2 = AudioFileHelper.LoadAudioFileToStereoArray(path2);
            //// Create a custom sample provider that mixes the two stereo arrays
            //var mixedSampleProvider = new MixedSampleProvider(audio1, audio2);

            //// Create a wave out device for playback
            //using var waveOut = new WaveOutEvent();
            //waveOut.Init(mixedSampleProvider);

            //// Start playback
            //waveOut.Play();
            //Thread.Sleep(1234);
            //// Wait until playback is done
            //Thread.Sleep(15000);
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
                    GlobalData.Logger.Log($"    {TranslateToReadable(format)}");
                }
            }
        }

        public string TranslateToReadable(SupportedWaveFormat format)
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



    public class MyWaveProvider : IWaveProvider
    {
        public WaveFormat WaveFormat => throw new NotImplementedException();

        public int Read(byte[] buffer, int offset, int count)
        {
            throw new NotImplementedException();
        }
    }


    // Custom sample provider for sequencing notes
    public class SequenceSampleProvider : ISampleProvider
    {
        private readonly double[] frequencies;
        private readonly double duration;
        private int currentIndex;
        private double currentSample;

        public WaveFormat WaveFormat { get; } = WaveFormat.CreateIeeeFloatWaveFormat(44100, 1);

        public SequenceSampleProvider(double[] frequencies, double duration)
        {
            this.frequencies = frequencies;
            this.duration = duration;
            currentIndex = 0;
            currentSample = 0;
        }

        public int Read(float[] buffer, int offset, int count)
        {
            int samplesRead = 0;
            for (int i = 0; i < count; i++)
            {
                if (currentSample >= duration * 44100)
                {
                    currentSample = 0;
                    currentIndex = (currentIndex + 1) % frequencies.Length;
                }
                buffer[offset + i] = (float)Math.Sin(2 * Math.PI * frequencies[currentIndex] * currentSample / 44100.0);
                currentSample++;
                samplesRead++;
            }
            return samplesRead;
        }
    }


}