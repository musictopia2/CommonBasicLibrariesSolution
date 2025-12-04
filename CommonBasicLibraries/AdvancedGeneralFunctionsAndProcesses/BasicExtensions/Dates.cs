namespace CommonBasicLibraries.AdvancedGeneralFunctionsAndProcesses.BasicExtensions;
public static class Dates
{
    extension(DateOnly date)
    {
        public DateTime ToDateTime() => new(date.Year, date.Month, date.Day); //must do as property or does not work since there was a method that took parameters.
        public string GetLongDate => date.ToString("dddd M/d/yyyy");
        public DateOnly WhenIsThanksgivingThisYear
        {
            get
            {
                int x;
                for (x = 22; x <= 30; x++)
                {
                    var tempDate = new DateOnly(date.Year, 11, x);
                    if (tempDate.DayOfWeek == DayOfWeek.Thursday)
                    {
                        return tempDate;
                    }
                }
                throw new CustomBasicException("Cannot find when thanksgiving is this year");
            }
        }
        public bool IsBetweenThanksgivingAndChristmas
        {
            get
            {
                // Day after Thanksgiving
                DateOnly start = date.WhenIsThanksgivingThisYear.AddDays(1);

                // Christmas Day
                DateOnly end = new (date.Year, 12, 25);

                // Return true if date is in the range
                return date >= start && date <= end;
            }
        }
    }
    extension(DateTime date)
    {
        public DateOnly ToDateOnly => DateOnly.FromDateTime(date);
        public TimeOnly ToTimeOnly => TimeOnly.FromDateTime(date);
        public DateTime AddTimeToDate(string timeString)
        {
            DateTime timed = DateTime.Parse(timeString);
            DateTime output = new(date.Year, date.Month, date.Day, timed.Hour, timed.Minute, timed.Second);
            return output;
        }
    }
}