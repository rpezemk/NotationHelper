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

        

        public override string GetInstrumentScript(int instrNo, string instrName)
        {
            var part =
                @$"
        ;##############################################################
        ;#####         {instrNo} {instrName}                    #######      
        ;##############################################################
        instr {instrNo}
            kenv linseg 0, p3, 1
            aLeft_Piano, aRight_Piano diskin2 ""{FilePathPiano}"", 1, p4
            aOutL_p atone aLeft_Piano,  230
            aOutR_p atone aRight_Piano, 230
            aLeft_Forte, aRight_Forte diskin2 ""{FilePathForte}"", 1, p4
            aOutL_f atone aLeft_Forte,  230
            aOutR_f atone aRight_Forte, 230
            iDynCount = 5                ; Number of dynamic files
            iCheckedDynNo = 2            ; Example checked dynamic number
            iCurrValue = 3               ; Example current value

            kmod chnget ""{instrNo.ToModulatorStr()}""
            ;event_i ""i"" ,{instrNo + 20}, 0, 0, 2, kmod, 0
            ;event_i ""i"" ,{instrNo + 20}, 0, 0, 2, kmod, 1 

            aOutL = 0.2*aOutL_p * (1-kmod) + 0.2*aOutL_f * kmod
            aOutR = 0.2*aOutR_p * (1-kmod) + 0.2*aOutR_f * kmod

            outs aOutL, aOutR
        endin


        ;;;;;;;;;;;;;;;;;;;;DYNAMICS_MODULATOR {instrNo} => {instrNo.ToEnvelopeNo()} {instrName}       
        ;;;;;;;;;;;;;;;;;;;instr {instrNo.ToEnvelopeNo()}
        ;;;;;;;;;;;;;;;;;;;        kfreq init 0.7                     ; Set LFO frequency to 5 Hz
        ;;;;;;;;;;;;;;;;;;;        ; Generate the LFO signal
        ;;;;;;;;;;;;;;;;;;;        kLFO oscili 1, kfreq, 1          ; LFO signal with amplitude 1
        ;;;;;;;;;;;;;;;;;;;        kmodulatedValue = (kLFO + 1) * 0.5 ; Normalize to 0-1 range
        ;;;;;;;;;;;;;;;;;;;        chnset kmodulatedValue, ""{instrNo.ToModulatorStr()}""
        ;;;;;;;;;;;;;;;;;;;endin

        ;DYNAMICS_MODULATOR
        instr {instrNo.ToEnvelopeNo()}
                prints ""PARAMETER: p4 = %f \n"", p4   ; 
                ;prints ""p6 = %f \n"", p6   ; 
                itest chnget ""{instrNo.ToModulatorStr()}""
                kenv linseg itest, p3, p4
                chnset kenv, ""{instrNo.ToModulatorStr()}""
        endin
        
        instr {instrNo + 20} 
            iRes init 0
            iDynCount = p4             ; N-files
            iMaxDyn = iDynCount - 1         
            iCheckedDynNo = p6 * iMaxDyn ; [0...N-1]
            iCurrValue = p5 * iMaxDyn   ; [0..N-1]       ; ""kenv""
            iDiff = iCurrValue - iCheckedDynNo

            if iDiff > 1 || iDiff < -1 then 
                iRes = 0
            endif
            if iDiff > 0 then
                iRes = iDiff 
            endif
            if iDiff < 0 then
                iRes = -iDiff 
            endif
    
            chnset iRes, ""myTestChan""   ;     
        endin
        


";
            return part;
        }
        public override List<string> GetEventsScript(int instrNo, string instrName)
        {
            return [];//$"i{instrNo.ToEnvelopeNo()} 0 10 1 ; i99 start at 0, 10 hours long, amp .0001"
        }

    }

    public static class InstrumentHelper
    {
        public static string ToLeftAudioOut(this int instrNo) => $"audio_out_left_{instrNo}";
        public static string ToRightAudioOut(this int instrNo) => $"audio_out_right_{instrNo}";
        public static string ToModulatorStr(this int instrNo) => $"MODULATOR_{instrNo}";
        public static int ToEnvelopeNo(this int instrNo) => instrNo + 10;

        public static string ToFileLoad(string filePath, int dynamics)
        {
            var str = @$"            
            aLeft_{dynamics}, aRight_{dynamics} diskin2 ""{filePath}"", 1, p4
            aOutL_{dynamics} atone aLeft_{dynamics},  230
            aOutR_{dynamics} atone aRight_{dynamics}, 230";
            return str;
        }


    }
}
