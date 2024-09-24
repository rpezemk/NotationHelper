using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AudioTool.CSoundWrapper
{
    public interface ICsEvent
    {
        public double[] GetParams();
    }

    public class SampleEvent : ICsEvent
    {
        public SampleEvent(int instrNo, double start, double len, double startFrom)
        {
            InstrNo = instrNo;
            Start = start;
            Length = len;
            StartSampleFrom = startFrom;
        }
        public int InstrNo { get; set; }
        public double Start { get; set; }
        public double Length { get; set; }
        public double StartSampleFrom { get; set; }

        public double[] GetParams() => [InstrNo, Start, Length, StartSampleFrom];
    }

    public class CsParam : ICsEvent
    {
        public CsParam(int instrNo,  double start, double len, double freq)
        {
            InstrNo = instrNo;
            Start = start;
            Length = len;
            Freq = freq;
        }
        public int InstrNo {  get; set; }
        public double Start { get; set; }
        public double Length { get; set; }
        public double Freq { get; set; }

        public double[] GetParams()
        {
            return [InstrNo, Start, Length, Freq];
        }
    }
}
