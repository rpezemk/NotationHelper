namespace AudioTool.Instruments
{
    public static class InstrumentHelper
    {
        public static string ToLeftAudioOut(this int instrNo) => $"audio_out_left_{instrNo}";
        public static string ToRightAudioOut(this int instrNo) => $"audio_out_right_{instrNo}";
        public static string ToModulatorStr(this int instrNo) => $"MODULATOR_{instrNo}";
        public static int ToEnvelopeInstrument(this int instrNo) => instrNo + 10;
        public static string JoinToBlock(this IEnumerable<string> values, int indent) => "\n" + string.Join("\n", values.Select(v => "".PadLeft(indent) + v));
        

        public static string ToKMod(this int instrNo) => $"kMod_{instrNo}";



        public static string ToOutput(this List<Layer> layers, string side)
        {
            //0.2*aLeft_P * (1-kMod) + 0.2*aLeft_F * kMod

            var res = string.Join(" + ", layers.Select(l => $"a{side}_{l.Dyn} * kRes_{l.No}"));
            return res;
        }

    }
}
