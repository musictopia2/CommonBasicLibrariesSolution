using System.Threading;
using aa3 = CommonBasicLibraries.AdvancedGeneralFunctionsAndProcesses.RandomGenerator.Advanced;
namespace CommonBasicLibraries.AdvancedGeneralFunctionsAndProcesses.RandomGenerator;
public partial class RandomGenerator : IRandomGenerator
{
    public static EnumFormula Formula => EnumFormula.TwisterStandard;
    private readonly IRandomData _data;
    public RandomGenerator()
    {
        _data = RandomHelpers.GetRandomDataClass();
    }
    /// <summary>
    /// Casing rules.
    /// </summary>
    internal enum EnumCasingRules
    {
        /// <summary>
        /// Lower case.
        /// </summary>
        LowerCase,

        /// <summary>
        /// Upper case.
        /// </summary>
        UpperCase,

        /// <summary>
        /// Mixed case.
        /// </summary>
        MixedCase
    }
    private readonly Lock _thisObj = new(); //so it has to lock when initializing.
    private bool _dids = false;
    private int _privateID;
    private Func<double>? _r;
    private void DoRandomize() //by this moment, you have to already have your interface that you need for random numbers.
    {
        lock (_thisObj)
        {
            if (_dids == true)
            {
                return;
            }
            _privateID = Guid.NewGuid().GetHashCode(); //this may not be too bad for the new behavior for the random
            SetUpRandom();
            _dids = true;
        }
    }
    private void SetUpRandom()
    {
        if (Formula == EnumFormula.Original)
        {
            Random Temps = new(_privateID);
            _r = Temps.NextDouble;
            return;
        }
        MersenneTwister ts = new((uint)_privateID);
        switch (Formula)
        {
            case EnumFormula.Twister2:

            case EnumFormula.TwisterStandard:
                _r = ts.GenRandReal2;
                break;
            case EnumFormula.TwisterHigh:
                _r = ts.GenRandRes53;
                break;
            case EnumFormula.Twister1:
                _r = ts.GenRandReal1;
                break;
            case EnumFormula.Twister3:
                _r = ts.GenRandReal3;
                break;
            default:
                break;
        }
    }
    internal int Next(int max)
    {
        DoRandomize();
        return (int)Math.Floor(_r!() * (max));
    }
    internal int Next(int min, int max)
    {
        DoRandomize();
        return (int)Math.Floor(_r!() * (max - min) + min);
    }
    public BasicList<int> GenerateRandomList(int maxNumber, int howMany = -1, int startingNumber = 1, BasicList<int>? previousList = null, BasicList<int>? setToContinue = null, bool putBefore = false)
    {
        DoRandomize();
        return aa3.RandomExtensions.GenerateRandomList(this, maxNumber, howMany, startingNumber, previousList, setToContinue, putBefore);
    }
    public BasicList<int> GenerateRandomNumberList(int maximumNumber, int howMany, int startingPoint = 0, int increments = 1)
    {
        DoRandomize();
        return aa3.RandomExtensions.GenerateRandomNumberList(this, maximumNumber, howMany, startingPoint, increments);
    }
    public string GenerateRandomPassword()
    {
        RandomPasswordParameterClass thisPassword = new();
        return GenerateRandomPassword(thisPassword);
    }
    public string GenerateRandomPassword(RandomPasswordParameterClass thisPassword)
    {
        DoRandomize();
        return aa3.RandomExtensions.Password(this, thisPassword);
    }
    public int GetRandomNumber(int maxNumber, int startingPoint = 1, BasicList<int>? previousList = null)
    {
        DoRandomize();
        return aa3.RandomExtensions.GetRandomNumber(this, maxNumber, startingPoint, previousList);
    }
    
