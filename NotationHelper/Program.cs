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
        public static void FillBasic(MainWindow mainWindow) 
        {
            ClearMain(mainWindow);
            var leftList = new List<VerticalBarContainer>();
            var rightList = new List<VerticalBarContainer>();

            Random rand = new Random();
            for (int columnNo = 0; columnNo < 4; columnNo++)
            {
                var w = mainWindow.NoteLayout.ActualWidth;
                var leftContainter = new Controls.VerticalBarContainer();
                var rightContainter = new Controls.VerticalBarContainer();

                for (int rowNo = 0; rowNo < 14; rowNo++)
                {
                    var leftBar = new Controls.BarControl();
                    var rightBar = new Controls.BarControl();
                    
                    leftContainter.AddBarControl(leftBar);
                    rightContainter.AddBarControl(rightBar);
                }
                leftList.Add(leftContainter);
                rightList.Add(rightContainter);

            }
            mainWindow.NoteLayout.AddControls(leftList);
            mainWindow.NoteLayoutAux.AddControls(rightList);

            var notePanels = new List<GeneralLayout>() { mainWindow.NoteLayout, mainWindow.NoteLayoutAux };

            foreach(var layout in notePanels)
            {
                var notes = TestHelper.GetRandomNotes(new PieceMatrix(30, 4));
                layout.ShowNotes(notes);
            }
        }

        public static void ClearMain(MainWindow mainWindow)
        {
            mainWindow.NoteLayout.MyNoteCanvas.Children.Clear();
            mainWindow.NoteLayoutAux.MyNoteCanvas.Children.Clear();
            mainWindow.NoteLayout.ClearNotes();
            mainWindow.NoteLayout.ClearControls();
            mainWindow.NoteLayoutAux.ClearNotes();
            mainWindow.NoteLayoutAux.ClearControls();
        }
    }
}
