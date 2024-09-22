namespace MusicDataModel.DataModel.Elementary
{
    public class Duration
    {
        public DurationEnum BaseDuration { get; set; } = DurationEnum.Querter;
        public DottingEnum Dotting { get; set; } = DottingEnum.NoDots;
        public float GetLen() => (4 / (float)(int)BaseDuration) * ((int)Dotting)/4;
    }

    public enum DurationEnum
    {
        Longa = 1,
        Breve = 2,
        Whole = 4,
        Half = 8,
        Querter = 16,
        Eight = 32,
        Sixteen = 64,
        ThirtyTwo = 128,
        SixtyFour= 256,
    }

    public enum DottingEnum
    {
        NoDots = 4,
        SingleDot = 6,
        DoubleDot = 7
    }
}
