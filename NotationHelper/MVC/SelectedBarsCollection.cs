using MusicDataModel.DataModel.Piece;
using MusicDataModel.Helpers;
using MusicDataModel.MusicViews.MusicControls;

namespace NotationHelper.MVC
{
    public static class SelectedBarsCollection
    {
        public static List<BarWithLine> BarsWithSelectedNotes = new List<BarWithLine>();
        public static void UnSelectAll()
        {
            BarsWithSelectedNotes.ForEach(bc => bc.GetTimeHolderDrawings()
                        .Where(th => th.TimeHolder.IsSelected)
                        .ForEach(b => b.Redraw(false, BarWithLine.Scale)));
            BarsWithSelectedNotes.Clear();
        }

        public static void UnSelectExceptOf(BarWithLine barWithLine)
        {
            BarsWithSelectedNotes.Where(b => b != barWithLine)
                .ForEach(a => a.GetTimeHolderDrawings()
                                .Where(th => th.TimeHolder.IsSelected)
                                .ForEach(th => th.Redraw(false, BarWithLine.Scale)));
            BarsWithSelectedNotes.Clear();
        }



        internal static void Add(BarWithLine barWithLine)
        {
            BarsWithSelectedNotes.Add(barWithLine);
        }

        public static List<TimeHolderDrawing> GetTimeHolderDrawings() => BarsWithSelectedNotes.SelectMany(b => b.GetTimeHolderDrawings()).ToList();
        public static List<TimeHolder> GetTimeHolders() => BarsWithSelectedNotes.SelectMany(b => b.GetTimeHolderDrawings().Select(thd => thd.TimeHolder)).ToList();
        public static List<TimeHolder> GetSelectedTimeHolders() => BarsWithSelectedNotes.SelectMany(b => b.GetTimeHolderDrawings().Select(thd => thd.TimeHolder)).Where(th => th.IsSelected).ToList();
    }


}
