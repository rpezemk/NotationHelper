﻿using MusicDataModel;
using NotationHelper.Commands;
using NotationHelper.Helpers;
using NotationHelper.MVC.Basics;
using System.Reflection;
using System.Windows;
using System.Windows.Input;

namespace NotationHelper.MVC
{
    internal static class HuiCombinations
    {

        public static MergedKey Escape = new MergedKey(Key.Escape);
        public static MergedKey Shift = new MergedKey(Key.LeftShift, Key.RightShift);
        public static MergedKey Ctrl = new MergedKey(Key.LeftCtrl, Key.RightCtrl);
        public static MergedKey Alt = new MergedKey(Key.LeftAlt, Key.RightAlt);
        public static MergedKey AKey = new MergedKey(Key.A);
        public static MergedKey Delete = new MergedKey(Key.Delete);

        #region KEYBOARD COMMANDS
        public static InputCommand EscAction => new InputCommand("ESC", Escape).AppendAction(() => SelectedBarsCollection.UnSelectAll());
        public static InputCommand SelectAllAction => new InputCommand("SELECT_ALL", Ctrl, AKey).AppendAction(() => MessageBox.Show("SELECT_ALL"));
        public static InputCommand DeleteAllAction => new InputCommand("DELETE_SELECTED", Delete)
            .AppendAction(() => 
            {
                var selectedBars = SelectedBarsCollection.BarsWithSelectedNotes;
                var visualTimeHolders = selectedBars.SelectMany(b => b.GetTimeHolders());
                var command = new DeleteManyTimeHoldersCommand(visualTimeHolders.Select(t => t.TimeHolder).Where(th => th.IsSelected).ToList());
                command.Execute();
                MainWindow.Instance.Refresh();
            }
            );
        #endregion

        #region KEYBOARD MODES
        public static InputMode SelectSpecific => new InputMode("INDIVIDUAL_SELECTION", Ctrl);
        public static InputMode SelectMeasures => new InputMode("RANGE_SELECTION", Shift);
        public static InputMode SelectBarColumn => new InputMode("BAR_COLUMN_SELECTION", Ctrl, Shift);
        public static InputMode SelectVisibleLine => new InputMode("VISIBLE_LINE", Alt);
        #endregion

        private static List<InputCommand> allInputCommands;
        public static List<InputCommand> AllInputCommands => TypeHelper.PassOrFill(ref allInputCommands, typeof(HuiCombinations));

        private static List<InputMode> allModes;
        public static List<InputMode> AllModes => TypeHelper.PassOrFill(ref allModes, typeof(HuiCombinations));

        private static List<MergedKey> allMergedKeys;
        public static List<MergedKey> AllMergedKeys => TypeHelper.PassOrFill(ref allMergedKeys, typeof(HuiCombinations));
    }
}