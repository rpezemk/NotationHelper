using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NAudioTest.DataModel
{
    public class StereoWave
    {
        public float[] Left { get; set; }
        public float[] Right { get; set; }
        public StereoWave(float[] left, float[] right)
        {
            this.Left = left;
            this.Right = right;
        }

        public StereoWave(int len) 
        {
            Left = new float[len];
            Right = new float[len];
        }
    }
}
