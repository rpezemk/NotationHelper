using NotationHelper.DataModel.Range;
using NotationHelper.MVVM.Base;
using NotationHelper.MVVM.MusicVM;
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
            MatrixRange matrixRange = new MatrixRange(new DataModel.Elementary.PieceMatrix(15, 30), 0, 4);
            musicContent = new VisualMusicContent_VM();
        }

        private Menu_VM menu_VM = new Menu_VM();
        private ToolBar_VM toolbarVM = new ToolBar_VM();
        private LeftPane_VM leftPane = new LeftPane_VM();
        private Preview_VM perview = new Preview_VM();
        private VisualMusicContent_VM musicContent = new VisualMusicContent_VM();
        public Menu_VM Menu_VM { get { return menu_VM; } set { menu_VM = value; OnPropertyChanged(); } }
        public ToolBar_VM ToolBar_VM { get { return toolbarVM; } set { toolbarVM = value; OnPropertyChanged(); } }
        public LeftPane_VM LeftPane { get { return leftPane; } set { leftPane = value; OnPropertyChanged(); } }
        public Preview_VM Preview { get { return perview; } set { perview = value; OnPropertyChanged(); } }
        public VisualMusicContent_VM MusicContentVM { get { return musicContent; } set { musicContent = value; OnPropertyChanged(); } }

    }
}
