using NAudioTest.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NAudioTest.TimeThings
{

    public class TimeQueue
    {
        public TimeQueue(Action<TimeEvent> action) 
        {
            Action = action;
        }
        public int CurrTick = 0;
        public Action<TimeEvent> Action;
        /// <summary>
        /// OLDEST LAST
        /// </summary>
        public List<TimeEvent> TimeEvents { get; set; } = new List<TimeEvent>();
        public void AppendEvent(TimeEvent timeEvent) 
        {
            if(TimeEvents.All(te => te.TickNo < timeEvent.TickNo))
                TimeEvents.Add(timeEvent);
            else
            {
                var before = TimeEvents.Where(te => te.TickNo < timeEvent.TickNo).OrderBy(te => te.TickNo).ToList();
                var after = TimeEvents.Where(te => te.TickNo >= timeEvent.TickNo).OrderBy(te => te.TickNo).ToList();
                TimeEvents = before.Append(timeEvent).Union(after).ToList();
            }    
        }

        public List<TimeEvent> GetEvents(int tickNo, double tickCount)
        {
            var preTick = tickNo - tickCount;
            var res = TimeEvents.Where(te => te.TickNo <= tickNo && te.TickNo > preTick).ToList();
            return res;
        }

        public void Play(double freq)
        {
            Task.Run(() => play(freq));
        }

        private void play(double freq)
        {
            var maxT = (TimeEvents.Count == 0? 0: TimeEvents.Max(te => te.TickNo)) / 1000;
            var period = 1 / freq;
            TimeSpan timeout = TimeSpan.FromSeconds(period);
            for (var i = 0; i < maxT; i++)
            {
                var events = GetEvents(i*1000, 1000*period);
                foreach (var e in events)
                {
                    Action.Invoke(e);
                }
                Thread.Sleep(timeout);
            }
        }
    }

    public class TimeEvent
    {
        public TimeEvent(int tickNo)
        {
            TickNo = tickNo;
        }
        public int TickNo;
    }

    public enum EventType
    {
        Tick,
        Shot,
        Duration,
    }
}
