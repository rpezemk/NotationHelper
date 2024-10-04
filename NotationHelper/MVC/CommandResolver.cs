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

        public static List<Key> AllKbdKeys = Enum.GetValues<Key>().ToList();
        public static List<MouseButton> AllMouseKeys = Enum.GetValues<MouseButton>().ToList();


        public static List<AKey> GetAllPressedKeys()
        {
            var aKeys = AllKbdKeys.Skip(1).Where(k => Keyboard.IsKeyDown(k)).ToList().SelectMany(a => HuiCombinations.AllMergedKeys.Where(mk => mk.Keys.Contains(a))).Cast<AKey>().ToList();
            var mouseButtonsPressed = new List<MouseButton>();
            if(Mouse.LeftButton == MouseButtonState.Pressed)
                aKeys.Add(HuiCombinations.LeftButton);
            if (Mouse.RightButton == MouseButtonState.Pressed)
                mouseButtonsPressed.Add(MouseButton.Right);

            return aKeys;
        }
        public static List<Key> PrevPressed = new List<Key>();

        public static void ResolveKeyboardInput(params object[] objects)
        {
            var allCommands = HuiCombinations.AllInputCommands;
            var allPressed = GetAllPressedKeys();

            if (allCommands.FirstOrDefault(c => c.MatchKeys(allPressed) && c.MatchArguments(objects)) is not InputCommand inputCommand)
                return;
            inputCommand.Execute(objects);
        }


        public static void ReportInput() { ResolveKeyboardInput(); }
        public static void ReportInput<T>(T argT) { ResolveKeyboardInput(); }
        public static void ReportInput<T1, T2>(T1 argT1,T2 argT2 ) { ResolveKeyboardInput(); }
    }
}
