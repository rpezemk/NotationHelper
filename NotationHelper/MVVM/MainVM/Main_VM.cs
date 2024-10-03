using MusicDataModel.DataModel.Piece;
using MusicDataModel.DataModel.Range;
using MusicDataModel.MVVM.Base;
using MusicDataModel.MVVM.MusicVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicDataModel.MVVM.MainVM
{
    public class Main_VM : ViewModelBase
    {
        public Main_VM() 
        {
            PieceMatrix matrix = new PieceMatrix(15, 30);
            MatrixRange matrixRange = new MatrixRange(matrix, 0, 6);
            //MatrixRange matrixRange = new MatrixRange(new PieceMatrix(1, 1), 0, 1);
            musicContent = new VisualMusicContent_VM(matrixRange);
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
