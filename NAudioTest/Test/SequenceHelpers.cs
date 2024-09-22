using MusicDataModel.DataModel.Piece;
using MusicDataModel.MidiModel;

namespace NAudioTest.Test
{
    internal static class SequenceHelpers
    {
        public static RhythmSequence ToSequence(this Part part)
        {
            RhythmSequence sequence = new RhythmSequence();
            var timeHolders = part.Children.SelectMany(vb => vb.Children).ToList();

            foreach (var th in timeHolders)
            {
                sequence.RhythmHolders.Add(new RhythmHolder() { Duration = th.Duration.GetLen() });
            }

            return sequence;
        }
    }
}