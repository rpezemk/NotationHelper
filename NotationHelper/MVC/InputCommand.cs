using System;
using System.Windows.Input;
using MusicDataModel;
using MusicDataModel.Helpers;
using NotationHelper.Commands.Base;
using NotationHelper.MVC.Basics;

namespace NotationHelper.MVC
{
    public class InputCommand
    {
        public InputCommand(string name, params AKey[] mergedKeys)
        {
            Name = name;
            Keys.AddRange(mergedKeys);
        }
        public string Name { get; set; }
        public bool MatchKeys(List<AKey> checkedMergedKeys)
        {
            if(checkedMergedKeys.Count == 0) return false;
            if(checkedMergedKeys.Count != Keys.Count) return false;
            var sameSet = checkedMergedKeys.IsSameSet(Keys);
            if(!sameSet) return false;

            var res = checkedMergedKeys.Last() == Keys.Last();
            return res;
        }
        public List<AStrangeAction> StrangeActions { get; set; } = new List<AStrangeAction>();
        public List<AKey> Keys { get; set; } = new List<AKey>();

        public object TObj;
        
        public InputCommand Append<T, T2>(T cmd, Func<T2> getSetFunc) where T : AEditCommand<T2>, new()
        {
            StrangeActions.Add(new StrangeAction(
                () => 
                {
                    var whatIsThis = getSetFunc.Invoke();
                    cmd.Accept(whatIsThis);
                    cmd.Execute();
                }));
            return this;
        }

        public InputCommand AfterAll(Action action) { StrangeActions.Add(new StrangeAction(action)); return this; }

        public InputCommand AppendAction(Action action) { StrangeActions.Add(new StrangeAction(action)); return this; }
        public InputCommand AppendAction<T>(Action<T> action) { StrangeActions.Add(new StrangeAction<T>(action)); return this; }
        public InputCommand AppendAction<T1, T2>(Action<T1, T2> action) { StrangeActions.Add(new StrangeAction<T1, T2>(action)); return this; }

        public void Execute(params object[] objects)
        {
            foreach(var act in StrangeActions)
            {
                act.TryRun(objects);
            }
        }

        internal bool MatchArguments(object[] objects)
        {
            var find = StrangeActions.FirstOrDefault();
            if (find == null)
                return false;
            var res = find.CanRun(objects);
            return res;
        }
    }


    public class InputMode
    {
        public InputMode(string name, params MergedKey[] mergedKeys)
        {
            Name = name;
            Keys.AddRange(mergedKeys);
        }
        public string Name { get; set; }
        public List<AStrangeAction> Actions { get; set; } = new List<AStrangeAction>();
        public List<AKey> Keys { get; set; } = new List<AKey>();
        public bool Match(List<AKey> checkedMergedKeys)
        {
            if (checkedMergedKeys.Count == 0) return false;
            if (checkedMergedKeys.Count != Keys.Count) return false;
            var sameSet = checkedMergedKeys.IsSameSet(Keys);
            if (!sameSet) return false;

            var res = checkedMergedKeys.Last() == Keys[0];
            return res;
        }

    }

    public abstract class AStrangeAction 
    {
        public abstract void TryRun(params object[] objects);
        public abstract bool CanRun(params object[] objects);
    }

    public class StrangeAction : AStrangeAction
    {
        public Action Action { get; set; }

        public StrangeAction(Action action)
        {
            this.Action = action;
        }

        public override void TryRun(params object[] objects)
        {
            if (objects == null || objects.Length == 0)
                Action.Invoke();
        }

        public override bool CanRun(params object[] objects)
        {
            return (objects == null || objects.Length == 0);
            throw new NotImplementedException();
        }
    }
    public class StrangeAction<T> : AStrangeAction
    {
        public Action<T> Action { get; set; }

        public StrangeAction(Action<T> action)
        {
            this.Action = action;
        }

        public override void TryRun(params object[] objects)
        {
            if (objects == null || objects.Length != 1)
                return;
            if (objects[0] == null || objects[0].GetType() != typeof(T))
                return;

            Action.Invoke((T)objects[0]);
        }

        public override bool CanRun(params object[] objects)
        {
            if (objects == null || objects.Length != 1)
                return false;
            if (objects[0] == null || objects[0].GetType() != typeof(T))
                return false;
            return true;
        }
    }
    public class StrangeAction<T1,T2> : AStrangeAction
    {
        public Action<T1, T2> Action { get; set; }
        public StrangeAction(Action<T1, T2> action)
        {
            this.Action = action;
        }
        public override void TryRun(params object[] objects)
        {
            if (objects == null || objects.Length != 2)
                return;
            if (objects[0] == null || objects[0].GetType() != typeof(T1) || objects[1].GetType() != typeof(T2))
                return;

            Action.Invoke((T1)objects[0], (T2)objects[1]);
        }

        public override bool CanRun(params object[] objects)
        {
            if (objects == null || objects.Length != 2)
                return false;
            if (objects[0] == null || objects[0].GetType() != typeof(T1) || objects[1].GetType() != typeof(T2))
                return false;
            return true;
        }
    }
}
