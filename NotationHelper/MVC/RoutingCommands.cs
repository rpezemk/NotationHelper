using System.Reflection;
using System.Windows;
using System.Windows.Input;
using NotationHelper.MVC.Basics;

namespace NotationHelper.MVC
{
    public static class RoutingCommands
    {
        public static List<Key> AllKeys = Enum.GetValues<Key>().ToList();

        public static MergedKey Escape = new MergedKey(Key.Escape);
        public static MergedKey Shift = new MergedKey(Key.LeftShift, Key.RightShift);
        public static MergedKey Ctrl = new MergedKey(Key.LeftCtrl, Key.RightCtrl);
        public static MergedKey Alt = new MergedKey(Key.LeftAlt, Key.RightAlt);
        public static MergedKey AKey = new MergedKey(Key.A);

        #region KEYBOARD ACTIONS
        public static InputCommand EscAction => new InputCommand("ESC", Escape).AppendAction(() => SelectedBarsCollection.UnSelectAll());
        public static InputCommand SelectAllAction => new InputCommand("SELECT_ALL", Ctrl, AKey).AppendAction(() => MessageBox.Show("SELECT_ALL"));
        #endregion


        #region KEYBOARD MODES
        public static InputMode SelectSpecific => new InputMode("INDIVIDUAL_SELECTION", Ctrl);
        public static InputMode SelectMeasures => new InputMode("RANGE_SELECTION", Shift);
        public static InputMode SelectBarColumn => new InputMode("BAR_COLUMN_SELECTION", Ctrl, Shift);
        public static InputMode SelectVisibleLine => new InputMode("VISIBLE_LINE", Alt);

        #endregion

        private static List<InputCommand> allKbdCommands;
        public static List<InputCommand> AllKbdCommands
        {
            get
            {
                if (allKbdCommands != null)
                    return allKbdCommands;

                var kbdModes = typeof(RoutingCommands)
                    .GetProperties(BindingFlags.Public | BindingFlags.Static)
                    .Where(prop => prop.PropertyType.IsAssignableTo(typeof(InputCommand)))
                    .Select(prop => prop.GetValue(null))
                    .Where(v => v is InputCommand)
                    .Select(v => v as InputCommand).ToList();

                allKbdCommands = kbdModes;
                return allKbdCommands;
            }
        }

        private static List<InputMode> allKbdModes;
        public static List<InputMode> AllKbdModes
        {
            get
            {
                if (allKbdCommands != null)
                    return allKbdModes;

                var kbdModes = typeof(RoutingCommands)
                    .GetProperties(BindingFlags.Public | BindingFlags.Static)
                    .Where(prop => prop.PropertyType.IsAssignableTo(typeof(InputMode)))
                    .Select(prop => prop.GetValue(null))
                    .Where(v => v is InputMode)
                    .Select(v => v as InputMode).ToList();

                allKbdModes = kbdModes;
                return allKbdModes;
            }
        }
        public static List<Key> GetAllPressedKeys() => AllKeys.Skip(1).Where(k => Keyboard.IsKeyDown(k)).ToList();


        public static void ReportInput() { }
        public static void ReportInput<T>(T argT) { }
        public static void ReportInput<T1, T2>(T1 argT1,T2 argT2 ) { }
    }



}
