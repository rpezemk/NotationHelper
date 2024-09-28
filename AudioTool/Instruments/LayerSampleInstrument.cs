using AudioTool.CsEvents;
using AudioTool.Instruments.InstrumentBase;

namespace AudioTool.Instruments
{
    public class Layer
    {
        public Layer(int no, string dyn, string instrName, string basePath, string extension, double sampleLen) 
        {
            No = no;
            Dyn = dyn;
            InstrName = instrName;
            BasePath = basePath;
            Extension = extension;
            SampleLen = sampleLen;
        }

        public int No;
        public string Dyn;
        public string InstrName;
        public string BasePath;
        public string Extension;
        public double SampleLen;

        public string FilePath => $"{BasePath}{Dyn}-{InstrName}.{Extension}";
        public string ToFileLoadScript()
        {
            var str = @$"
                iSampleLen = {SampleLen}
                ;;;;; TODO smoothly loop
                kTimeEnv_{this.Dyn} linseg 0, iSampleLen, iSampleLen

                aLeft_{this.Dyn}_wav1, aRight_{this.Dyn}_wav1 diskin2 ""{this.FilePath}"", 1, iSampleLen

                aLeft_{this.Dyn} = aLeft_{this.Dyn}_wav1
                aRight_{this.Dyn} = aRight_{this.Dyn}_wav1
                ";
            return str;
        }
        /// <summary>
        /// kDiff_{layer.No}
        /// </summary>
        /// <param name="layer"></param>
        /// <param name="instrNo"></param>
        /// <param name="nLayers"></param>
        /// <returns></returns>
        public string ToCalculatedDynamics(int nLayers)
        {
            var res = @$"            
            kDiff_{No} = kMod * {nLayers - 1} - {No}
            kRes_{No} init 0
            if kDiff_{No} > 1 || kDiff_{No} < -1 then 
                kRes_{No} = 0
            elseif kDiff_{No} >= 0 then
                kRes_{No} =   (1 - kDiff_{No})
            elseif kDiff_{No} < 0 then
                kRes_{No} = - (1 - kDiff_{No})
            endif
            ";
            return res;
        }
    }
    public static class InstrumentFileHelper
    {
        public static List<Layer> ViolaLayers = 
            [
                new (0, "P", "Viole", @"C:/Users/slonj/Desktop/Samples/Bowed_", "wav", 4),
                new (1, "MP", "Viole", @"C:/Users/slonj/Desktop/Samples/Bowed_", "wav", 4),
                new (2, "MF", "Viole", @"C:/Users/slonj/Desktop/Samples/Bowed_", "wav", 4),
                new (3, "F", "Viole", @"C:/Users/slonj/Desktop/Samples/Bowed_", "wav", 4),
                new (4, "FF", "Viole", @"C:/Users/slonj/Desktop/Samples/Bowed_", "wav", 4),
            ];
    }


    public class LayerSampleInstrument : AScriptedInstrument<LayerSampleEvent>
    {
        public List<Layer> Layers { get; set; } = new List<Layer>();
        public LayerSampleInstrument(string name, List<Layer> layers) : base(name) 
        {
            Layers = layers;
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
            {Layers.Select(l => l.ToFileLoadScript()).JoinToBlock(18)}

            kMod chnget ""{instrNo.ToModulatorStr()}""
            
            {Layers.Select(l => l.ToCalculatedDynamics(InstrumentFileHelper.ViolaLayers.Count())).JoinToBlock(18)}

            aOutL = 0.1 * ({Layers.ToOutput("Left")})
            aOutR = 0.1 * ({Layers.ToOutput("Right")})
            
            outs aOutL, aOutR
        endin

        ;DYNAMICS_MODULATOR
        instr {instrNo.ToEnvelopeInstrument()}
                prints ""PARAMETER: p4 = %f \n"", p4   ; 
                ;prints ""p6 = %f \n"", p6   ; 
                iCurrEnv chnget ""{instrNo.ToModulatorStr()}""
                kenv linseg iCurrEnv, p3, p4
                chnset kenv, ""{instrNo.ToModulatorStr()}""
        endin
";
            return part;
        }

        public override List<string> GetEventsScript(int instrNo, string instrName)
        {
            return [];//$"i{instrNo.ToEnvelopeNo()} 0 10 1 ; i99 start at 0, 10 hours long, amp .0001"
        }

    }
}
