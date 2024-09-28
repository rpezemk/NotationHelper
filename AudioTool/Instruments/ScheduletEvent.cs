namespace AudioTool.Instruments
{
    public class ScheduletEvent
    {
        public Action Action;
        public double Duration;
        public void RunWaitin()
        {
            if (Action != null)
                Action();
            Thread.Sleep(1000 * (int)Duration);
        }
    }
}
