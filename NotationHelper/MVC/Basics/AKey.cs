using System.Reflection;
using System.Windows.Input;

namespace NotationHelper.MVC.Basics
{
    public abstract class AKey
    {
    }

    public class MouseKey : AKey
    {
        public List<MouseButton> MouseButtons { get; set; } = new List<MouseButton>();
        public MouseKey(params MouseButton[] buttons) { MouseButtons = buttons.ToList(); }
    }
    /// <summary>
    /// Class for mergin 'same' keys (in ex. right & left shift/control), also single keys....
    /// </summary>
    public class MergedKey : AKey
    {
        public List<Key> Keys = new List<Key>();
        public MergedKey(params Key[] keys)
        {
            Keys.AddRange(keys);
        }
        public bool Match(Key key) => Keys.Any(k => Keyboard.IsKeyDown(k));
    }

    public class KbdKey : AKey
    {
        public List<Key> Keys { get; set; } = new List<Key>();
        public KbdKey(params Key[] keys) { Keys = keys.ToList(); }
    }

}
