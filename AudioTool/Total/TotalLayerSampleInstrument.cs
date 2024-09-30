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
            iDuration      init p3
            iskiptim       init p4
            iSampleLen     init p5
            iBegLen    min iDuration, iSampleLen
            iEndLen    min iDuration, iSampleLen
            iPreEnd    max 0.0001, iDuration - iEndLen
            iFadeTime = iBegLen / 2
            iStableFillTime max 0, (iDuration - 2*iFadeTime)
            iFillFreq = 1/iSampleLen
            iNWholeFills = floor(iDuration / iSampleLen) * 4
            iLastFillDur = iDuration - (iNWholeFills * iSampleLen)
            iEndSkip = max(0, iSampleLen - iDuration)
            iLegatoMark init p6 
            

            ;CONTROL ENVELOPES
            kTimeEnv linseg 0, iDuration, iDuration
            
            kBegEnv linseg 0, 0.1, 1, iBegLen-0.2, .7, 0.1, 0
            kEndEnv linseg 0, 0.1, 0.9, iPreEnd, .9, iEndLen-0.2, 1, 0.1, 0

            kEndTrigger init 0
            if kEndTrigger = 0 then
                if kTimeEnv >= iPreEnd then
                    kEndTrigger = 1
                    ;printks ""kEndTrigger = 1\n"", 0
                endif
            endif
            kRand randh 1, 0.5, 1  ; 
            kTestMetro metro iFillFreq * 4 + kRand*0.2
            kFillCnt init 0
            if kTestMetro == 1 then
                if kFillCnt < iNWholeFills - 1 then
                    event ""i"", p1+60, 0, iSampleLen, iskiptim
                    kFillCnt = kFillCnt + 1
                endif
            endif
            
            {Layers.Select(l => l.ToWholeSample()).JoinToBlock(18)}

            kModChan chnget ""{instrNo.ToModulatorStr()}""
            iDelay = 0.01
            kSupport1 linseg 0, iDelay, 1, iDuration - 2*iDelay, 1, iDelay, 0
            kSupport2 linseg 0, iDuration * 0.33, 1, iDuration * 0.66, 0
            kMod = ((kSupport1 + kSupport2*0.5)*0.1 - 0.1)  + + kModChan            
            {Layers.Select(l => l.ToCalculatedDynamics(InstrumentFileHelper.ViolaLayers.Count())).JoinToBlock(18)}

            aLeft = 0.3 * ({LayersToOutput("Left")})
            aRight = 0.3 * ({LayersToOutput("Right")})
            aOutL atone aLeft,  230
            aOutR atone aRight, 230 

            aRvbL,aRvbR reverbsc aOutL,aOutR,0,12000
            outs aRvbL - aOutL,aRvbR - aOutR
            gaRvbSendL = gaRvbSendL + (aOutL)
            gaRvbSendR = gaRvbSendR + (aOutR)


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

            aOutL = 0.2 * ({LayersToOutput("Left")})
            aOutR = 0.2 * ({LayersToOutput("Right")})
            
            gaRvbSendL = gaRvbSendL + (aOutL)
            gaRvbSendR = gaRvbSendR + (aOutR)
        endin

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
            return ["i 5 0 300 "];//$"i{instrNo.ToEnvelopeNo()} 0 10 1 ; i99 start at 0, 10 hours long, amp .0001"
        }

        public string LayersToOutput(string side)
        {
            var res = string.Join(" + ", Layers.Select(l => $"a{side}_{l.Dyn} * kRes_{l.Dyn}"));
            return res;
        }

        public void PlaySample(int pitch, double noteDuration, bool legato)
        {
            //1 -- legato
            //0 -- non legato
            Engine.Play(EmitFromInstr(new TotalSampleEvent(0, noteDuration, pitch * SampleSpacing + SampleOffset, SampleLen, legato? 1: 0)));
        }




        public void ApplyDynamics(double period, double dynamics)
        {
            LayerDynamicsEvent dynamicsEvent = new LayerDynamicsEvent(0.1, period, dynamics);
            dynamicsEvent.InstrumentNo = InstrNo + 10;//MODULATOR
            Engine.Play(dynamicsEvent);
        }
    }
}
