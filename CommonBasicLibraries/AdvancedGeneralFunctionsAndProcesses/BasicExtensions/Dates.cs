namespace CommonBasicLibraries.AdvancedGeneralFunctionsAndProcesses.BasicExtensions
{
    public static class Dates
    {
        public static DateTime ToDateTime(this DateTime date) => new (date.Year, date.Month, date.Day); //hopefully this simple.
        public static DateOnly ToDateOnly(this DateTime date) => DateOnly.FromDateTime(date);
        public static TimeOnly ToTimeOnly(this DateTime date) => TimeOnly.FromDateTime(date);
        //try to do as dateonly.  if i really need datetime, rethink
        public static string GetLongDate(this DateOnly thisDate) => thisDate.ToString("dddd M/d/yyyy");
        //public static string GetLongDate(this DateTime thisDate)
        //{
        //    return thisDate.ToString("dddd M/d/yyyy");
        //    //return thisDate.DayOfWeek.ToString() + " " + thisDate.Month + "/" + thisDate.Day + "/" + thisDate.Year;
        //}
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
        //may require rethinking (?)
        public static DateTime AddTimeToDate(this DateTime thisDate, string timestring) //since you are adding time, then means must be date/time this time.
        {
            DateTime timed = DateTime.Parse(timestring);
            DateTime output = new(thisDate.Year, thisDate.Month, thisDate.Day, timed.Hour, timed.Minute, timed.Second);
            return output;
        }

    }
}