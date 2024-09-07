using NotationHelper.Controls;
using NotationHelper.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Media3D;
using System.Windows.Media;
using NotationHelper.DataModel;
using NotationHelper.DataModel.Elementary;
using NotationHelper.FlowTypes;

namespace NotationHelper
{
    public static class Program
    {
        public static void FillBasic(MainWindow mainWindow, PieceMatrix matrix, int startBarNo, int barCount) 
        {
            ClearMain(mainWindow);
            var gridHeight = mainWindow.MyMulticolumnView.MultiColumnGrid.ActualHeight;
            var hLayoutHeight = new HLayout().Height;

            var nPartsPerSide = (int)Math.Floor(gridHeight / (hLayoutHeight));

            matrix.Parts.DivideSet(nPartsPerSide, out var partGroups, out var nResCount);

            int groupId = 0;
            foreach (var partGroup in partGroups) 
            {
                mainWindow.MyMulticolumnView.MultiColumnGrid.ColumnDefinitions.Add(new ColumnDefinition());
                StackPanel stackPanel = new StackPanel();
                Grid.SetColumn(stackPanel, groupId);
                foreach(var part in partGroup)
                {
                    var hLayout = new HLayout();
                    hLayout.ShowNBars(part.Bars.Count());
                    stackPanel.Children.Add(hLayout);
                }
                mainWindow.MyMulticolumnView.MultiColumnGrid.Children.Add(stackPanel);

                groupId++;
            }
        }

        public static void ClearMain(MainWindow mainWindow)
        {
            mainWindow.MyMulticolumnView.MultiColumnGrid.ColumnDefinitions.Clear();
            mainWindow.MyMulticolumnView.MultiColumnGrid.Children.Clear();
        }

        public static VirtualMenu GetMainMenu()
        {
            VirtualMenu menu = new VirtualMenu();
            menu.CreateSubMenu(() => TestMethod(), "File", "Open");
            menu.CreateSubMenu(() => TestMethod(), "File", "Save");
            menu.CreateSubMenu(() => TestMethod(), "Edit", "Cut");
            menu.CreateSubMenu(() => TestMethod(), "Edit", "Paste");
            menu.CreateSubMenu(() => TestMethod(), "Edit", "Paste");
            menu.CreateSubMenu(() => TestMethod(), "Options", "InnerSubmenu", "Some option 1");
            menu.CreateSubMenu(() => TestMethod(), "Options", "InnerSubmenu", "Some option 2");
            return menu;
        }

        private static void TestMethod()
        {
            MessageBox.Show("test");
        }
    }
}
