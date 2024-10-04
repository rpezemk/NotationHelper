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
        public double VisualDuration { get; set; }

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

        public TimeHolder GetNextTimeHolder()
        {
            if (Parent == null)
                return null;
            var thisIdx = Parent.Children.IndexOf(this);
            if(thisIdx < Parent.Children.Count - 1)
                return Parent.Children[thisIdx + 1];

            if (Parent.Parent == null)
                return null;
            var gran = Parent.Parent;
            var parIdx = gran.Children.IndexOf(Parent);
            if (parIdx < gran.Children.Count - 1)
                return gran.Children[parIdx + 1].Children.FirstOrDefault();

            return null;
        }

    }
}
