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

            Random rand = new Random();

            matrix.HBarGroups.DivideSet(out var outLeft, out var outRight);

            foreach (var part in outLeft) 
            {
                mainWindow.NoteLayout.Children.Add(new HLayout());
            }

            foreach (var part in outRight)
            {
                mainWindow.NoteLayoutAux.Children.Add(new HLayout());
            }


            //foreach (var layout in notePanels)
            //{
            //    var notes = TestHelper.GetRandomNotes(matrix);
            //    layout.ShowNotes(notes);
            //}
        }

        public static void ClearMain(MainWindow mainWindow)
        {
            mainWindow.NoteLayout.Children.Clear();
            mainWindow.NoteLayoutAux.Children.Clear();
        }
    }
}
