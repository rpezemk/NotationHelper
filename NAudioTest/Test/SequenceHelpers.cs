using MusicDataModel.DataModel.Elementary;
using MusicDataModel.DataModel.Piece;
using MusicDataModel.MidiModel;
using NAudioTest.TimeThings;

namespace NAudioTest.Test
{
    internal static class SequenceHelpers
    {
        public static RhythmSequence ToRhythmSequence(this MonoPart part)
        {
            RhythmSequence sequence = new RhythmSequence();
            var timeHolders = part.Children.SelectMany(vb => vb.Children).ToList();
            var scaleUp = (60 / part.BPM) * (((int)part.BeatBase) / 4);
            foreach (var th in timeHolders)
            {
                sequence.RhythmHolders.Add(new RhythmHolder() { Duration = th.Duration.GetLen() * scaleUp });
            }

            return sequence;
        }

        public static List<TimeEvent> ToTimeEvents(this RhythmSequence rhythmSequence)
        {
            List<TimeEvent> timeEvents = new List<TimeEvent>();
            var currTime = 0.0D;
            foreach(var holder in rhythmSequence.RhythmHolders)
            {
                timeEvents.Add(new RhythmEvent(currTime, "rObj ON ", holder));
                currTime += holder.Duration;
                timeEvents.Add(new RhythmEvent(currTime, "rObj OFF", holder));
            }

            return timeEvents;
        }
    }
}