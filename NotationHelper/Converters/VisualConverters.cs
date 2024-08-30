using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace NotationHelper.Converters
{
    public static class VisualConverters
    {
        private static BrushConverter brushConverter = new BrushConverter();
        public static Brush FromHex(string hex)
        {
            var brush = (Brush)brushConverter.ConvertFromString(hex);
            return brush;
        }
    }
}
