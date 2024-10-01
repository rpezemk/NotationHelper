using MusicDataModel.DataModel.Elementary;
using MusicDataModel.DataModel.Structure;

namespace MusicDataModel.DataModel.Piece
{
    public abstract class TimeHolder : AObjectWithParent<VoiceBar, TimeHolder>
    {
        public Duration Duration { get; set; } = new Duration();
        public abstract TimeGroupTypeEnum GroupType { get; }

        public int BarNo { get; set; }
        public int PartNo { get; set; }

        public double MOffset { get; set; }
        public double XOffset { get; set; }
        public double YOffset { get; set; }
        public bool IsSelected { get; set; }
        public TimeHolder AsTimeGroup() => this;
        public TimeHolder SetBaseDuration(DurationEnum duration)
        {
            Duration.BaseDuration = duration;
            return this;
        }

        public TimeHolder SetDotting(DottingEnum dotting)
        {
            Duration.Dotting = dotting;
            return this;
        }

    }
}
