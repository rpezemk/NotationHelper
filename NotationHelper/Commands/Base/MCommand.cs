using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotationHelper.Commands.Base
{
    public enum CommandStateEnum
    {
        NotExecuted,
        Executed
    }

    public abstract class AEditCommand
    {
        public CommandStateEnum State { get; set; }
        public abstract void Execute();

    }

    public abstract class AEditCommand<T, T2> : AEditCommand where T2 : AEditCommand
    {
        private T value;
        public AEditCommand(T value) { this.value = value; }

        public override void Execute() { Execute(value); }

        protected abstract void Execute(T value);

        public abstract T2 Emit(T o);
    }

    public abstract class ANestedCommand<T1, T2> : AEditCommand where T2 : AEditCommand<T1, T2>
    {
        protected List<T1> arguments = new List<T1>();
        protected List<T2> subCommands = new List<T2>();
        public override void Execute()
        {
            throw new NotImplementedException();
        }
        protected abstract void Execute(List<T1> arguments);
    }
}
