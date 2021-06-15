//using CommonBasicLibraries.AdvancedGeneralFunctionsAndProcesses.Misc;
using CommonBasicLibraries.AdvancedGeneralFunctionsAndProcesses.Misc;
using CommonBasicLibraries.BasicDataSettingsAndProcesses;
using CommonBasicLibraries.CollectionClasses;
using System;
using System.Linq;
using System.Threading.Tasks;
namespace CommonBasicLibraries.AdvancedGeneralFunctionsAndProcesses.BasicExtensions
{
    public static class Integers
    {
        public static string Join(this BasicList<int> thisList, string delimiter)
        {
            StrCat cats = new ();
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
            StrCat cats = new ();
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
            return progressSpan.SongProgress(durationSpan);
        }
        public static string MusicProgressStringFromSeconds(this int secondsUpTo, int durationSeconds)
        {
            TimeSpan progressSpan = TimeSpan.FromSeconds(secondsUpTo);
            TimeSpan durationSpan = TimeSpan.FromSeconds(durationSeconds); //meant to use from seconds.
            return progressSpan.SongProgress(durationSpan);
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

            return default!; //i think should be fine (?)
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
                action?.Invoke(i + 1); //we want it one based.
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
                await action?.Invoke(i + 1)!; //we want it one based.
            }
        }
        public static bool IsNumberOdd(this int x)
        {
            int results = x % 2;
            return Math.Abs(results) == 1;
        }
    }
}