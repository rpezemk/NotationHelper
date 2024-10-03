using MusicDataModel.Helpers;
using System.Reflection;
using System.Windows.Input;

namespace NotationHelper.MVC.Basics
{
    
    public class MergedKey
    {
        public List<Key> Keys = new List<Key>();
        public MergedKey(params Key[] keys)
        {
            Keys.AddRange(keys);
        }

        public bool IsMatch(Key inputKey)
        {
            var res = Keys.Contains(inputKey);
            return res;
        }
    }

}
