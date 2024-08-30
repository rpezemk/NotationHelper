using NotationHelper.DataModel.Elementary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotationHelper.DataModel
{
    public class Note
    {
        public int X {  get; set; }
        public int Y { get; set; }
        public Pitch Pitch { get; set; } = new Pitch();
        public int BarNo { get; set; }
        public int PartNo { get; set; }
    }
}
