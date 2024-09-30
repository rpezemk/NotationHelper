namespace NotationHelper.MVC
{
    internal static class KbdModeHelpers
    {

        public static AKbdMode AppendAction(this KbdMode mode, Action action) =>
            new KbdModeZero(mode.ModeName, action, mode.KeysGroups.ToArray());

        public static KbdMode<T> AppendAction<T>(this KbdMode mode, Action<T> action) =>
            new KbdMode<T>(mode.ModeName, action, mode.KeysGroups.ToArray());

        public static KbdMode<T1, T2> AppendAction<T1, T2>(this KbdMode mode, Action<T1, T2> action) =>
            new KbdMode<T1, T2>(mode.ModeName, action, mode.KeysGroups.ToArray());
    }
}