    public int GetSeed()
    {
        return _privateID;
    }
    public BasicList<string> GetSeveralUniquePeople(int howMany)
    {
        HashSet<string> firstList = [];
        do
        {
            firstList.Add(NextAnyName());
            if (howMany == firstList.Count)
            {
                return firstList.ToBasicList();
            }
        } while (true);
    }
    public string NextAddress(int syllables = 2, bool shortSuffix = true) => aa3.RandomExtensions.NextAddress(this, syllables, shortSuffix);
    public int NextAge(EnumAgeRanges types = EnumAgeRanges.Adult) //this means if somebody wants to create a new version and have different rules it can.
    {
        return aa3.RandomExtensions.NextAge(this, types);
    }
    public string NextAnyName()
    {
        BasicList<string> allList = _data.FirstNamesFemale;
        allList.AddRange(_data.FirstNamesMale);
        allList.SendRandoms(this);
        return allList.GetRandomItem();
    }
    public bool NextBool(int likelihood = 50)
    {
        if (likelihood < 0 || likelihood > 100)
        {
            throw new ArgumentOutOfRangeException(nameof(likelihood), "Likelihood accepts values from 0 to 100.");
        }
        DoRandomize();
        return _r!() * 100 < likelihood;
    }
    public string NextCity()
    {
        BasicList<CityStateClass> cities = _data.Cities;
        cities.SendRandoms(this);
        return cities.GetRandomItem().City;
    }
    public string NextColor()
    {
        var list = _data.ColorNames;
        list.SendRandoms(this);
        return list.GetRandomItem();
    }
    internal long NextLong(long min = long.MinValue + 1, long max = long.MaxValue - 1)
    {
        if (min > max)
        {
            throw new ArgumentException("Min cannot be greater than Max.", nameof(min));
        }
        DoRandomize(); //i think i need this in order to make it work for randomizing longs which could help with dates (?)
        return (long)Math.Floor(_r!() * (max - min + 1) + min);
    }
    internal double NextDouble(double min = double.MinValue + 1.0, double max = double.MaxValue - 1.0, uint decimals = 4)
    {
        var _fixed = Math.Pow(10, decimals);
        var num = NextLong((int)(min * _fixed), (int)(max * _fixed));
        var numFixed = (num / _fixed).ToString("N" + decimals);

        return double.Parse(numFixed);
    }
    public DateOnly NextDateOnly(DateOnly? min = null, DateOnly? max = null)
    {
        //can't be simple anymore because its dateonly now.
        if (min.HasValue && max.HasValue)
        {
            DateTime tempmin = new(min.Value.Year, min.Value.Month, min.Value.Day);
            DateTime tempmax = new(max.Value.Year, max.Value.Month, max.Value.Day);
            //chose to do this way so i don't have to rethink the function that i found that don't know how i can modify without problems.
            DateTime tempDate = DateUtils.UnixTimestampToDateTime(NextLong((long)DateUtils.DateTimeToUnixTimestamp(tempmin),
                (long)DateUtils.DateTimeToUnixTimestamp(tempmax)));
            return DateOnly.FromDateTime(tempDate);
        }
        var m = GetRandomNumber(12, 1);
        var d = GetRandomNumber(Months[m - 1].Day, 1);
        var y = NextYear();
        return new DateOnly(y, m, d);
    }
    public DateTime NextDateTime(DateTime? min = null, DateTime? max = null)
    {
        if (min.HasValue && max.HasValue)
        {
            DateTime tempDate = DateUtils.UnixTimestampToDateTime(NextLong((long)DateUtils.DateTimeToUnixTimestamp(min.Value),
                (long)DateUtils.DateTimeToUnixTimestamp(max.Value)));
            return tempDate;
        }
        var m = GetRandomNumber(12, 1);
        var d = GetRandomNumber(Months[m - 1].Day, 1);
        var y = NextYear();
        return new DateTime(y, m, d, NextHour(), NextMinute(),
            NextSecond(), NextMillisecond());
    }
    public string NextDomainName(string? tld = null) => aa3.RandomExtensions.NextDomainName(this, tld);
    public string NextTopLevelDomain()
    {
        return aa3.RandomExtensions.NextTopLevelDomain(this);
    }
    public string NextEmail(string? domain = null, int length = 7) => aa3.RandomExtensions.NextEmail(this, domain, length);
    public string NextFirstName(bool isFemale = false)
    {
        BasicList<string> listToUse;
        if (isFemale == false)
        {
            listToUse = _data.FirstNamesMale;
        }
        else
        {
            listToUse = _data.FirstNamesFemale;
        }

        listToUse.SendRandoms(this);
        return listToUse.GetRandomItem();
    }
    public FullNameClass NextFullName()
    {
        BasicList<FullNameClass> list = _data.FullNames;
        list.SendRandoms(this);
        return list.GetRandomItem();
    }
    public string NextGender()
    {
        return aa3.RandomExtensions.NextGender(this);
    }
    public string NextGeohash(int length = 7) => aa3.RandomExtensions.NextGeohash(this, length);
    public string NextGUID(int version = 5)
    {
        return aa3.RandomExtensions.NextGUID(this, version);
    }
    public string NextHashtag() => aa3.RandomExtensions.NextHashtag(this);
    /// <summary>
    /// Generates a random hour.
    /// </summary>
    /// <param name="twentyfourHours">True to use 24-hours format.</param>
    /// <param name="min">Min value.</param>
    /// <param name="max">Max value.</param>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when <c>min</c> is less than 0.</exception>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when <c>max</c> is greater than 23.</exception>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when <c>max</c> is greater than 12 in 12-hours format.</exception>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when <c>min</c> is greater than <c>max</c>.</exception>
    /// <returns>Returns random generated hour.</returns>
    public int NextHour(bool twentyfourHours = true, int? min = null, int? max = null)
    {
        return aa3.RandomExtensions.NextHour(this, twentyfourHours, min, max);
    }
    /// <summary>
    /// Returns a random IP Address.
    /// </summary>
    /// <returns>Returns a random IP Address.</returns>
    public string NextIP() => aa3.RandomExtensions.NextIP(this);
    /// <summary>
    /// Generates a random last name.
    /// </summary>
    /// <returns>Returns random generated last name.</returns>
    public string NextLastName()
    {
        var thisList = _data.LastNames;
        thisList.SendRandoms(this);
        return thisList.GetRandomItem();
    }
    /// <summary>
    /// Generates a random latitude.
    /// </summary>
    /// <param name="min">Min value.</param>
    /// <param name="max">Max value.</param>
    /// <param name="decimals">Number of decimals.</param>
    /// <returns>Returns random generated latitude.</returns>
    public double NextLatitude(double min = -90, double max = 90, uint decimals = 5) => NextDouble(min, max, decimals);

