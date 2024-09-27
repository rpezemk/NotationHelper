using AudioTool.CsEvents;
using AudioTool.Instruments.InstrumentBase;

namespace AudioTool.Instruments
{
    public class Layer
    {
        public Layer(int no, string dyn, string instrName, string basePath, string extension) 
        {
            No = no;
            Dyn = dyn;
            InstrName = instrName;
            BasePath = basePath;
            Extension = extension;
        }
        public int No;
        public string Dyn;
        public string InstrName;
        public string BasePath;
        public string Extension;
        public string FilePath => $"{BasePath}{Dyn}-{InstrName}.{Extension}";
    }
    public static class InstrumentFileHelper
    {
        public static List<Layer> ViolaLayers = 
            [
                new (0, "P", "Viole", @"C:/Users/slonj/Desktop/Samples/Bowed_", "wav"),
                new (1, "MP", "Viole", @"C:/Users/slonj/Desktop/Samples/Bowed_", "wav"),
                new (2, "MF", "Viole", @"C:/Users/slonj/Desktop/Samples/Bowed_", "wav"),
                new (3, "F", "Viole", @"C:/Users/slonj/Desktop/Samples/Bowed_", "wav"),
                new (4, "FF", "Viole", @"C:/Users/slonj/Desktop/Samples/Bowed_", "wav"),
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
            {InstrumentFileHelper.ViolaLayers.Select(l => l.ToFileLoad()).JoinToBlock(18)}

            kMod chnget ""{instrNo.ToModulatorStr()}""
            
            {InstrumentFileHelper.ViolaLayers.Select(l => l.ToCalculatedDynamics(InstrumentFileHelper.ViolaLayers.Count())).JoinToBlock(18)}

            aOutL = 0.1 * ({InstrumentFileHelper.ViolaLayers.ToOutput("Left")})
            aOutR = 0.1 * ({InstrumentFileHelper.ViolaLayers.ToOutput("Right")})
            
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

    public static class InstrumentHelper
    {
        public static string ToLeftAudioOut(this int instrNo) => $"audio_out_left_{instrNo}";
        public static string ToRightAudioOut(this int instrNo) => $"audio_out_right_{instrNo}";
        public static string ToModulatorStr(this int instrNo) => $"MODULATOR_{instrNo}";
        public static int ToEnvelopeInstrument(this int instrNo) => instrNo + 10;
        public static string JoinToBlock(this IEnumerable<string> values, int indent) => "\n" + string.Join("\n", values.Select(v => "".PadLeft(indent) + v));
        public static string ToFileLoad(this Layer layer)
        {
            var str = @$"
                iSampleLen = p4
                ;;;;; TODO smoothly loop
                kTimeEnv_{layer.Dyn} linseg 0, iSampleLen, iSampleLen

                aLeft_{layer.Dyn}_wav1, aRight_{layer.Dyn}_wav1 diskin2 ""{layer.FilePath}"", 1, iSampleLen

                aLeft_{layer.Dyn} = aLeft_{layer.Dyn}_wav1
                aRight_{layer.Dyn} = aRight_{layer.Dyn}_wav1
                ";
            return str;
        }

        public static string ToKMod(this int instrNo) => $"kMod_{instrNo}";

        /// <summary>
        /// kDiff_{layer.No}
        /// </summary>
        /// <param name="layer"></param>
        /// <param name="instrNo"></param>
        /// <param name="nLayers"></param>
        /// <returns></returns>
        public static string ToCalculatedDynamics(this Layer layer, int nLayers)
        {
            var res = @$"            
            kDiff_{layer.No} = kMod * {nLayers - 1} - {layer.No}
            kRes_{layer.No} init 0
            if kDiff_{layer.No} > 1 || kDiff_{layer.No} < -1 then 
                kRes_{layer.No} = 0
            elseif kDiff_{layer.No} >= 0 then
                kRes_{layer.No} =   (1 - kDiff_{layer.No})
            elseif kDiff_{layer.No} < 0 then
                kRes_{layer.No} = - (1 - kDiff_{layer.No})
            endif
            ";
            return res;
        }

        public static string ToOutput(this List<Layer> layers, string side)
        {
            //0.2*aLeft_P * (1-kMod) + 0.2*aLeft_F * kMod

            var res = string.Join(" + ", layers.Select(l => $"a{side}_{l.Dyn} * kRes_{l.No}"));
            return res;
        }

    }
}
