namespace NAudioTest.TimeThings
{
    public static class TestTimeMachine
    {
        // OLDEST LAST
        public static EventPlayer EvtPlayer { get; set; } // oldest last

        public static EventPlayer GetTestTimeQueue(double playFreq, int nEvents, float eventDuration, Action<TimeEvent> action)
        {
            EvtPlayer = new EventPlayer(action, playFreq);
            for(int i = 0; i < nEvents; i++)
            {
                EvtPlayer.AppendEvent(new TimeEvent(eventDuration * i, "test", Guid.NewGuid()));
            }
            return EvtPlayer;
        }
    }
}
