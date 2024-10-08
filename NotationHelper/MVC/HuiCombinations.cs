﻿using MusicDataModel;
using MusicDataModel.Helpers;
using MusicDataModel.MusicViews.MusicControls;
using NotationHelper.Commands;
using NotationHelper.Helpers;
using NotationHelper.MVC.Basics;
using System.Windows.Input;

namespace NotationHelper.MVC
{
    internal static class HuiCombinations
    {

        public static MergedKey Escape = new MergedKey(Key.Escape);
        public static MergedKey Shift = new MergedKey(Key.LeftShift, Key.RightShift);
        public static MergedKey Ctrl = new MergedKey(Key.LeftCtrl, Key.RightCtrl);
        public static MergedKey Alt = new MergedKey(Key.LeftAlt, Key.RightAlt);
        public static MergedKey A_Button = new MergedKey(Key.A);
        public static MergedKey Delete = new MergedKey(Key.Delete);

        public static MouseKey LeftButton = new MouseKey(MouseButton.Left);
        public static MouseKey RightButton = new MouseKey(MouseButton.Right);



        #region KEYBOARD COMMANDS

        public static InputCommand SelectSingleNote => new InputCommand("SELECT_SINGLE", LeftButton)
            .AppendAction<List<TimeHolderDrawing>>((timeHolderDrawings) =>
            {
                var c = true;
                var prevSelected = MainWindow.GetTimeHolderDrawings().Where(v => v.TimeHolder.IsSelected).ToList();

                prevSelected.ButNotIn(timeHolderDrawings).ForEach(v => v.Redraw(false, BarWithLine.Scale));

                timeHolderDrawings.ButNotIn(prevSelected).ToList().ForEach(v => v.Redraw(true, BarWithLine.Scale));

                MainWindow.GetTimeHolderDrawings().Where(b => b.BarWithLine != timeHolderDrawings.FirstOrDefault().BarWithLine)
                                .Where(th => th.TimeHolder.IsSelected)
                                .ForEach(th => th.Redraw(false, BarWithLine.Scale));

            });        
        
        public static InputCommand SelectMeasure => new InputCommand("SELECT_MEASURE", LeftButton)
            .AppendAction<BarWithLine>((barWithLine) =>
            {
                barWithLine.GetTimeHolderDrawings().ForEach(th => th.Redraw(true, BarWithLine.Scale));
                var c = true;
                if (c)
                    MainWindow.GetTimeHolderDrawings().Where(b => b.BarWithLine != barWithLine)
                                    .Where(th => th.TimeHolder.IsSelected)
                                    .ForEach(th => th.Redraw(false, BarWithLine.Scale));
            });

        public static InputCommand SelectAll => new InputCommand("SELECT_ALL", Ctrl, A_Button)
            .AppendAction(() => 
            {
                MainWindow.Instance.PieceMatrix.Children.SelectMany(pt => pt.Children).SelectMany(vb => vb.Children).ToList().ForEach(th => th.IsSelected = true);
                MainWindow.GetTimeHolderDrawings().ForEach(th => th.Redraw(th.TimeHolder.IsSelected, BarWithLine.Scale));
            });


        public static InputCommand SelectSpecificRange => new InputCommand("SELECT_SPECIFIC_RANGE", Shift, LeftButton)
            .AppendAction<List<TimeHolderDrawing>>((b) =>
            {
                MainWindow.GetTimeHolderDrawings().ForEach(th => th.Redraw(true, BarWithLine.Scale));
            });





        public static InputCommand EscAction => new InputCommand("ESC", Escape)
            .AppendAction(
            () =>
            {
                MainWindow.Instance.PieceMatrix.Children.SelectMany(pt => pt.Children).SelectMany(vb => vb.Children).ToList().ForEach(th => th.IsSelected = false);
                MainWindow.GetTimeHolderDrawings().ForEach(th => th.Redraw(th.TimeHolder.IsSelected, BarWithLine.Scale));
            });

        public static InputCommand DeleteAllAction => new InputCommand("DELETE_SELECTED", Delete)
            .Append(new DeleteTimeHoldersCommand(),
            () => MainWindow
                  .GetBarWithLines()
                  .SelectMany(b => b.GetTimeHolderDrawings())
                  .Where(th => th.TimeHolder.IsSelected).ToList())
            .AfterAll(() => MainWindow.Instance.Refresh());
        #endregion

        private static List<InputCommand> allInputCommands;
        public static List<InputCommand> AllInputCommands => TypeHelper.PassOrFill(ref allInputCommands, typeof(HuiCombinations));

        private static List<MergedKey> allMergedKeys;
        public static List<MergedKey> AllMergedKeys => TypeHelper.PassOrFill(ref allMergedKeys, typeof(HuiCombinations));
        private static List<MouseKey> allMouseKeys;
        public static List<MouseKey> AllMouseKeys => TypeHelper.PassOrFill(ref allMouseKeys, typeof(HuiCombinations));
    }
}