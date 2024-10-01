using System.Reflection;
using System.Windows.Input;

namespace NotationHelper.MVC
{
    public static class RoutingCommands
    {
        public static List<Key> AllKeys = Enum.GetValues<Key>().ToList();

        public static MergedKey Escape = new MergedKey() { Keys = [Key.Escape] };
        public static MergedKey Shift = new MergedKey() { Keys = [Key.LeftShift, Key.RightShift] };
        public static MergedKey Ctrl = new MergedKey() { Keys = [Key.LeftCtrl, Key.RightCtrl] };
        public static MergedKey Alt = new MergedKey() { Keys = [Key.LeftAlt, Key.RightAlt] };
        public static MergedKey AKey = new MergedKey() { Keys = [Key.A] };

        #region KEYBOARD ACTIONS
        public static AKbdAction EscAction => new KbdAction() 
        { 
            ActionName = "", 
            KeysGroups = [Escape,]
        }
        .AppendAction(() => SelectedBarsCollection.UnSelectAll());
        public static AKbdAction SelectAllAction => new KbdAction() 
        {
            ActionName = "",
            KeysGroups = [Ctrl, AKey]
        };
        #endregion


        #region KEYBOARD MODES
        public static AKbdAction SelectSpecific => new KbdAction() 
        {
            ActionName = "",
            KeysGroups = [Shift]
        };
        public static AKbdAction SelectMeasures => new KbdAction() 
        { 
            ActionName = "",
            KeysGroups = [Ctrl]
        };
        public static AKbdAction SelectBarColumn => new KbdAction() 
        {
            ActionName = "",
            KeysGroups = [Ctrl, Shift]
        };
        public static AKbdAction SelectVisibleLine => new KbdAction() 
        {
            ActionName = "",
            KeysGroups = [Ctrl, Alt]
        };

        #endregion

        private static List<KbdAction> allKbdModes;
        public static List<KbdAction> AllKbdModes
        {
            get
            {
                if (allKbdModes != null)
                    return allKbdModes;

                var kbdModes = typeof(RoutingCommands)
                    .GetProperties(BindingFlags.Public | BindingFlags.Static)
                    .Where(prop => prop.PropertyType.IsAssignableTo(typeof(KbdAction)))
                    .Select(prop => prop.GetValue(null))
                    .Where(v => v is KbdAction)
                    .Select(v => v as KbdAction).ToList();

                allKbdModes = kbdModes;
                return allKbdModes;
            }
        }

        public static List<Key> GetAllPressedKeys() => AllKeys.Skip(1).Where(k => Keyboard.IsKeyDown(k)).ToList();
        public static KbdAction? GetCurrMode()
        {
            var pressed = GetAllPressedKeys();
            var maybeMode = AllKbdModes.FirstOrDefault(mode => mode.IsCurrentAction());
            return maybeMode;
        }
    }

    /// <summary>
    /// Class for mergin 'same' keys (in ex. right & left shift/control), also single keys....
    /// </summary>
    public abstract class AMergedKey
    {
        public List<Key> Keys = new List<Key>();
        public AMergedKey(params Key[] keys)
        {
            Keys.AddRange(keys);
        }
        public abstract bool Match(Key key);
    }

    public class MouseInput : AMergedKey
    {
        public override bool Match(Key key)
        {
            throw new NotImplementedException();
        }
    }
    public class MergedKey : AMergedKey
    {
        public override bool Match(Key key) => Keys.Any(k => Keyboard.IsKeyDown(k));
    }
}
