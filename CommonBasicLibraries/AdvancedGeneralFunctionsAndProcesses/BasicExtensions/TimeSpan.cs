namespace CommonBasicLibraries.AdvancedGeneralFunctionsAndProcesses.BasicExtensions;
public static class TimeSpanExtensions
{
    public static string MediaProgress(this TimeSpan progressSpan, TimeSpan durationSpan) //not necessarily a song.
    {
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