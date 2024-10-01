using MusicDataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace NotationHelper.MVC
{
    public class RoutingBuilder
    {
        public List<AKbdAction> AllActions = new List<AKbdAction>();
        public List<AKbdAction> AllModes = new List<AKbdAction>();
        public AKbdAction ActionStart(string actionName, params Key[] keys)
        {
            KbdAction action = new KbdAction();
            action.ActionName = actionName;
            AllActions.Add(action);
            return action;
        }

        public AKbdAction Mode(string actionName, params Key[] keys)
        {
            KbdAction action = new KbdAction();
            action.ActionName = actionName;
            AllActions.Add(action);
            return action;
        }
    }

    public class AllRoutings : RoutingBuilder
    {
        public static AllRoutings Instance => new AllRoutings();
        private AllRoutings() 
        {
            ActionStart("ESCAPE", Key.Escape)
                .AppendAction<MainWindow, KeyEventArgs>(
                (win, kArgs) => 
                {
                    SelectedBarsCollection.UnSelectAll();
                }
                );

             
        }
    }
}
