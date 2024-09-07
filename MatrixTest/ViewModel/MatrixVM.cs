using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatrixTest.ViewModel
{
    public class MatrixVM
    {
    }

    public class LoLVM
    {

    }

    public class ScopeVM : ViewModelBase
    {
        public ObservableCollection<LoLVM> lolVMs = new ObservableCollection<LoLVM>();
        public ObservableCollection<LoLVM> LoLVMs { get => lolVMs; set {  lolVMs = value; OnPropertyChanged(); } }
    }

}
