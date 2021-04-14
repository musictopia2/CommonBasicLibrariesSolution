using System;
namespace CommonBasicLibraries.AdvancedGeneralFunctionsAndProcesses.BasicExtensions
{
    public static class Dates
    {
        public static string GetLongDate(this DateTime thisDate)
        {
            return thisDate.DayOfWeek.ToString() + " " + thisDate.Month + "/" + thisDate.Day + "/" + thisDate.Year;
        }
        /// <summary>
        /// had to introduce a breaking change.
        /// so when using for testing, will still work.
        /// </summary>
        /// <param name="dateUsed"></param>
        /// <returns></returns>
        public static DateTime WhenIsThanksgivingThisYear(this DateTime dateUsed)
        {
            int x;
            for (x = 22; x <= 30; x++)
            {
                var tempDate = new DateTime(dateUsed.Year, 11, x);
                if (tempDate.DayOfWeek == DayOfWeek.Thursday)
                {
                    return tempDate;
                }
            }
            throw new Exception("Cannot find when thanksgiving is this year");
        }
        public static bool IsBetweenThanksgivingAndChristmas(this DateTime thisDate)
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
                var tempDate = DateTime.Now;
                thisDate = new DateTime(tempDate.Year, 11, thisDate.Day);
                var tDate = DateTime.Now.WhenIsThanksgivingThisYear();
                tDate = tDate.AddDays(1);
                if (thisDate >= tDate)
                {
                    return true;
                }
            }
            return false;
        }
        public static DateTime AddTimeToDate(this DateTime thisDate, string timestring)
        {
            DateTime timed = DateTime.Parse(timestring);
            DateTime output = new (thisDate.Year, thisDate.Month, thisDate.Day, timed.Hour, timed.Minute, timed.Second);
            return output;
        }

    }
}