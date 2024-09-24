using MusicDataModel.MidiModel;
using NAudioTest.Helpers;
using NAudioTest.Providers;
using NAudioTest.Test;
using NAudioTest.TimeThings;
using Serilog;
using System.Windows;

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
            Log.Logger = new LoggerConfiguration()
            .WriteTo.Sink(new TextBoxSink(LoggerTextBox))
            .CreateLogger();
        }

        RhythmSequence rtmSeq;
        EventPlayer eventPlayer = null;
        private void PlaySequenceClick(object sender, RoutedEventArgs e)
        {
            var samplePart = TestRhythms.GetSamplePart();
            if(rtmSeq == null) 
                rtmSeq = samplePart.ToRhythmSequence();
            SequenceView.ShowSequence(rtmSeq);
            var timeEvents = rtmSeq.ToTimeEvents();
            if(eventPlayer != null && eventPlayer.IsPlaying)
            {
                //Log.Information("already playing");
                return;
            }
            DateTime dt1 = DateTime.Now;
            var evtCnt = 0;
            eventPlayer = 
                new EventPlayer(
                    te =>
                    {
                        Dispatch(te, t =>
                        {
                            
                            if(te is NoteOnEvent)
                            {
                                var now = DateTime.Now;
                                //Log.Information($"event {(int)(now - dt1).TotalMilliseconds}, evtNo: {evtCnt}");
                                OldTests.PlayEvent(t);
                                dt1 = now;
                                evtCnt++;
                            }

                        }
                        );
                    },
                    16, 
                    timeEvents);
            eventPlayer.PlayAsync(); 
        }

        private void Dispatch<T>(T arg, Action<T> action)
        {
            Dispatcher.Invoke(action, arg);
        }

        private void LoggerTextBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            LoggerTextBox.ScrollToEnd();
            LoggerScrollViewer.ScrollToBottom();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (AsyncAudio.Provider == null)
                Task.Run(() => AsyncAudio.InitOnce());
        }
    }
}