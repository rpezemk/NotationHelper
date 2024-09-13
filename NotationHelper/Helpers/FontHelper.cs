using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace NotationHelper.Helpers
{
    public static class FontHelper
    {
        private static FontStyle fontStyle;
        private static FontFamily fontFamily;
        public static FontFamily BravuraFont => fontFamily ?? (fontFamily = new FontFamily(new Uri("pack://application:,,,/"), "./FontResources/#Bravura"));
        public static FontStyle BravuraStyle => new FontStyle() {};
    }
}
