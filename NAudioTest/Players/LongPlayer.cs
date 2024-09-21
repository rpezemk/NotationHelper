using NAudioTest.TimeThings;

namespace NAudioTest.Players
{
    public class LongPlayer : APlayer<ShotPlayer>
    {
        public override bool CanPlay(TimeEvent timeEvent)
        {
            return true;
        }
    }
}
