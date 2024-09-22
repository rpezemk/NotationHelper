using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicDataModel.DataModel.Elementary
{
    public enum NoteName
    {
        C = 0, 
        D = 2, 
        E = 4, 
        F = 5, 
        G = 7, 
        A = 9, 
        B = 11
    }
    public class Pitch
    {
        public int ResultPitch => OctaveNo * 12 + (int)BaseNoteName + Alter;
        public int OctaveNo { get; set; } = 5;
        public NoteName BaseNoteName { get; set; } = NoteName.C;
        public int Alter {  get; set; }
    }
}
