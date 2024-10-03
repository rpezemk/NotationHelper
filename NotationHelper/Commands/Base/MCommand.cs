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

    public abstract class AEditCommand<T> : AEditCommand
    {
        public void Accept(T obj) { Value = obj; }
        public T Value;
    }
}
