namespace MatrixTest.DataModel.Rhythm
{
    public class RMeasureSetting
    {
        public RegularRatio Ratio { get; set; } = new RegularRatio() { Numerator = 4, Denominator = RRegularDenominator.Four };
        public List<RBar> Bars { get; set; } = new List<RBar>();
    }

    public class RBar
    {
        public List<RVoice> Voices { get; set; } = new List<RVoice>();
    }

    public class RVoice
    {
        public List<RNote> Notes { get; set; } = new List<RNote>();
    }

    public class RDuration
    {

    }

    public class RNote
    {
        public bool LigatureDestiny {  get; set; }
        public bool LigatureSource { get; set; }

    }

    public class RRest
    {

    }

    public class RTupleGroup : RNote
    {
        public int Rank {  get; set; }
    }

    public enum RDurationEnum
    {
        Longa = 0, Breve = 2, Whole = 3,
        Semi = 4, Quarter = 5, Eight = 6,
        Sixteen = 7, ThirtyTwo = 8, SixtyFour = 9
    }

    public class RegularRatio
    {
        public int Numerator { get; set; } = 4;
        public RRegularDenominator Denominator { get; set; } = RRegularDenominator.Four;
    }


    public class RRatio
    {
        public int Numerator { get; set; }
        public int Denominator { get; set; }
        public RRatio(int numerator, int denominator)
        {
            Numerator = numerator;
            Denominator = denominator;
        }

        public static RRatio operator +(RRatio r1, RRatio r2)
        {
            // Calculate the numerator and denominator of the result
            int commonDenominator = r1.Denominator * r2.Denominator;
            int numeratorSum = r1.Numerator * r2.Denominator + r2.Numerator * r1.Denominator;

            return new RRatio(numeratorSum, commonDenominator).Simplify();
        }

        public static RRatio operator -(RRatio r1, RRatio r2)
        {
            // Calculate the numerator and denominator of the result
            int commonDenominator = r1.Denominator * r2.Denominator;
            int numeratorSum = r1.Numerator * r2.Denominator + r2.Numerator * r1.Denominator;

            return new RRatio(numeratorSum, commonDenominator).Simplify();
        }


        public RRatio Simplify()
        {
            int gcd = GCD(Numerator, Denominator);
            return new RRatio(Numerator / gcd, Denominator / gcd);
        }

        private static int GCD(int a, int b)
        {
            while (b != 0)
            {
                int temp = b;
                b = a % b;
                a = temp;
            }
            return Math.Abs(a);
        }

        public override string ToString()
        {
            return $"{Numerator}/{Denominator}";
        }

    }

    public enum RRegularDenominator
    {
        One = 1, Two = 2, Four = 4, Eight = 8, Sixteen = 16, ThirtyTwo = 32, SixtyFour = 64,
    }

}
