using AudioTool.CsEvents;
using AudioTool.CSoundWrapper;
using AudioTool.InstrumentBase;
using System.IO;

namespace AudioTool.Flat
{
    public class FlatSampleInstrument : AScriptedInstrument<FlatSampleEvent>, ISinglePathInstrument
    {

        public string FilePath = @"C:/Users/slonj/Desktop/Samples/Bowed_MP-Viole.wav";

        public FlatSampleInstrument(string name) : base(name)
        {
        }

        public override List<string> GetEventsScript(int instrNo, string instrName)
        {
            return new List<string>();
        }

        public override string GetInstrumentScript(int instrNo, string instrName)
        {
            var part =
                @$"
        ;##############################################################
        ;#####         {instrNo} {instrName}                    #######      
        ;##############################################################
        instr {instrNo}
            aLeft, aRight diskin2 ""{FilePath}"", 1, p4
            aOutL atone aLeft,  230
            aOutR atone aRight, 230
            outs 0.2*aOutL, 0.2*aOutR  ; Output to both stereo channels
        endin";
            return part;
        }

        public string GetPath()
        {
            return FilePath;
        }
    }
}
