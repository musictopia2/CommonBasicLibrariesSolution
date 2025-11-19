namespace CommonBasicLibraries.MediaHelpers.Helpers;
public static class Extensions
{
    //has to remain for this one
    extension(string)
    {
        internal static string StartProgress()
        {
            ProgressModel progressModel = new();
            return progressModel.GetProgress();
        }
    }
    extension(ProgressModel progress)
    {
        public string GetProgress()
        {
            string upto = progress.UpTo.GetProgress();
            string duration = progress.Duration.GetProgress();
            return $"{upto}/{duration}";
        }
    }
    extension(int payLoad)
    {
        internal string GetProgress()
        {
            if (payLoad < 0)
            {
                payLoad = 0;
            }
            int minutes = payLoad / 60;
            int hours = minutes / 60;
            int leftovers = payLoad - (minutes * 60);
            minutes -= hours * 60;
            string hourString = GetDoubleString(hours);
            string minuteString = GetDoubleString(minutes);
            string secondString = GetDoubleString(leftovers);
            return $"{hourString}:{minuteString}:{secondString}";
        }
        internal string GetDoubleString()
        {
            if (payLoad < 10)
            {
                return $"0{payLoad}";
            }
            else
            {
                return payLoad.ToString();
            }
        }
    }
}