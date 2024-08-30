﻿using System.Windows;
using System.Windows.Media;

namespace NotationHelper.Controls
{
    public class DrawingVisualHost : FrameworkElement
    {
        private VisualCollection _visuals;

        public DrawingVisualHost()
        {
            _visuals = new VisualCollection(this);
        }

        public void AddVisual(DrawingVisual visual)
        {
            _visuals.Add(visual);
        }

        public void RemoveVisual(DrawingVisual visual)
        {
            _visuals.Remove(visual);
        }

        protected override int VisualChildrenCount => _visuals.Count;

        protected override Visual GetVisualChild(int index)
        {
            if (index < 0 || index >= _visuals.Count)
            {
                throw new ArgumentOutOfRangeException(nameof(index));
            }

            return _visuals[index];
        }
    }
}
