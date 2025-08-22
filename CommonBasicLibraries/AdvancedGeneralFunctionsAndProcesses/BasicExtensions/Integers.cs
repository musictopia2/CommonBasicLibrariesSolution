namespace CommonBasicLibraries.AdvancedGeneralFunctionsAndProcesses.BasicExtensions;
public static class Integers
{
    private readonly static BasicList<string> _units =
    [
        "Zero",
        "One",
        "Two",
        "Three",
        "Four",
        "Five",
        "Six",
        "Seven",
        "Eight",
        "Nine",
        "Ten",
        "Eleven",
        "Twelve",
        "Thirteen",
        "Fourteen",
        "Fifteen",
        "Sixteen",
        "Seventeen",
        "Eighteen",
        "Nineteen"
    ];
    private readonly static BasicList<string> _tens =
    [
        "",
        "",
        "Twenty",
        "Thirty",
        "Forty",
        "Fifty",
        "Sixty",
        "Seventy",
        "Eighty",
        "Ninety"
    ];
    public static string ConvertToDecimalWords(this decimal value)
    {
        // Split the decimal into integer and fractional parts
        int integerPart = (int)value;
        int fractionalPart = (int)((value - integerPart) * 100); // Get the cents as an integer
        // Convert the integer part to words
        string integerWords = integerPart.ConvertToIntegerWords();
        if (fractionalPart == 0)
        {
            return integerWords; //that always worked so no problem there.
        }
        string fractionalWords = $"and {fractionalPart}/100";
        return $"{integerWords} {fractionalWords}";
    }
    public static string ConvertToIntegerWords(this int value)
    {
        if (value < 20)
        {
            return _units[value].ToLower();
        }
        if (value < 100)
        {
            return _tens[value / 10].ToLower() + ((value % 10 > 0) ? " " + ConvertToIntegerWords(value % 10) : "");
        }
        if (value < 1000)
        {
            return _units[value / 100].ToLower() + " hundred"
                    + ((value % 100 > 0) ? " " + ConvertToIntegerWords(value % 100) : "");
        }
        if (value < 100000)
        {
            return ConvertToIntegerWords(value / 1000) + " thousand"
            + ((value % 1000 > 0) ? " " + ConvertToIntegerWords(value % 1000) : "");
        }
        throw new CustomBasicException("This only supports up to 100,000.  If you need hundred thousands, then rethink");
    }
    public static string Join(this BasicList<int> thisList, string delimiter)
    {
        StrCat cats = new();
        thisList.ForEach(x => cats.AddToString(x.ToString(), delimiter));
        return cats.GetInfo();
    }
    public static (int Batches, int LeftOver) GetRemainderInfo(this int thisInt, int batchSize)
    {
        int x = 0;
        int b = 0;
        for (int i = 1; i < thisInt; i++)
        {
            x += 1;
            if (x == batchSize)
            {
                x = 0;
                b++;
            }
        }
        return (b, x);
    }
    public static string ConvertToSpecificStrings(this int thisInt, int desiredDigits)
    {
        string temps = thisInt.ToString();
        if (temps.Length > desiredDigits)
        {
            throw new CustomBasicException($"The Integer Of {thisInt} has more digits than the desired digits of {desiredDigits}");
        }
        if (temps.Length == desiredDigits)
        {
            return temps;
        }
        int padding = desiredDigits - temps.Length;
        StrCat cats = new();
        for (int i = 0; i < padding; i++)
        {
            cats.AddToString("0");
        }
        cats.AddToString(temps);
        return cats.GetInfo();
    }
    public static string MusicProgressStringFromMillis(this int milliSecondsUpTo, int durationMilliseconds)
    {
        TimeSpan progressSpan = TimeSpan.FromMilliseconds(milliSecondsUpTo);
        TimeSpan durationSpan = TimeSpan.FromMilliseconds(durationMilliseconds);
        return progressSpan.MediaProgress(durationSpan);
    }
    public static string MusicProgressStringFromSeconds(this int secondsUpTo, int durationSeconds)
    {
        TimeSpan progressSpan = TimeSpan.FromSeconds(secondsUpTo);
        TimeSpan durationSpan = TimeSpan.FromSeconds(durationSeconds); //meant to use from seconds.
        return progressSpan.MediaProgress(durationSpan);
    }
    public static int MultiplyPercentage(this int amount, int percentage) => (int)Math.Ceiling((decimal)percentage / 100 * amount); //decided this needs to be clear it multiplies
    public static T ToEnum<T>(this int param) //i may need to cast other times to enums and even generic enums.
    {
        var info = typeof(T);
        if (info.IsEnum)
        {
            T result = (T)Enum.Parse(typeof(T), param.ToString(), true);
            return result;
        }
        return default!;
    }
    public static int FromEnum<T>(this T param) where T : Enum
    {
        object firsts = Convert.ChangeType(param, param.GetTypeCode());
        return (int)firsts;
    }
    public static void Times(this int @this, Action action)
    {
        for (var i = 0; i < @this; i++)
        {
            action?.Invoke();
        }
    }
    public static void Times(this int @this, Action<int> action)
    {
        for (var i = 0; i < @this; i++)
        {
            action?.Invoke(i + 1);
        }
    }
    public async static Task TimesAsync(this int @this, Func<Task> action)
    {
        for (var i = 0; i < @this; i++)
        {
            await action?.Invoke()!;
        }
    }
    public async static Task TimesAsync(this int @this, Func<int, Task> action)
    {
        for (var i = 0; i < @this; i++)
        {
            await action?.Invoke(i + 1)!;
        }
    }
    public static bool IsNumberOdd(this int x)
    {
        int results = x % 2;
        return Math.Abs(results) == 1;
    }
    public static long Megs(this int megabytes)
    {
        return megabytes * 1024 * 1024;
    }
    public static int MinutesToSeconds(this int minutes) => minutes * 60;
}