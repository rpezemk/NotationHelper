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

namespace NotationHelper
{
    public static class Program
    {
        public static void FillBasic(MainWindow mainWindow, PieceMatrix matrix, int startBarNo, int barCount) 
        {
            ClearMain(mainWindow);
            var leftList = new List<VerticalBarContainer>();
            var rightList = new List<VerticalBarContainer>();

            var gridHeight = mainWindow.MultiColumnGrid.ActualHeight;
            var hLayoutHeight = new HLayout().Height;

            var nPartsPerSide = (int)Math.Floor(gridHeight / (hLayoutHeight));


            matrix.HBarGroups.DivideSet(nPartsPerSide, out var partGroups, out var nResCount);

            int groupId = 0;
            foreach (var partGroup in partGroups) 
            {
                mainWindow.MultiColumnGrid.ColumnDefinitions.Add(new ColumnDefinition());
                StackPanel stackPanel = new StackPanel();
                Grid.SetColumn(stackPanel, groupId);
                foreach(var part in partGroup)
                {
                    var hLayout = new HLayout();
                    hLayout.ShowNBars(part.Bars.Count());
                    stackPanel.Children.Add(hLayout);
                }
                mainWindow.MultiColumnGrid.Children.Add(stackPanel);

                groupId++;
            }
            

            //foreach (var part in outLeft) 
            //{
            //    mainWindow.NoteLayout.Children.Add(new HLayout());
            //}

            //foreach (var part in nResCount)
            //{
            //    mainWindow.NoteLayoutAux.Children.Add(new HLayout());
            //}


            //foreach (var layout in notePanels)
            //{
            //    var notes = TestHelper.GetRandomNotes(matrix);
            //    layout.ShowNotes(notes);
            //}
        }

        public static void ClearMain(MainWindow mainWindow)
        {
            mainWindow.MultiColumnGrid.ColumnDefinitions.Clear();
            mainWindow.MultiColumnGrid.Children.Clear();
            //mainWindow.NoteLayout.Children.Clear();
            //mainWindow.NoteLayoutAux.Children.Clear();
        }
    }
}
