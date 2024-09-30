using System.Reflection;
using System.Windows.Input;

namespace NotationHelper.MVC
{
    public static class ModeHelper
    {
        public static List<Key> AllKeys = Enum.GetValues<Key>().ToList();

        public static MergedKeys Escape = new MergedKeys(Key.Escape);
        public static MergedKeys Shift = new MergedKeys(Key.LeftShift, Key.RightShift);
        public static MergedKeys Ctrl = new MergedKeys(Key.LeftCtrl, Key.RightCtrl);
        public static MergedKeys Alt = new MergedKeys(Key.LeftAlt, Key.RightAlt);

        #region MODES
        public static KbdMode EscMode => new KbdMode("ESC", Escape);
        public static KbdMode SelectSpecific => new KbdMode("INDIVIDUAL", Ctrl);
        public static KbdMode SelectMeasures => new KbdMode("RANGE", Shift);
        public static KbdMode SelectBarColumn => new KbdMode("BAR_COLUMN", Ctrl, Shift);
        public static KbdMode SelectVisibleLine => new KbdMode("VISIBLE_LINE", Alt);

        #endregion



        private static List<KbdMode> allKbdModes;
        public static List<KbdMode> AllKbdModes
        {
            get
            {
                if (allKbdModes != null)
                    return allKbdModes;

                var kbdModes = typeof(ModeHelper)
                    .GetProperties(BindingFlags.Public | BindingFlags.Static)
                    .Where(prop => prop.PropertyType.IsAssignableTo(typeof(KbdMode)))
                    .Select(prop => prop.GetValue(null))
                    .Where(v => v is KbdMode)
                    .Select(v => v as KbdMode).ToList();

                allKbdModes = kbdModes;
                return allKbdModes;
            }
        }

        public static List<Key> GetAllPressedKeys() => AllKeys.Skip(1).Where(k => Keyboard.IsKeyDown(k)).ToList();
        public static KbdMode? GetCurrMode()
        {
            var pressed = GetAllPressedKeys();
            var maybeMode = AllKbdModes.FirstOrDefault(mode => mode.IsCurrentMode());
            return maybeMode;
        }
    }

    /// <summary>
    /// Class for mergin 'same' keys (in ex. right & left shift/control), also single keys....
    /// </summary>
    public class MergedKeys
    {
        public List<Key> Keys = new List<Key>();
        public MergedKeys(params Key[] keys)
        {
            Keys.AddRange(keys);
        }
        public bool Match(Key key) => Keys.Any(k => Keyboard.IsKeyDown(k));
    }


}
