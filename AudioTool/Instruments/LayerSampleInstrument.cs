using AudioTool.CsEvents;
using AudioTool.Instruments.InstrumentBase;

namespace AudioTool.Instruments
{


    public class LayerSampleInstrument : AScriptedInstrument<LayerSampleEvent>
    {
        public double SampleLen;
        public List<Layer> Layers { get; set; } = new List<Layer>();
        public LayerSampleInstrument(string name, List<Layer> layers) : base(name)
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
            {Layers.Select(l => l.ToWholeSample()).JoinToBlock(18)}

            kMod chnget ""{instrNo.ToModulatorStr()}""
            ;printks ""kMod Value: %f\n"", 0, kMod
            
            {Layers.Select(l => l.ToCalculatedDynamics(InstrumentFileHelper.ViolaLayers.Count())).JoinToBlock(18)}

            aOutL = 0.1 * ({LayersToOutput("Left")})
            aOutR = 0.1 * ({LayersToOutput("Right")})
            
            outs aOutL, aOutR
        endin

        instr {instrNo + 30}; SAMPLE FADE IN FADE OUT
            
            {Layers.Select(l => l.ToFadeInOutSample()).JoinToBlock(18)}

            kMod chnget ""{instrNo.ToModulatorStr()}""
            ;printks ""kMod Value: %f\n"", 0, kMod
            {Layers.Select(l => l.ToCalculatedDynamics(InstrumentFileHelper.ViolaLayers.Count())).JoinToBlock(18)}
            
           
            aOutL = 0.1 * ({LayersToOutput("Left")})
            aOutR = 0.1 * ({LayersToOutput("Right")})
            
            outs aOutL, aOutR
        endin

        instr {instrNo + 40}; one to zero to play beginning
            
            {Layers.Select(l => l.FadeOneToZero()).JoinToBlock(18)}

            kMod chnget ""{instrNo.ToModulatorStr()}""
            ;printks ""kMod Value: %f\n"", 0, kMod
            
            {Layers.Select(l => l.ToCalculatedDynamics(InstrumentFileHelper.ViolaLayers.Count())).JoinToBlock(18)}
            
           
            aOutL = 0.1 * ({LayersToOutput("Left")})
            aOutR = 0.1 * ({LayersToOutput("Right")})
            
            outs aOutL, aOutR
        endin
        instr {instrNo + 50}; zero to one to PLAY END
            
            {Layers.Select(l => l.FadeZeroToOne()).JoinToBlock(18)}

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

        public void PlaySample(int pitch, double duration)
        {
            Engine.Play(EmitFromInstr(new LayerSampleEvent(0, duration, pitch * 4, 1, 0, SampleLen)));
        }

        public void PlayFill(int pitch, double duration)
        {
            //Engine.Play(EmitFromInstr(new LayerSampleEvent(0, duration, pitch * 4, 1, 0, sampleLen)));
            var sdf = EmitFromInstr(new LayerSampleEvent(0, duration, pitch * 4, 1, 0, SampleLen));
            sdf.InstrumentNo += 30;//Filling instrument
            Engine.Play(sdf);
        }

        public void PlayBeginning(int pitch, double duration)
        {
            //Engine.Play(EmitFromInstr(new LayerSampleEvent(0, duration, pitch * 4, 1, 0, sampleLen)));
            var sdf = EmitFromInstr(new LayerSampleEvent(0, duration, pitch * 4, 1, 0, SampleLen));
            sdf.InstrumentNo += 40;//Filling instrument
            Engine.Play(sdf);
        }

        public void PlayEnd(int pitch, double duration)
        {
            //Engine.Play(EmitFromInstr(new LayerSampleEvent(0, duration, pitch * 4, 1, 0, sampleLen)));
            var sdf = EmitFromInstr(new LayerSampleEvent(0, duration, pitch * 4, 1, 0, SampleLen));
            sdf.InstrumentNo += 50;//Filling instrument
            Engine.Play(sdf);
        }


        public void PlaySeparatedNote(int pitch, double duration)
        {
            if (duration < SampleLen)
                duration = SampleLen;
            var nFills = 2 * (int)Math.Floor((duration / SampleLen)) - 1;

            List<ScheduletEvent> events = new List<ScheduletEvent>();
            events.Add(new ScheduletEvent() { Action = () => PlayBeginning(pitch, SampleLen), Duration = SampleLen / 2 });
            for (int i = 0; i < nFills; i++)
                events.Add(new ScheduletEvent() { Action = () => PlayFill(pitch, SampleLen), Duration = SampleLen / 2 });

            events.Add(new ScheduletEvent() { Action = () => PlayEnd(pitch, SampleLen), Duration = SampleLen / 2 });

            Task.Run(() =>
            {
                foreach (var e in events)
                    e.RunWaitin();
            });
        }

        public void ApplyDynamics(double period, double dynamics)
        {
            LayerDynamicsEvent dynamicsEvent = new LayerDynamicsEvent(0.1, period, dynamics);
            dynamicsEvent.InstrumentNo = this.InstrNo + 10;//MODULATOR
            Engine.Play(dynamicsEvent);
        }
    }
}
