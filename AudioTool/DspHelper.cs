
using MathNet.Numerics;
using MathNet.Numerics.IntegralTransforms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace AudioTool
{
    public static class DspHelper
    {

        public static float[] GetFlatResponse(int len)
        {
            var dirac = Dirac(len);
            var circDirac = new CircularData(dirac);
            var randomPhases = GetRandomPhases(len);
            for(int i = 0; i < len; i++)
            {
                circDirac.Phases[i] = circDirac.Phases[i] + randomPhases[i];
            }
            return circDirac.ToReals();
        }
       
        public static float[] Dirac(int i)
        {
            if (i == 0)
                return Array.Empty<float>();

            float[] res = new float[i];
            res[0] = 1;
            return res;
        }

        public static float[] GetRandomPhases(int len)
        {
            Random random = new Random();
            float[] res = new float[len];
            for(int i = 0; i < len; i++)
            {
                res[i] = (float)(random.NextDouble() * Math.PI * 2);
            }
            return res;
        }
    }


    public static class ArrayHelpers
    {
        public static T[] CloneArr<T>(this T[] inArr) 
        {
            var len = inArr.Length;
            var ret = new T[len];
            for (int i = 0; i < len; i++) 
            {
                ret[i] = inArr[i];
            }

            return ret;
        }

        public static Complex32[] ToComplex32Arr(this float[] reals)
        {
            var len = reals.Length;
            var ret = new Complex32[len];
            for(int i = 0; i < len; i++)
            {
                ret[i] = reals[i];
            }

            return ret;
        }       
    }
    public class CircularData
    {
        public float[] Amplitudes;
        public float[] Phases;
        public CircularData(float[] onlyReals)
        {
            var circular = onlyReals.CloneArr().ToComplex32Arr();
            Fourier.Forward(circular, FourierOptions.Matlab);

            Amplitudes = circular.Select(c => c.Magnitude).ToArray();
            Phases = circular.Select(c => c.Phase).ToArray();
        }

        public Complex32[] ToComplex32Arr() 
        {
            var restored = Amplitudes.Select(
                    (a, i) =>
                        new Complex32(
                            (float)(a * Math.Cos(Phases[i])),
                            (float)(a * Math.Sin(Phases[i]))))
                .ToArray();

            Fourier.Inverse(restored, FourierOptions.Matlab);
            return restored;
        }

        public float[] ToReals()
        {
            var restored = Amplitudes.Select(
                    (a, i) =>
                        new Complex32(
                            (float)(a * Math.Cos(Phases[i])),
                            (float)(a * Math.Sin(Phases[i]))))
                .ToArray();

            Fourier.Inverse(restored, FourierOptions.Matlab);
            var res = restored.Select(r => r.Real).ToArray();
            return res;
        }
    }
}
