namespace CommonBasicLibraries.MediaHelpers.Helpers;
public static class Extensions
{
    internal static string StartProgress()
    {
        ProgressModel progressModel = new();
        return progressModel.GetProgress();
    }
    public static string GetProgress(this ProgressModel progress)
    {
        string upto = progress.UpTo.GetProgress();
        string duration = progress.Duration.GetProgress();
        return $"{upto}/{duration}";
    }
    private static string GetProgress(this int seconds)
    {
        if (seconds < 0)
        {
            seconds = 0;
        }
        int minutes = seconds / 60;
        int hours = minutes / 60;
        int leftovers = seconds - (minutes * 60);
        minutes -= hours * 60;
        string hourString = GetDoubleString(hours);
        string minuteString = GetDoubleString(minutes);
        string secondString = GetDoubleString(leftovers);
        return $"{hourString}:{minuteString}:{secondString}";
    }
    private static string GetDoubleString(this int value)
    {
        if (value < 10)
        {
            return $"0{value}";
        }
        else
        {
            return value.ToString();
        }
    }
}