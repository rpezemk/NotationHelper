using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace MusicDataModel.FlowTypes
{
    public abstract class AMenuNode
    {
        public string Name { get; set; }

        public override string ToString() => Name;
    }

    public class SubMenu : AMenuNode 
    {
        public List<AMenuNode> Children { get; set; } = new List<AMenuNode>();
    }

    public class ActionItem : AMenuNode 
    {
        public Action Action { get; set; }
    }

    public class VirtualMenu : SubMenu
    {

    }

}
