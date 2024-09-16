namespace NAudioTest.TimeThings
{
    public static class TimeMachine
    {
        // OLDEST LAST
        public static TimeQueue TimeQueue { get; set; } // oldest last

        public static void Test(Action<TimeEvent> action)
        {
            TimeQueue = new TimeQueue(action);
            for(int i = 0; i < 5000; i++)
            {
                TimeQueue.AppendEvent(new TimeEvent(i));
            }
            TimeQueue.Play(1);
        }
    }
}
