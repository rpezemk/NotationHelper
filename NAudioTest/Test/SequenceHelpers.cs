using MusicDataModel.DataModel.Elementary;
using MusicDataModel.DataModel.Piece;
using MusicDataModel.MidiModel;
using NAudioTest.TimeThings;
using Serilog;

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
                if(th is Note note)
                {
                    sequence.RhythmHolders.Add(new PitchHolder() { Duration = th.Duration.GetLen() * scaleUp, Guid = Guid.NewGuid(), Pitch = note.Pitch.ResultPitch});
                }
                else
                {
                    sequence.RhythmHolders.Add(new RhythmHolder() { Duration = th.Duration.GetLen() * scaleUp, Guid = Guid.NewGuid()});
                }
            }

            return sequence;
        }

        public static List<TimeEvent> ToTimeEvents(this RhythmSequence rhythmSequence)
        {
            List<TimeEvent> timeEvents = new List<TimeEvent>();
            var currTime = 0.0D;
            foreach(var holder in rhythmSequence.RhythmHolders)
            {
                if(holder is PitchHolder noteHolder) 
                    timeEvents.Add(new NoteOnEvent(currTime, holder, holder.Guid) { Pitch = noteHolder.Pitch });
                currTime += holder.Duration;
                timeEvents.Add(new NoteOffEvent(currTime, holder, holder.Guid));
            }

            return timeEvents;
        }
    }
}