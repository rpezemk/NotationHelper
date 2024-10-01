namespace NotationHelper.MVC
{
    internal static class KbdModeHelpers
    {

        public static KbdModeZero AppendAction(this KbdMode mode, Action action) =>
            new KbdModeZero(mode.ModeName, action, mode.KeysGroups.ToArray());

        public static KbdMode<T> AppendAction<T>(this KbdMode mode, Action<T> action) =>
            new KbdMode<T>(mode.ModeName, action, mode.KeysGroups.ToArray());

        public static KbdMode<T1, T2> AppendAction<T1, T2>(this KbdMode mode, Action<T1, T2> action) =>
            new KbdMode<T1, T2>(mode.ModeName, action, mode.KeysGroups.ToArray());


        public static KbdActionZero AppendAction(this KbdAction mode, Action action) =>
            new KbdActionZero(mode.ActionName, action, mode.KeysGroups.ToArray());

        public static KbdAction<T> AppendAction<T>(this KbdAction mode, Action<T> action) =>
            new KbdAction<T>(mode.ActionName, action, mode.KeysGroups.ToArray());

        public static KbdAction<T1, T2> AppendAction<T1, T2>(this KbdAction mode, Action<T1, T2> action) =>
            new KbdAction<T1, T2>(mode.ActionName, action, mode.KeysGroups.ToArray());
    }
}