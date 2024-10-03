using MusicDataModel.Helpers;
using MusicDataModel.MusicViews.MusicControls;

namespace NotationHelper.MVC
{
    public static class SelectedBarsCollection
    {
        public static List<BarWithLine> BarsWithSelectedNotes = new List<BarWithLine>();
        public static void UnSelectAll()
        {
            BarsWithSelectedNotes.ForEach(bc => bc.GetTimeHolders()
                        .Where(th => th.TimeHolder.IsSelected)
                        .ForEach(b => b.Redraw(false, BarWithLine.Scale)));
            BarsWithSelectedNotes.Clear();
        }

        public static void UnSelectExceptOf(BarWithLine barWithLine)
        {
            BarsWithSelectedNotes.Where(b => b != barWithLine)
                .ForEach(a => a.GetTimeHolders()
                                .Where(th => th.TimeHolder.IsSelected)
                                .ForEach(th => th.Redraw(false, BarWithLine.Scale)));
            BarsWithSelectedNotes.Clear();
        }

        internal static void Add(BarWithLine barWithLine)
        {
            BarsWithSelectedNotes.Add(barWithLine);
        }
    }


}
