using System;
namespace CommonBasicLibraries.AdvancedGeneralFunctionsAndProcesses.BasicExtensions
{
    public static class TimeSpanExtensions //decided to go ahead and rename.  because i can forsee a problem with it being ambigious.  since c# are all classes now.
    {
        public static string SongProgress(this TimeSpan progressSpan, TimeSpan durationSpan)
        {
            // videos is over an hour.  if i do the videos in the new format, needs to be redone
            //looks like i have to do it this way in c#.  this means if special extensions are needed for timespans, then this is needed
            if (durationSpan.Hours > 0)
            {
                return string.Format("{0:00}:{1:00}:{2:00}/{3:00}:{4:00}:{5:00}", progressSpan.Hours, progressSpan.Minutes, progressSpan.Seconds, durationSpan.Hours, durationSpan.Minutes, durationSpan.Seconds);
            }
            else
            {
                return string.Format("{0:00}:{1:00}/{2:00}:{3:00}", progressSpan.Minutes, progressSpan.Seconds, durationSpan.Minutes, durationSpan.Seconds);
            }
        }
    }
}