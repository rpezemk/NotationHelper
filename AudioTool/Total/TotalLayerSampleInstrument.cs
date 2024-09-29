using AudioTool.CsEvents;
using AudioTool.Helpers;
using AudioTool.InstrumentBase;
using AudioTool.Layered;

namespace AudioTool.Total
{
    public class TotalLayerSampleInstrument : AScriptedInstrument<LayerSampleEvent>
    {
        public double SampleLen = 3.4;
        public double SampleSpacing = 4;
        public double SampleOffset = 0.0;
        public List<TotalLayer> Layers { get; set; } = new List<TotalLayer>();
        public TotalLayerSampleInstrument(string name, List<TotalLayer> layers) : base(name)
        {
            Layers = layers;
            //SampleLen = Layers[0].SampleLen;
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
            if kTrig == 99 then
                printks ""TOTAL SAMPLE INSTR \n"", 0
                printks ""P1   instrNo: %f\n"", 0, p1
                printks ""P2 startTime: %f\n"", 0, p2
                printks ""P3  duration: %f\n"", 0, p3
                printks ""P4 pitchSkip: %f\n"", 0, p4
                printks ""P5 sampleLen: %f\n"", 0, p5
                kTrig = 0
            endif
            iDuration  init p3
            iskiptim init p4
            iSampleLen init p5
            iBegLen min iDuration, iSampleLen
            iEndLen min iDuration, iSampleLen
            iPreEnd = max(0.0001, iDuration - iEndLen)
            iFadeTime = iBegLen / 2
            iStableFillTime max 0, (iDuration - 2*iFadeTime)
            iFillFreq = 1/iSampleLen
            
            iNWholeFills = floor(iDuration / iSampleLen)
            iLastFillDur = iDuration - (iNWholeFills * iSampleLen)
            
            iEndSkip = max(0, iSampleLen - iDuration)
            
            kFillCnt init 0


            ;CONTROL ENVELOPES
            kTimeEnv linseg 0, iDuration, iDuration
            kBegEnv linseg 1, iBegLen, 0
            kEndEnv linseg 0, iPreEnd, 0, iEndLen, 1
            kEndTrigger init 0
            if kEndTrigger = 0 then
                if kTimeEnv >= iPreEnd then
                    kEndTrigger = 1
                    ;printks ""kEndTrigger = 1\n"", 0
                endif
            endif
            
            kFillEnvelope linseg 0, iFadeTime, 1, iStableFillTime, 1, iFadeTime, 0


            kPhasor1 init 0
            kPhasor2 init 0
            kPhasor_prev1 = kPhasor1
            kPhasor_prev2 = kPhasor2
            kPhasor1 phasor iFillFreq
            kPhasor2 = frac(kPhasor1 + 0.5)
            kFastPhasor phasor 2
            kFillWave1 = 1 - abs(2 * kPhasor1 - 1) ; 
            kFillWave2 = 1 - abs(2 * kPhasor2 - 1) ; 

            kWaveTrig1 init 0
            kWaveTrig2 init 0

            if kTimeEnv == 0 || kPhasor_prev1 - kPhasor1 > 0.5 then
                kWaveTrig1 = 1
            else 
                kWaveTrig1 = 0
            endif

            if kPhasor_prev2 - kPhasor2 > 0.5 then
                kWaveTrig2 = 1
            else 
                kWaveTrig2 = 0
            endif
            

            if kWaveTrig1 == 1 || kWaveTrig2 == 1 then
                if kFillCnt < iNWholeFills then
                    event ""i"", p1+60, 0, iSampleLen, iskiptim
                    kFillCnt = kFillCnt + 1
                endif
            endif

            {Layers.Select(l => l.ToWholeSample()).JoinToBlock(18)}

            kMod = 0.5; chnget ""{instrNo.ToModulatorStr()}""
            ;printks ""kMod Value: %f\n"", 0, kMod
            
            {Layers.Select(l => l.ToCalculatedDynamics(InstrumentFileHelper.ViolaLayers.Count())).JoinToBlock(18)}

            aOutL = 0.3 * ({LayersToOutput("Left")})
            aOutR = 0.3 * ({LayersToOutput("Right")})
                   
            outs aOutL, aOutR

        endin



        instr {instrNo + 60}
            kTrig init 1
            if kTrig == 1 then
                printks ""P1 instrNo : %f\n"", 0, p1
                printks ""P2 start   : %f\n"", 0, p2
                printks ""P3 durat   : %f\n"", 0, p3
                printks ""P4 skiptim : %f\n"", 0, p4
                kTrig = 0
            endif
            iDuration  init p3
            iskiptim init p4
            kFillEnv linseg 0, iDuration/2, 1, iDuration/2, 0
            {Layers.Select(l => l.ToSimpleEnvelope()).JoinToBlock(18)}

            kMod = 0.5;kMod chnget ""{instrNo.ToModulatorStr()}""
            {Layers.Select(l => l.ToCalculatedDynamics(InstrumentFileHelper.ViolaLayers.Count())).JoinToBlock(18)}

            aOutL = 0.3 * ({LayersToOutput("Left")})
            aOutR = 0.3 * ({LayersToOutput("Right")})
            
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
            var res = string.Join(" + ", Layers.Select(l => $"a{side}_{l.Dyn} * kRes_{l.Dyn}"));
            return res;
        }

        public void PlaySample(int pitch, double noteDuration)
        {
            Engine.Play(EmitFromInstr(new TotalSampleEvent(0, noteDuration, pitch * SampleSpacing + SampleOffset, SampleLen)));
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
