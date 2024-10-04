using System.Windows.Input;

namespace NotationHelper.MVC.Basics
{
    public class AKey
    {
        
    }
    public class MergedKey : AKey
    {
        public List<Key> Keys = new List<Key>();
        public MergedKey(params Key[] keys)
        {
            Keys.AddRange(keys);
        }
    }

    public class MouseKey : AKey
    {
        public MouseButton Button;
        public MouseKey(MouseButton mouseButton)
        {
            Button = mouseButton;
        }
    }
}
