using AudioTool.CsEvents;
using AudioTool.Helpers;
using AudioTool.InstrumentBase;
using AudioTool.Layered;

namespace AudioTool.Total
{
    public class TotalLayerSampleInstrument : AScriptedInstrument<LayerSampleEvent>
    {
        public double SampleLen;
        public List<TotalLayer> Layers { get; set; } = new List<TotalLayer>();
        public TotalLayerSampleInstrument(string name, List<TotalLayer> layers) : base(name)
        {
            Layers = layers;
            SampleLen = Layers[0].SampleLen;
        }
        public override string GetInstrumentScript(int instrNo, string instrName)
        {
            var part =
                @$"
        ;##############################################################
        ;#####         {instrNo} {instrName}                    #######      
        ;##############################################################
        instr {instrNo}; WHOLE SAMPLE
            kTrig init 1
            if kTrig == 1 then
                printks ""TOTAL SAMPLE INSTR \n"", 0
                printks ""P1   instrNo: %f\n"", 0, p1
                printks ""P2 startTime: %f\n"", 0, p2
                printks ""P3  duration: %f\n"", 0, p3
                printks ""P4 pitchSkip: %f\n"", 0, p4
                printks ""P5 sampleLen: %f\n"", 0, p5
                kTrig = 0
            endif
            iDuration  init p3
            iPitchSkip init p4
            iSampleLen init p5
            iBegLen min iDuration, iSampleLen
            iEndLen min iDuration, iSampleLen
            iPreEnd = max(0, iDuration - iEndLen)
            kEndTrigger init 0
            ;control envelopes
            kTimeEnv linseg 0, iDuration, iDuration
            kBegEnv linseg 1, iBegLen, 0
            kEndEnv linseg 0, iPreEnd, 0, iEndLen, 1
            if kEndTrigger = 0 then
                if kTimeEnv >= iPreEnd then
                    kEndTrigger = 1
                    printks ""kEndTrigger = 1\n"", 0
                endif
            endif
            ;

            {Layers.Select(l => l.ToWholeSample()).JoinToBlock(18)}

            kMod chnget ""{instrNo.ToModulatorStr()}""
            ;printks ""kMod Value: %f\n"", 0, kMod
            
            {Layers.Select(l => l.ToCalculatedDynamics(InstrumentFileHelper.ViolaLayers.Count())).JoinToBlock(18)}

            aOutL = 0.1 * ({LayersToOutput("Left")})
            aOutR = 0.1 * ({LayersToOutput("Right")})
            
            outs aOutL, aOutR
        endin

        ;DYNAMICS_MODULATOR
        instr {instrNo.ToEnvelopeInstrument()}
                kTrig init 1
                if kTrig == 1 then
                    printks ""P1 kEnv Value: %f\n"", 0, p1
                    printks ""P2 kEnv Value: %f\n"", 0, p2
                    printks ""P3 kEnv Value: %f\n"", 0, p3
                    printks ""P4 kEnv Value: %f\n"", 0, p4
                    kTrig = 0
                endif
                iCurr chnget ""{instrNo.ToModulatorStr()}""
                kEnv linseg iCurr, p3, p4
                chnset kEnv, ""{instrNo.ToModulatorStr()}""
                chnset kEnv, ""{instrNo.ToModulatorStr()}""
        endin
";
            return part;
        }



        public override List<string> GetEventsScript(int instrNo, string instrName)
        {
            return [];//$"i{instrNo.ToEnvelopeNo()} 0 10 1 ; i99 start at 0, 10 hours long, amp .0001"
        }

        public string LayersToOutput(string side)
        {
            //0.2*aLeft_P * (1-kMod) + 0.2*aLeft_F * kMod

            var res = string.Join(" + ", Layers.Select(l => $"a{side}_{l.Dyn} * kRes_{l.Dyn}"));
            return res;
        }

        public void PlaySample(int pitch, double noteDuration)
        {
            Engine.Play(EmitFromInstr(new TotalSampleEvent(0, noteDuration, pitch * SampleLen, SampleLen)));
        }


        public void PlaySeparatedNote(int pitch, double noteDuration)
        {
            List<ScheduletEvent> events = new List<ScheduletEvent>();
            Task.Run(() =>
            {
                PlaySample(pitch, noteDuration);
            });
        }

        public void ApplyDynamics(double period, double dynamics)
        {
            LayerDynamicsEvent dynamicsEvent = new LayerDynamicsEvent(0.1, period, dynamics);
            dynamicsEvent.InstrumentNo = InstrNo + 10;//MODULATOR
            Engine.Play(dynamicsEvent);
        }
    }
}
