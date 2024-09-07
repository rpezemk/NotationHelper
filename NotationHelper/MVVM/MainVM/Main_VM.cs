using NotationHelper.MVVM.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotationHelper.MVVM.MainVM
{
    public class Main_VM : ViewModelBase
    {
        public Main_VM() 
        {

        }

        private Menu_VM menu_VM = new Menu_VM();
        private ToolBar_VM toolbarVM = new ToolBar_VM();
        private LeftPane_VM leftPane = new LeftPane_VM();
        private Preview_VM perview = new Preview_VM();
        public Menu_VM Menu_VM { get { return menu_VM; } set { menu_VM = value; OnPropertyChanged(); } }
        public ToolBar_VM ToolBar_VM { get { return toolbarVM; } set { toolbarVM = value; OnPropertyChanged(); } }
        public LeftPane_VM LeftPane { get { return leftPane; } set { leftPane = value; OnPropertyChanged(); } }
        public Preview_VM Preview { get { return perview; } set { perview = value; OnPropertyChanged(); } }
    }
}
