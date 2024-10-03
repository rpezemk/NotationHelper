using System.Collections.ObjectModel;
using System.Reflection;
using System.Windows;
using System.Windows.Input;
using MusicDataModel.Helpers;
using NotationHelper.MVC.Basics;

namespace NotationHelper.MVC
{
    public static class CommandResolver
    {
        public static ObservableCollection<MergedKey> PressedKeys = new ObservableCollection<MergedKey>();

        public static List<Key> AllKeys = Enum.GetValues<Key>().ToList();


        public static List<Key> GetAllPressedKeys() => AllKeys.Skip(1).Where(k => Keyboard.IsKeyDown(k)).ToList();
        public static List<Key> PrevPressed = new List<Key>();

        public static void ResolveKeyboardInput(params object[] objects)
        {
            var allCommands = HuiCombinations.AllInputCommands;
            var allPressed = GetAllPressedKeys();
            var allMerged = HuiCombinations.AllMergedKeys;
            var allPressedAsMerged = allPressed.SelectMany(k => allMerged.Where(mk => mk.Keys.Contains(k)))?.ToList();
            
            var command = allCommands.FirstOrDefault(c => c.Match(allPressedAsMerged));
            if(command != null)
            {
                command.Execute(objects);
                return;
            }

            var allModes = HuiCombinations.AllModes;
            var mode = allModes.FirstOrDefault(c => c.Match(allPressedAsMerged));
            if (mode != null)
            {
                return;
            }
        }


        public static void ReportInput() { ResolveKeyboardInput(); }
        public static void ReportInput<T>(T argT) { ResolveKeyboardInput(); }
        public static void ReportInput<T1, T2>(T1 argT1,T2 argT2 ) { ResolveKeyboardInput(); }
    }
}
