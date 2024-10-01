namespace NotationHelper.MVC
{
    public class KbdAction : AKbdAction
    {
        public KbdAction(string ActionName, params MergedKeys[] keys) : base(ActionName, keys)
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


    public class KbdActionZero : AKbdAction
    {
        public Action Action;
        public KbdActionZero(string ActionName, Action action, params MergedKeys[] keys) : base(ActionName, keys)
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

    public class KbdAction<T> : AKbdAction
    {
        public Action<T> Action;
        public KbdAction(string ActionName, Action<T> action, params MergedKeys[] keys) : base(ActionName, keys)
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
    public class KbdAction<T1, T2> : AKbdAction
    {
        public Action<T1, T2> Action;
        public KbdAction(string ActionName, Action<T1, T2> action, params MergedKeys[] keys) : base(ActionName, keys)
        {
            Action = action;
        }

        public override bool CanAccept(params object[] objects)
        {
            if (objects.Length != 2)
                return false;
            return objects[0] is T1 && objects[1] is T2;
        }

        public override void RunAction(params object[] objects)
        {
            Action.Invoke((T1)objects[0], (T2)objects[1]);
        }
    }
    public class KbdAction<T1, T2, T3> : AKbdAction
    {
        public Action<T1, T2, T3> Action;
        public KbdAction(string ActionName, Action<T1, T2, T3> action, params MergedKeys[] keys) : base(ActionName, keys)
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
}
