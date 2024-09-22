using MusicDataModel.DataModel.Elementary;

namespace MusicDataModel.Helpers
{
    public static class MeterHelper
    {
        public static double DurationToLength(this Duration duration)
        {
            var absLen = duration.GetLen();
            return absLen;
        }
    }
}
