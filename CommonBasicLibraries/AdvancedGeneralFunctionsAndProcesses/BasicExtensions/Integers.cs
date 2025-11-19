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
    extension (int value)
    {
        public string ConvertToIntegerWords
        {
            get
            {
                if (value < 20)
                {
                    return _units[value].ToLower();
                }
                int temp;
                if (value < 100)
                {
                    temp = value % 10;
                    return _tens[value / 10].ToLower() + ((value % 10 > 0) ? " " + temp.ConvertToIntegerWords : "");
                    //return _tens[value / 10].ToLower() + ((value % 10 > 0) ? " " + ConvertToIntegerWords(value % 10) : "");
                }
                if (value < 1000)
                {
                    temp = value % 100;
                    return _units[value / 100].ToLower() + " hundred"
                            + ((value % 100 > 0) ? " " + temp.ConvertToIntegerWords : "");
                }
                if (value < 100000)
                {
                    temp = value % 1000;
                    var fins = value / 1000;
                    return fins.ConvertToIntegerWords + " thousand"
                    + ((value % 1000 > 0) ? " " + temp.ConvertToIntegerWords : "");
                }
                throw new CustomBasicException("This only supports up to 100,000.  If you need hundred thousands, then rethink");
            }
        }
        public (int Batches, int LeftOver) GetRemainderInfo(int batchSize)
        {
            int x = 0;
            int b = 0;
            for (int i = 1; i < value; i++)
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
        public string ConvertToSpecificStrings(int desiredDigits)
        {
            string temps = value.ToString();
            if (temps.Length > desiredDigits)
            {
                throw new CustomBasicException($"The Integer Of {value} has more digits than the desired digits of {desiredDigits}");
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
        public string MusicProgressStringFromMillis(int durationMilliseconds)
        {
            TimeSpan progressSpan = TimeSpan.FromMilliseconds(value);
            TimeSpan durationSpan = TimeSpan.FromMilliseconds(durationMilliseconds);
            return progressSpan.MediaProgress(durationSpan);
        }
        public string MusicProgressStringFromSeconds(int durationSeconds)
        {
            TimeSpan progressSpan = TimeSpan.FromSeconds(value);
            TimeSpan durationSpan = TimeSpan.FromSeconds(durationSeconds); //meant to use from seconds.
            return progressSpan.MediaProgress(durationSpan);
        }
        public int MultiplyPercentage(int percentage) => (int)Math.Ceiling((decimal)percentage / 100 * value);
        public T ToEnum<T>()
        {
            var info = typeof(T);
            if (info.IsEnum)
            {
                T result = (T)Enum.Parse(typeof(T), value.ToString(), true);
                return result;
            }
            return default!;
        }
        public void Times(Action action)
        {
            for (var i = 0; i < value; i++)
            {
                action?.Invoke();
            }
        }
        //may eventually have a way so if you want to quit early, you can.   that may be a new feature.
        public void Times(Action<int> action)
        {
            for (var i = 0; i < value; i++)
            {
                action?.Invoke(i + 1);
            }
        }
        public async Task TimesAsync(Func<Task> action, CancellationToken token = default)
        {
            for (var i = 0; i < value; i++)
            {
                if (token.IsCancellationRequested)
                {
                    return; //don't finish then.
                    //may do something else custom (?)
                }
                await action?.Invoke()!;
            }
        }
        public async Task TimesAsync(Func<int, Task> action, CancellationToken token = default)
        {
            for (var i = 0; i < value; i++)
            {
                if (token.IsCancellationRequested)
                {
                    return; //don't finish then.
                    //may do something else custom (?)
                }
                await action?.Invoke(i + 1)!;
            }
        }
        public bool IsNumberOdd
        {
            get
            {
                int results = value % 2;
                return Math.Abs(results) == 1;
            }
        }
        public int Megs => value * 1024 * 1024;
        public int MinutesToSeconds => value * 60;
    }
    extension<T>(T param)
        where T: Enum
    {
        public int FromEnum => Convert.ToInt32(param);
    }
}