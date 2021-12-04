namespace CommonBasicLibraries.AdvancedGeneralFunctionsAndProcesses.BasicExtensions;
public static class Dates
{
#if NET6_0_OR_GREATER
    public static DateTime ToDateTime(this DateOnly date) => new(date.Year, date.Month, date.Day); //hopefully this simple.
    public static DateOnly ToDateOnly(this DateTime date) => DateOnly.FromDateTime(date);
    public static TimeOnly ToTimeOnly(this DateTime date) => TimeOnly.FromDateTime(date);
    public static string GetLongDate(this DateOnly thisDate) => thisDate.ToString("dddd M/d/yyyy");
#endif
#if NETSTANDARD2_0
    public static string GetLongDate(this DateTime thisDate) => thisDate.ToString("dddd M/d/yyyy");
#endif
#if NET6_0_OR_GREATER
    public static DateOnly WhenIsThanksgivingThisYear(this DateOnly dateUsed) //try this way (since i don't think time matters)
    {
        int x;
        for (x = 22; x <= 30; x++)
        {
            var tempDate = new DateOnly(dateUsed.Year, 11, x);
            if (tempDate.DayOfWeek == DayOfWeek.Thursday)
            {
                return tempDate;
            }
        }
        throw new Exception("Cannot find when thanksgiving is this year");
    }
    public static bool IsBetweenThanksgivingAndChristmas(this DateOnly thisDate) //only date portion is important.
    {
        if (thisDate.Month == 12)
        {
            if (thisDate.Day <= 25)
            {
                return true;
            }
            return false;
        }
        if (thisDate.Month == 11)
        {
            if (thisDate.Day < 22)
            {
                return false;
            }
            var tempDate = DateTime.Now.ToDateOnly();
            thisDate = new DateOnly(tempDate.Year, 11, thisDate.Day);
            var tDate = DateTime.Now.ToDateOnly().WhenIsThanksgivingThisYear();
            tDate = tDate.AddDays(1);
            if (thisDate >= tDate)
            {
                return true;
            }
        }
        return false;
    }
#endif
    public static DateTime AddTimeToDate(this DateTime thisDate, string timestring) //since you are adding time, then means must be date/time this time.
    {
        DateTime timed = DateTime.Parse(timestring);
        DateTime output = new(thisDate.Year, thisDate.Month, thisDate.Day, timed.Hour, timed.Minute, timed.Second);
        return output;
    }
}