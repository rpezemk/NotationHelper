using NotationHelper.Controls;
using NotationHelper.MVVM.Base;
using NotationHelper.Views.MainViews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace NotationHelper.Helpers
{
    public static class VisualHelper
    {
        public static void SubdivideLikeGrid<Tvm, TControl>(this StackPanel panel, double externalWidth, double gridHeight, 
            double subHeight, List<Tvm> values, TControl dummyGeneric) 
            where Tvm : ViewModelBase where TControl : UserControl, new()
        {
            var nPartsPerSide = (int)Math.Floor(gridHeight / (subHeight));
            values.ToList().DivideSet(nPartsPerSide, out var vmGroups, out var nResCount);
            var columnWidth = externalWidth / nResCount;

            foreach (var vmGroup in vmGroups)
            {
                StackPanel subPanel = new StackPanel() { Width = columnWidth };
                foreach (var vm in vmGroup)
                {
                    TControl subControl = new TControl();
                    subControl.DataContext = vm;
                    subPanel.Children.Add(subControl);
                }
                panel.Children.Add(subPanel);
            }
        }

        public static void SubdivideHorizontal<Tvm, TControl>
            (this StackPanel panel, 
            double externalWidth, List<Tvm> values, 
            TControl dummGeneric)
            where Tvm : ViewModelBase 
            where TControl : UserControl, new()
        {
            var columnWidth = externalWidth / values.Count();
            panel.Orientation = Orientation.Horizontal;
            foreach (var vm in values)
            {
                TControl subControl = new TControl() { Width = columnWidth };
                subControl.DataContext = vm;
                panel.Children.Add(subControl);
            }
        }

    }
}
