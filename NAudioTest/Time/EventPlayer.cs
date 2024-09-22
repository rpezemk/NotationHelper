using MusicDataModel.MidiModel;
using NAudioTest.Helpers;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Threading;

namespace NAudioTest.TimeThings
{

    public class EventPlayer
    {
        bool isPlaying = false;
        public bool IsPlaying => isPlaying;
        private double freq;
        public int CurrTick = 0;
        public Action<TimeEvent> ReportAction;
        private System.Timers.Timer timer;
        private double currentTime;
        private double maxTsecs;
        private double period;

        public EventPlayer(Action<TimeEvent> action, double freq) 
        {
            ReportAction = action;
            this.freq = freq;
        }

        public EventPlayer(Action<TimeEvent> action, double freq, List<TimeEvent> timeEvents)
        {
            ReportAction = action;
            this.freq = freq;
            foreach(var te in timeEvents)
            {
                AppendEvent(te);
            }
        }

        public void PlayAsync()
        {
            if (isPlaying)
            {
                Log.Information("already playing");
                return;
            }
            isPlaying = true;
            Log.Information("starting");
            Task.Run(() => play());
        }

        private void play()
        {
            isPlaying = true;
            maxTsecs = TimeEvents.Count == 0 ? 0 : TimeEvents.Max(te => te.Time);
            period = 1 / freq;
            timer = new System.Timers.Timer();
            timer.Interval = period * 1000;  // Set the timer interval
            currentTime = 0;
            var tickCounter = 0;
            var eventeCounter = 0;
            timer.Elapsed += (sender, e) =>
            {
                var thisTickEvents = GetEvents(currentTime, period);
                TimeEvent prev = null;
                foreach (var evt in thisTickEvents)
                {
                    if(prev != null)
                        prev.IsLastPlayed = false;
                    evt.IsLastPlayed = true;
                    ReportAction?.Invoke(evt);
                    Log.Information(evt.ToString());
                    prev = evt;
                    eventeCounter++;
                }

                if (currentTime > maxTsecs)
                {
                    Log.Information("stopping");
                    timer.Stop();
                    Log.Information("stopped");
                    isPlaying = false;
                    return;
                }
                currentTime += period;
                tickCounter++;
            };
            timer.Start();
        }

        /// <summary>
        /// OLDEST LAST
        /// </summary>
        public List<TimeEvent> TimeEvents { get; set; } = new List<TimeEvent>();
        public void AppendEvent(TimeEvent timeEvent) 
        {
            if(TimeEvents.All(te => te.Time < timeEvent.Time))
                TimeEvents.Add(timeEvent);
            else
            {
                var before = TimeEvents.Where(te => te.Time <= timeEvent.Time).OrderBy(te => te.Time).ToList();
                var after = TimeEvents.Where(te => te.Time > timeEvent.Time).OrderBy(te => te.Time).ToList();
                TimeEvents = before.Append(timeEvent).Union(after).ToList();
            }    
        }

        public List<TimeEvent> GetEvents(double time, double timeBefore)
        {
            var preTime = time - timeBefore;
            var res = TimeEvents.Where(te => te.Time <= time && te.Time > preTime).ToList();
            return res;
        }
    }

    public class TimeEvent
    {
        public Guid Guid;
        public TimeEvent(double timeSeconds, string name, Guid guid)
        {
            Time = timeSeconds;
            Name = name;
            Guid = guid;
        }

        public bool IsLastPlayed {  get; set; }
        /// <summary>
        /// Seconds
        /// </summary>
        public double Time { get; set; }
        public string Name { get; set; }
        public override string ToString()
        {
            var s = (int)Time;
            var ms = (int)((Time - s) * 1000);
            var name = string.IsNullOrEmpty(Name) ? "" : $"[{Name}]: ";
            var res = $"{name}{s}.{ms}";
            return res;
        } 

    }

    public class NoteOnEvent : TimeEvent
    {
        public int Pitch;
        public RhythmHolder ParentHolder { get; set; }
        public NoteOnEvent(double timeSeconds, Guid guid) : base(timeSeconds, "NoteOn", guid)
        {
        }
        public NoteOnEvent(double timeSeconds, RhythmHolder rhythmHolder, Guid guid) : base(timeSeconds, "NoteOn", guid)
        {
            ParentHolder = rhythmHolder;
        }
    }

    public class NoteOffEvent : TimeEvent
    {
        public RhythmHolder ParentHolder { get; set; }
        public NoteOffEvent(double timeSeconds, Guid guid) : base(timeSeconds, "NoteOff", guid)
        {
        }
        public NoteOffEvent(double timeSeconds, RhythmHolder rhythmHolder, Guid guid) : base(timeSeconds, "NoteOff", guid)
        {
            ParentHolder = rhythmHolder;
        }
    }


    public enum EventType
    {
        Tick,
        Shot,
        Duration,
    }
}
