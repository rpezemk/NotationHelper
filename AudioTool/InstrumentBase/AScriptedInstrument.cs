using AudioTool.CsEvents;

namespace AudioTool.InstrumentBase
{
    public abstract class AScriptedInstrument
    {
        public AScriptedInstrument(string name)
        {
            this.name = name;
        }
        public int InstrNo;
        private string name;
        public string Name => name;

        public CsEngine Engine { get; internal set; }

        public abstract string GetInstrumentScript(int instrNo, string instrName);
        public abstract List<string> GetEventsScript(int instrNo, string instrName);
        public ACsEvent EmitFromInstr(ACsEvent tEvent)
        {
            tEvent.SetInstrNo(InstrNo);
            return tEvent;
        }

    }
    public abstract class AScriptedInstrument<TEvent> : AScriptedInstrument where TEvent : ACsEvent
    {
        protected AScriptedInstrument(string name) : base(name)
        {
        }

        public TEvent EmitFromInstr(TEvent tEvent)
        {
            var evt = base.EmitFromInstr(tEvent);
            tEvent.SetInstrNo(InstrNo);
            return tEvent;
        }
    }
}
