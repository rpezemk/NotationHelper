using NotationHelper.MVC.Basics;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace NotationHelper.MVC
{
    public static class CommandResolver
    {
        public static ObservableCollection<AKey> PressedKeys = new ObservableCollection<AKey>();

        public static List<Key> AllKbdKeys = Enum.GetValues<Key>().ToList();
        public static List<MouseButton> AllMouseKeys = Enum.GetValues<MouseButton>().ToList();

        public static List<AKey> GetPhysicallyPressedKeys()
        {
            var aKeys = AllKbdKeys.Skip(1).Where(k => Keyboard.IsKeyDown(k)).ToList().SelectMany(a => HuiCombinations.AllMergedKeys.Where(mk => mk.Keys.Contains(a))).Cast<AKey>().ToList();
            var mouseButtonsPressed = new List<MouseButton>();
            if (Mouse.LeftButton == MouseButtonState.Pressed)
                aKeys.Add(HuiCombinations.LeftButton);
            if (Mouse.RightButton == MouseButtonState.Pressed)
                mouseButtonsPressed.Add(MouseButton.Right);

            return aKeys;
        }

        public static void UpdatePressedKeys()
        {
            var actuallyPressed = GetPhysicallyPressedKeys();

            var toRemove = new List<AKey>();
            foreach (var maybeStillPressed in PressedKeys)
            {
                if (!actuallyPressed.Contains(maybeStillPressed))
                    toRemove.Add(maybeStillPressed);
            }

            foreach (var rem in toRemove)
            {
                PressedKeys.Remove(rem);
            }

            foreach (var key in actuallyPressed)
            {
                if (!PressedKeys.Contains(key))
                    PressedKeys.Add(key);
            }
        }


        public static List<Key> PrevPressed = new List<Key>();

        public static void ResolveKeyboardInput(params object[] objects)
        {
            UpdatePressedKeys();
            var allPressed = GetPhysicallyPressedKeys();

            if (HuiCombinations.AllInputCommands
                .FirstOrDefault(c =>
                    c.MatchOrderedKeys(PressedKeys.ToList()) &&
                    c.MatchArguments(objects)) is InputCommand inputCommand)
            {
                inputCommand.Execute(objects);
            }
        }


        public static void ReportInput() { ResolveKeyboardInput(); }
        public static void ReportInput<T>(T argT) { ResolveKeyboardInput(); }
        public static void ReportInput<T1, T2>(T1 argT1, T2 argT2) { ResolveKeyboardInput(); }
    }
}
