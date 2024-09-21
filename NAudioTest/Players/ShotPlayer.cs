using NAudioTest.TimeThings;

namespace NAudioTest.Players
{
    public class ShotPlayer : APlayer<ShotPlayer>
    {
        public override bool CanPlay(TimeEvent timeEvent)
        {
            return true;
        }
    }
}
