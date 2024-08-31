using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotationHelper.Helpers
{
    public static class EnumHelper
    {
        public static void DivideSet<T>(this List<T> inputValues, out List<T> outLeft, out List<T> outRight)
        {
            outLeft = new List<T>();
            outRight = new List<T>();

            var cnt = inputValues.Count;
            var half = (int)Math.Ceiling(cnt / 2.0);

            for (var i = 0; i < half; i++)
            {
                outLeft.Add(inputValues[i]);
            }

            for(var i = half; i < cnt; i++)
            {
                outRight.Add(inputValues[i]);
            }
        }
    }
}
