namespace NAudioTest.TimeThings
{
    public static class TestTimeMachine
    {
        // OLDEST LAST
        public static TimeQueue TimeQueue { get; set; } // oldest last

        public static void Test(Action<TimeEvent> action, double freq)
        {
            TimeQueue = new TimeQueue(action, freq);
            for(int i = 0; i < 5000; i++)
            {
                TimeQueue.AppendEvent(new TimeEvent(i/1000));
            }
            TimeQueue.Play();
        }
    }
}
