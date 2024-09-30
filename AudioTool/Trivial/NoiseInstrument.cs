using AudioTool.InstrumentBase;

namespace AudioTool.Trivial
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
        ;##################
        ;### TAPE NOISE ###
        ;##################
        instr {instrNo}
            kEnv linen p4, 0.1, p3, 0.1  ; Create an envelope for amplitude control
            aNoise rand -1, 1            ; Generate white noise
            aOut = aNoise * kEnv          ; Apply envelope to noise
            outs aOut, aOut               ; Output to stereo channels
        endin";
            return part;
        }
    }    
    
    public class ReverbInstrument : AScriptedInstrument
    {
        public ReverbInstrument(string name) : base(name)
        {
        }

        public override List<string> GetEventsScript(int instrNo, string instrName)
        {
            List<string> res =
                [
                    $"i 5 0 300 "
                ];
            return res;
        }

        public override string GetInstrumentScript(int instrNo, string instrName)
        {
            var part = $@"
        ;##################
        ;### TAPE NOISE ###
        ;##################
        instr 5 ; reverb - always on
            kroomsize init 0.85 ; room size (range 0 to 1)
            kHFDamp init 0.5 ; high freq. damping (range 0 to 1)
            ; create reverberated version of input signal (note stereo input and output)
            aRvbL,aRvbR freeverb gaRvbSendL, gaRvbSendR,kroomsize,kHFDamp
            outs aRvbL, aRvbR ; send audio to outputs
            ;outs gaRvbSendL, gaRvbSendR ; send audio to outputs
            ;outs gaRvbSendL, gaRvbSendR ; send audio to outputs
            clear gaRvbSendL ; clear global audio variable
            clear gaRvbSendR ; clear global audio variable
        endin";
            return part;
        }
    }
}
