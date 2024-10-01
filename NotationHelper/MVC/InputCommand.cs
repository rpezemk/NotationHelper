using System.Windows.Input;
using NotationHelper.MVC.Basics;

namespace NotationHelper.MVC
{

    public class InputCommand
    {
        public InputCommand(string name, params MergedKey[] mergedKeys)
        {
            Name = name;
            Keys.AddRange(mergedKeys);
        }
        public string Name { get; set; }

        public List<AStrangeAction> Actions { get; set; } = new List<AStrangeAction>();
        public List<AKey> Keys { get; set; } = new List<AKey>();
        public string V { get; }
        public InputCommand AppendAction(Action action) { Actions.Add(new StrangeAction(action)); return this; }
        public InputCommand AppendAction<T>(Action<T> action) { Actions.Add(new StrangeAction<T>(action)); return this; }
        public InputCommand AppendAction<T1, T2>(Action<T1, T2> action) { Actions.Add(new StrangeAction<T1, T2>(action)); return this; }
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

    }

    public abstract class AStrangeAction 
    {
        public abstract void TryRun(params object[] objects);
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
    }
}
