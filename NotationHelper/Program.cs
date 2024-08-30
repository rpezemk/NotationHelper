using NotationHelper.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace NotationHelper
{
    public static class Program
    {
        public static void FillBasic(MainWindow mainWindow) 
        {
            ClearMain(mainWindow);
            //mainWindow.NoteLayout = new Controls.GeneralLayout();
            var leftList = new List<VerticalBarContainer>();
            var rightList = new List<VerticalBarContainer>();

            Random rand = new Random();
            for(int columnNo =  0; columnNo < 4; columnNo++)
            {
                var w = mainWindow.NoteLayout.ActualWidth;
                var leftContainter = new Controls.VerticalBarContainer() ;
                var rightContainter = new Controls.VerticalBarContainer() ;
               
                for (int rowNo = 0; rowNo < 14; rowNo++)
                {
                    var leftBar = new Controls.BarControl();
                    var rightBar = new Controls.BarControl();
                    //for (int k = 0; k < 25; k++) 
                    //{
                    //    Thickness th = new Thickness(k*10 - 30, rand.Next(10)*5 - 20, 0, 0);
                    //    leftBar.ShowNote(new NoteControl() { Margin = th });
                    //    rightBar.ShowNote(new NoteControl() { Margin = th });
                    //}
                    leftContainter.AddBarControl(leftBar);
                    rightContainter.AddBarControl(rightBar);    
                }
                leftList.Add(leftContainter);
                rightList.Add(rightContainter);

            }
            mainWindow.NoteLayout.AddControls(leftList);
            mainWindow.NoteLayoutAux.AddControls(rightList);
        }

        public static void ClearMain(MainWindow mainWindow)
        {
            mainWindow.NoteLayout.ClearBarControl();
            mainWindow.NoteLayoutAux.ClearBarControl();
        }
    }
}
