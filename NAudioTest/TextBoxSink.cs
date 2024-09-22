using Serilog.Core;
using Serilog.Events;
using System.Windows.Controls;

namespace NAudioTest
{
    public class TextBoxSink : ILogEventSink
    {
        private readonly TextBox _textBox;
        private readonly IFormatProvider _formatProvider;

        public TextBoxSink(TextBox textBox, IFormatProvider formatProvider = null)
        {
            _textBox = textBox;
            _formatProvider = formatProvider;
        }

        public void Emit(LogEvent logEvent)
        {
            var message = logEvent.RenderMessage(_formatProvider);

            // Ensure the TextBox is updated on the UI thread
            try
            {
                _textBox.Dispatcher.Invoke(() =>
                {
                    var ts = logEvent.Timestamp;
                    var timestamp = ts.ToString("HH:mm:ss") + ">" + ts.ToString("fff");
                    var message = logEvent.RenderMessage();
                    var logEntry = $"[{timestamp}] {message}";
                    _textBox.Dispatcher.Invoke(() =>
                    {
                        _textBox.AppendText(logEntry + Environment.NewLine);
                        _textBox.ScrollToEnd();  // Optional: Auto-scroll to the latest log
                    });
                });
            }
            catch (Exception ex)
            {
                _textBox.Dispatcher.Invoke(() => { _textBox.AppendText("job cancelled")});
            }
        }
    }
}