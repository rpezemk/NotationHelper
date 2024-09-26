using AudioTool.Instruments.InstrumentBase;

namespace AudioTool.Instruments
{
    public class NoiseInstrument : AScriptedInstrument
    {
        public NoiseInstrument(string name) : base(name)
        {
        }

        public override List<string> GetEventsScript(int instrNo, string instrName)
        {
            List<string> res =
                [
                    $"i{instrNo} 0 36000 0.0003 ; i99 start at 0, 10 hours long, amp .0001"
                ];
            return res;
        }

        public override string GetInstrumentScript(int instrNo, string instrName)
        {
            var part = $@"
        ;#########################################################################    
        ;### NOISE NOISE NOISE NOISE NOISE NOISE NOISE NOISE NOISE NOISE NOISE  ##    
        ;#########################################################################    
        instr {instrNo}
            kEnv linen p4, 0.1, p3, 0.1  ; Create an envelope for amplitude control
            aNoise rand -1, 1            ; Generate white noise
            aOut = aNoise * kEnv          ; Apply envelope to noise
            outs aOut, aOut               ; Output to stereo channels
        endin";
            return part;
        }
    }
}
