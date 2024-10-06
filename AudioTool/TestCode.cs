using AudioTool.MidiModel;

namespace AudioTool
{
    public static class TestCode
    {
        public static List<(int pitch, double duration)> SampleNotes = new List<(int pitch, double duration)>()
        {
            (2,  0.25),
            (4,  0.25),
            (5,  0.25),
            (7,  0.25),
            (9,  0.50),
            (14, 0.50),
            (13, 0.50),
            (9,  0.50),
            (4,  0.50),
            (7,  0.50),
        };
        public static MidiPiece GetSamplePiece(int noOfParts, int noOfBars)
        {
            MidiPiece piece = new MidiPiece();
            for (int partNo = 0; partNo < noOfParts; partNo++)
            {
                MidiPart midiPart = new MidiPart();
                for (int barNo = 0; barNo < noOfBars; barNo++)
                {
                    MidiBar midiBar = new MidiBar();
                    foreach (var tuple in SampleNotes)
                    {
                        MidiNote midiNote = new MidiNote()
                        {
                            Pitch = tuple.pitch,
                            Duration = tuple.duration,
                        };
                        midiBar.AppendChild(midiNote);
                    }
                    midiPart.AppendChild(midiBar);
                }
                piece.AppendChild(midiPart);
            }
            return piece;
        }
    }
}
