using NAudioTest.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NAudioTest.TimeThings
{
    public abstract class APlayer
    {
        public abstract bool CanPlay(TimeEvent timeEvent);
    }

    public abstract class APlayer<T> : APlayer
    {

    }

    public class TimeQueue
    {
        public TimeQueue(Action<TimeEvent> action, double freq) 
        {
            ReportAction = action;
            this.freq = freq;
        }

        public void Play()
        {
            Task.Run(() => play());
        }
        private double freq;
        public int CurrTick = 0;
        public Action<TimeEvent> ReportAction;
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
                var before = TimeEvents.Where(te => te.Time < timeEvent.Time).OrderBy(te => te.Time).ToList();
                var after = TimeEvents.Where(te => te.Time >= timeEvent.Time).OrderBy(te => te.Time).ToList();
                TimeEvents = before.Append(timeEvent).Union(after).ToList();
            }    
        }

        public List<TimeEvent> GetEvents(double time, double timeBefore)
        {
            var preTime = time - timeBefore;
            var res = TimeEvents.Where(te => te.Time <= time && te.Time > preTime).ToList();
            return res;
        }

        private void play()
        {
            var maxTsecs = TimeEvents.Count == 0? 0: TimeEvents.Max(te => te.Time);
            var period = 1 / freq;
            TimeSpan timeout = TimeSpan.FromSeconds(period);
            for (double i = 0; i < maxTsecs; i+=period)
            {
                var events = GetEvents(i, period);
                foreach (var e in events)
                {
                    if(ReportAction != null)
                        ReportAction.Invoke(e);
                }
                Thread.Sleep(timeout);
            }
        }
    }

    public class TimeEvent
    {
        public TimeEvent(double timeSeconds)
        {
            Time = timeSeconds;
        }

        /// <summary>
        /// Seconds
        /// </summary>
        public double Time { get; set; }
        public override string ToString()
        {
            var s = (int)Time;
            var ms = (int)((Time - s) * 1000);
            var res = $"{s}.{ms}";
            return res;
        } 

    }

    public enum EventType
    {
        Tick,
        Shot,
        Duration,
    }
}