    /// <summary>
    /// Generates a random longitude.
    /// </summary>
    /// <param name="min">Min value.</param>
    /// <param name="max">Max value.</param>
    /// <param name="decimals">Number of decimals.</param>
    /// <returns>Returns random generated longitude.</returns>
    public double NextLongitude(double min = -180, double max = 180, uint decimals = 5) => NextDouble(min, max, decimals);

    /// <summary>
    /// Generates a random millisecond.
    /// </summary>
    /// <returns>Returns random generated millisecond.</returns>
    public int NextMillisecond() => aa3.RandomExtensions.NextMillisecond(this);

    /// <summary>
    /// Generates a random minute.
    /// </summary>
    /// <param name="min">Min value.</param>
    /// <param name="max">Max value.</param>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when <c>min</c> is less than 0.</exception>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when <c>max</c> is greater than 59.</exception>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when <c>min</c> is greater than <c>max</c>.</exception>
    /// <returns>Returns random generated minute.</returns>
    public int NextMinute(int min = 0, int max = 59)
    {
        return aa3.RandomExtensions.NextMinute(this, min, max);
    }
    /// <summary>
    /// Generates a random month.
    /// </summary>
    /// <param name="min">Min value.</param>
    /// <param name="max">Max value.</param>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when <c>min</c> is less than 1.</exception>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when <c>max</c> is greater than 12.</exception>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when <c>min</c> is greater than <c>max</c>.</exception>
    /// <returns>Returns random generated month.</returns>
    public string NextMonth(int min = 1, int max = 12)
    {
        return aa3.RandomExtensions.NextMonth(this, min, max);
    }
    /// <summary>
    /// Generates a random second.
    /// </summary>
    /// <returns>Returns random generated second.</returns>
    public int NextSecond() => aa3.RandomExtensions.NextSecond(this);
    /// <summary>
    /// Generates a random social security number.
    /// </summary>
    /// <param name="ssnFour">True to generate last 4 digits.</param>
    /// <param name="dashes">False to remove dashes.</param>
    /// <returns>Returns random generated social security number.</returns>
    public string NextSSN(bool ssnFour = false, bool dashes = true)
    {
        return aa3.RandomExtensions.NextSSN(this, ssnFour, dashes);
    }


