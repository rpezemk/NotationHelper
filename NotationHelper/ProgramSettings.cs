using MusicDataModel.MusicViews;
using MusicDataModel.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Media3D;
using System.Windows.Media;
using MusicDataModel.DataModel;
using MusicDataModel.DataModel.Elementary;
using MusicDataModel.FlowTypes;
using MusicDataModel.MVVM.MainVM;

namespace MusicDataModel
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

        public static List<ToolbarItem_VM> GetToolbarItems() 
        {
            List<ToolbarItem_VM> toolBarVMS = new List<ToolbarItem_VM>();
            toolBarVMS.Add(new ToolbarItem_VM("||4||",    () => { }));
            toolBarVMS.Add(new ToolbarItem_VM("|2|",    () => { }));
            toolBarVMS.Add(new ToolbarItem_VM("1",    () => { }));
            toolBarVMS.Add(new ToolbarItem_VM("1/2",  () => { }));
            toolBarVMS.Add(new ToolbarItem_VM("1/4",  () => { }));
            toolBarVMS.Add(new ToolbarItem_VM("1/8",  () => { }));
            toolBarVMS.Add(new ToolbarItem_VM("1/16", () => { }));
            toolBarVMS.Add(new ToolbarItem_VM("1/32", () => { }));
            return toolBarVMS;
        }
        private static void TestMethod()
        {
            MessageBox.Show("test");
        }
    }
}
