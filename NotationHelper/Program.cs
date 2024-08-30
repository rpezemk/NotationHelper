using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotationHelper
{
    public static class Program
    {
        public static void MainEntry(MainWindow mainWindow) 
        {
            //mainWindow.NoteLayout = new Controls.GeneralLayout();
            for(int j =  0; j < 4; j++)
            {
                var w = mainWindow.NoteLayout.ActualWidth;
                var leftContainter = new Controls.VerticalBarContainer() { Width = w/4 };
                var rightContainter = new Controls.VerticalBarContainer() { Width = w/4 };
               
                for (int i = 0; i < 4; i++)
                {
                    leftContainter.AddBarControl(new Controls.BarControl());
                    rightContainter.AddBarControl(new Controls.BarControl());    
                }

                mainWindow.NoteLayout.AddControl(leftContainter);
                mainWindow.NoteLayoutAux.AddControl(rightContainter);
            }
            
        }
    }
}
