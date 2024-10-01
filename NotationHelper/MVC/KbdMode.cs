using System.Windows.Input;

namespace NotationHelper.MVC
{
    public abstract class AKbdMode
    {
        public List<MergedKeys> KeysGroups = new List<MergedKeys>();
        public string ModeName { get; set; }
        public AKbdMode(string modeName, params MergedKeys[] keys)
        {
            KeysGroups.AddRange(keys);
            ModeName = modeName;
        }

        public bool IsCurrentMode()
        {
            var keys = RoutingCommands.GetAllPressedKeys();
            if (keys.Count == 0) return false;
            var res = keys.All(k => KeysGroups.Any(kgr => kgr.Match(k)));
            return res;
        }

        public abstract bool CanAccept(params object[] objects);

        public abstract void RunAction(params object[] objects);
    }

    public class KbdMode : AKbdMode
    {
        public KbdMode(string modeName, params MergedKeys[] keys) : base(modeName, keys)
        {
        }

        public override bool CanAccept(params object[] objects)
        {
            return false;
        }

        public override void RunAction(params object[] objects)
        {
        }
    }

    public class KbdModeZero : AKbdMode
    {
        public Action Action;
        public KbdModeZero(string modeName, Action action, params MergedKeys[] keys) : base(modeName, keys)
        {
            Action = action;
        }

        public override bool CanAccept(params object[] objects)
        {
            return objects.Length == 0;
        }

        public override void RunAction(params object[] objects)
        {
            Action.Invoke();
        }
    }

    public class KbdMode<T> : AKbdMode
    {
        public Action<T> Action;
        public KbdMode(string modeName, Action<T> action, params MergedKeys[] keys) : base(modeName, keys)
        {
            Action = action;
        }

        public override bool CanAccept(params object[] objects)
        {
            if (objects.Length != 1)
                return false;
            return objects[0] != null && objects[0] is T;
        }

        public override void RunAction(params object[] objects)
        {
            Action.Invoke((T)(objects[0]));
        }
    }
    public class KbdMode<T1, T2> : AKbdMode
    {
        public Action<T1, T2> Action;
        public KbdMode(string modeName, Action<T1, T2> action, params MergedKeys[] keys) : base(modeName, keys)
        {
            Action = action;
        }

        public override bool CanAccept(params object[] objects)
        {
            if (objects.Length != 2)
                return false;
            return objects[0] is T1 && objects[1]  is T2;
        }

        public override void RunAction(params object[] objects)
        {
            Action.Invoke((T1)objects[0], (T2)objects[1]);
        }
    }
    public class KbdMode<T1, T2, T3> : AKbdMode
    {
        public Action<T1, T2, T3> Action;
        public KbdMode(string modeName, Action<T1, T2, T3> action, params MergedKeys[] keys) : base(modeName, keys)
        {
            Action = action;
        }

        public override bool CanAccept(params object[] objects)
        {
            if (objects.Length != 3)
                return false;
            return objects[0] is T1 && objects[1] is T2 && objects[2] is T3;
        }

        public override void RunAction(params object[] objects)
        {
            Action.Invoke((T1)objects[0], (T2)objects[1], (T3)objects[2]);
        }
    }

    public abstract class AKbdAction
    {
        public List<MergedKeys> KeysGroups = new List<MergedKeys>();
        public string ActionName { get; set; }
        public AKbdAction(string ActionName, params MergedKeys[] keys)
        {
            KeysGroups.AddRange(keys);
            ActionName = ActionName;
        }

        public bool IsCurrentAction()
        {
            var keys = RoutingCommands.GetAllPressedKeys();
            if (keys.Count == 0) return false;
            var res = keys.All(k => KeysGroups.Any(kgr => kgr.Match(k)));
            return res;
        }

        public abstract bool CanAccept(params object[] objects);

        public abstract void RunAction(params object[] objects);
    }

}
