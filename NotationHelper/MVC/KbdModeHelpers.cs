using System.Windows.Input;

namespace NotationHelper.MVC
{
    internal static class KbdModeHelpers
    {
        public static KbdActionNoArg AppendAction(this AKbdAction mode, Action action) =>
            new KbdActionNoArg() { Action = action, ActionName = mode.ActionName, KeysGroups = mode.KeysGroups };

        public static KbdAction<T> AppendAction<T>(this AKbdAction mode, Action<T> action) =>
            new KbdAction<T>() { Action = action, ActionName = mode.ActionName, KeysGroups = mode.KeysGroups };

        public static KbdAction<T1, T2> AppendAction<T1, T2>(this AKbdAction mode, Action<T1, T2> action) =>
            new KbdAction<T1, T2>() { Action = action, ActionName = mode.ActionName, KeysGroups = mode.KeysGroups  };

        public static AKbdAction Then(this AKbdAction aKbdAction, params Key[] keys)
        {
            aKbdAction.KeysGroups.Add(new MergedKey() { Keys = keys.ToList() });
            return aKbdAction;
        }
    }
}