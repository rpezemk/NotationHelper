using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotationHelper.DataModel.Elementary
{
    public class Meter
    {
        public int Numerator;
        public DurationEnum Denominator;
        public double GetLen()
        {
            return 4 * Numerator / (int)Denominator;
        }
    }
}
