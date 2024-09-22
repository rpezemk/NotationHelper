using Microsoft.Win32;
using NAudio.Wave;
using NAudioTest.AudioInfo;
using NAudioTest.Helpers;
using NAudioTest.Providers;
using NAudioTest.Test;
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

        private void UpdateTextBox(object? obj)
        {
            TestTimeMachine.Test(
           (te) =>
           {
               Dispatcher.Invoke(() =>
               {
                   LoggerTextBox.Text += $"timeEvent at {te.Time}" + "\n";
               });
           },
           40);
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


        private void ShowSequence(object sender, RoutedEventArgs e)
        {
            var seq = TestRhythms.GetSampleSequence();
            SequenceView.ShowSequence(seq);
        }

        private void PlaySequence(object sender, RoutedEventArgs e)
        {
            Thread backgroundThread = new Thread(o => UpdateTextBox(o));
            backgroundThread.Start();
        }



    }

}