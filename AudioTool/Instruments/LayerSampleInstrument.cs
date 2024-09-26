using AudioTool.CsEvents;
using AudioTool.Instruments.InstrumentBase;

namespace AudioTool.Instruments
{
    public class LayerSampleInstrument : AScriptedInstrument<LayerSampleEvent>
    {
        public string FilePathPiano = @"C:/Users/slonj/Desktop/Samples/Bowed_P-Viole.wav";
        public string FilePathForte = @"C:/Users/slonj/Desktop/Samples/Bowed_F-Viole.wav";
        public LayerSampleInstrument(string name) : base(name)
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
            ;ip5 = p5
            ;ip6 = p6
            ;printf ""p5 = %f, p6 = %f\n"", ip5, ip6
            prints ""p5 = %f \n"", p5   ; 
            prints ""p6 = %f \n"", p6   ; 
            kenv linseg 0, p3, 1
            aLeft_Piano, aRight_Piano diskin2 ""{FilePathPiano}"", 1, p4
            aLeft_Forte, aRight_Forte diskin2 ""{FilePathForte}"", 1, p4
            aOutL_p atone aLeft_Piano,  230
            aOutR_p atone aRight_Piano, 230
            aOutL_f atone aLeft_Forte,  230
            aOutR_f atone aRight_Forte, 230
            
            aOutL = 0.2*aOutL_p * (1-kenv) + 0.2*aOutL_f * kenv
            aOutR = 0.2*aOutR_p * (1-kenv) + 0.2*aOutR_f * kenv
            
            outs aOutL, aOutR  ; Output to both stereo channels
        endin";
            return part;
        }
    }
}
