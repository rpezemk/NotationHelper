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
                Log.Information("already playing");
                return;
            }
            eventPlayer = 
                new EventPlayer(
                    te =>
                    {
                        Dispatch(te, t =>
                        {
                            SequenceView.UnSelectAll();
                            if (te is RhythmEvent sdf)
                                SequenceView.SelectOne(sdf.ParentHolder);
                        }
                        );
                    }, 10, timeEvents);
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
    }
}