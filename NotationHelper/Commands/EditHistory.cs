using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NotationHelper.Commands.Base;

namespace NotationHelper.Commands
{
    public class EditHistory
    {
        public List<AEditCommand> Commands { get; set; } = new List<AEditCommand>();

    }
}
