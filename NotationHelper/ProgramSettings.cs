using NotationHelper.MusicViews;
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
    public static class ProgramSettings
    {
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