    /// <summary>
    /// Generates a random street.
    /// </summary>
    /// <param name="syllables">Number of syllables.</param>
    /// <param name="shortSuffix">True to use short suffix.</param>
    /// <returns>Returns random generated street name.</returns>
    public string NextStreet(int syllables = 2, bool shortSuffix = true) => aa3.RandomExtensions.NextStreet(this, syllables, shortSuffix);
    public string NextString(int howMany, string stringsToPick)
    {
        return aa3.RandomExtensions.NextString(this, howMany, stringsToPick);
    }
    /// <summary>
    /// Returns a random twitter handle.
    /// </summary>
    /// <returns>Returns a random twitter handle.</returns>
    public string NextTwitterName() => aa3.RandomExtensions.NextTwitterName(this);

    /// <summary>
    /// Returns a random url.
    /// </summary>
    /// <param name="protocol">Custom protocol.</param>
    /// <param name="domain">Custom domain.</param>
    /// <param name="domainPrefix">Custom domain prefix.</param>
    /// <param name="path">Url path.</param>
    /// <param name="extensions">A list of Url extensions to pick one from.</param>
    /// <returns>Returns a random url.</returns>
    public string NextUrl(string protocol = "http", string? domain = null, string? domainPrefix = null, string? path = null, BasicList<string>? extensions = null)
    {
       return aa3.RandomExtensions.NextUrl(this, protocol, domain, domainPrefix, path, extensions);
    }
    /// <summary>
    /// Generates a random year.
    /// </summary>
    /// <param name="min">Min value.</param>
    /// <param name="max">Max value.</param>
    /// <returns>Returns random generated year.</returns>
    public int NextYear(int min = 2000, int max = -1)
    {
        return aa3.RandomExtensions.NextYear(this, min, max);
    }
    /// <summary>
    /// Generates a random (U.S.) zip code.
    /// </summary>
    /// <param name="plusfour">True to return a Zip+4.</param>
    /// <returns>Returns random generated U.S. zip code.</returns>
    public string NextZipCode(bool plusfour = true)
    {
        return aa3.RandomExtensions.NextZipCode(this, plusfour);
    }
    public void SetRandomSeed(int value) //this can come from anywhere.  saved data, etc.
    {
        _privateID = value; //so it can be saved and used for testing (to more easily replay the game).
        SetUpRandom();
        _dids = true; //this means that this will use the same value every time.  useful for debugging.
    }
    public long NextCreditCardNumber(string? cardType = null)
    {
        return aa3.RandomExtensions.NextCreditCardNumber(this, cardType);
    }
    int IRandomNumberList.Next(int max)
    {
        return Next(max);
    }
    int IRandomNumberList.Next(int min, int max)
    {
        return Next(min, max);
    }
}