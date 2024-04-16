using aa3 = CommonBasicLibraries.AdvancedGeneralFunctionsAndProcesses.RandomGenerator.Advanced;
namespace CommonBasicLibraries.AdvancedGeneralFunctionsAndProcesses.RandomGenerator;
public partial class RandomGenerator : IRandomGenerator
{
    public static EnumFormula Formula => EnumFormula.TwisterStandard;
    private readonly IRandomData _data;
    public RandomGenerator()
    {
        _data = RandomHelpers.GetRandomDataClass;
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
    private readonly object _thisObj = new(); //so it has to lock when initializing.
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
    private static int PrivateHowManyPossible(int maxNumber, int startingNumber, int previousCount, int setCount)
    {
        int count = maxNumber - (startingNumber - 1);
        count -= previousCount;
        count -= setCount;
        return count;
    }
    internal int Next(int max)
    {
        return (int)Math.Floor(_r!() * (max));
    }
    internal int Next(int min, int max)
    {
        return (int)Math.Floor(_r!() * (max - min) + min);
    }
    public BasicList<int> GenerateRandomList(int maxNumber, int howMany = -1, int startingNumber = 1, BasicList<int>? previousList = null, BasicList<int>? setToContinue = null, bool putBefore = false)
    {
        DoRandomize();
        ArgumentOutOfRangeException.ThrowIfGreaterThan(howMany, maxNumber);
        if (maxNumber == 0)
        {
            throw new ArgumentNullException(nameof(maxNumber));
        }
        ArgumentOutOfRangeException.ThrowIfGreaterThan(startingNumber, maxNumber);
        bool isMax = false;
        if (howMany == -1)
        {
            howMany = maxNumber;
            isMax = true;
        }
        int adjustedMany = howMany;
        adjustedMany += startingNumber - 1;
        if (previousList != null && previousList.Exists(x => x > maxNumber || x <= startingNumber - 1))
        {
            throw new ArgumentOutOfRangeException(nameof(previousList));
        }
        if (setToContinue != null && setToContinue.Exists(x => x > maxNumber || x <= startingNumber - 1))
        {
            throw new ArgumentException(nameof(setToContinue));
        }
        int oldC;
        oldC = startingNumber - 1;
        int counts;
        BasicList<int> oldList = [];
        int preC = 0;
        int setC = 0;
        if (previousList != null)
        {
            if (previousList.Count == 0)
            {
                throw new ArgumentOutOfRangeException(nameof(previousList), "If you sent previouslist, must contain at least one item");
            }
            counts = previousList.Distinct().Count();
            if (counts != previousList.Count)
            {
                throw new ArgumentException("Previous List Had Duplicate Numbers", nameof(previousList));
            }
            oldC += previousList.Count;
            adjustedMany += previousList.Count;
            oldList.AddRange(previousList);
            preC = previousList.Count;
        }
        if (setToContinue != null)
        {
            if (setToContinue.Count == 0)
            {
                throw new ArgumentOutOfRangeException(nameof(setToContinue), "If you sent settocontinue, must contain at least one item");
            }
            counts = setToContinue.Distinct().Count();
            if (counts != setToContinue.Count)
            {
                throw new ArgumentException("Set List Had Duplicate Numbers", nameof(setToContinue));
            }
            adjustedMany += setToContinue.Count;
            oldList.AddRange(setToContinue);
            oldC += setToContinue.Count;
            setC = setToContinue.Count;
        }
        if (setToContinue != null && previousList != null)
        {
            counts = oldList.Distinct().Count();
            if (counts != previousList.Count + setToContinue.Count)
            {
                throw new CustomBasicException("When combining the set list and previous list, there was duplicates.  This means can't do another list of non duplicate numbers");
            }
        }
        bool isSingle = false;
        int total;
        total = PrivateHowManyPossible(maxNumber, startingNumber, preC, setC);
        if (isMax == false && total < howMany)
        {
            throw new Exception("Since you are not choosing match, not reconciling.  Will cause a never ending loop or you get less than expected");
        }
        if (total == 1)
        {
            isSingle = true;
        }
        if (total < 1)
        {
            throw new Exception("Does not reconcile to randomize.   Will result in a never ending loop");
        }
        if (isSingle == true)
        {
            BasicList<int> tempList = Enumerable.Range(startingNumber, maxNumber - startingNumber + 1).ToBasicList();
            if (oldList.Count > tempList.Count)
            {
                throw new Exception("Unable to get the one number remaining.  Something is corrupted.  Rethink");
            }
            if (oldList.Count == 0)
            {
                return [startingNumber];
            }
            int possibleItem = 0;
            foreach (int index in tempList)
            {
                if (oldList.Contains(index) == false)
                {
                    if (possibleItem > 0)
                    {
                        throw new CustomBasicException("Getting single item failed.  Rethink");
                    }
                    possibleItem = index;
                }
            }
            if (possibleItem == 0)
            {
                throw new CustomBasicException("The single item not found");
            }
            if (setToContinue == null)
            {
                return [possibleItem]; //should not bother doing the random items because there is only one.
            }
            BasicList<int> finalList = [];
            if (putBefore == true)
            {
                finalList.AddRange(setToContinue);
                finalList.Add(possibleItem);
            }
            else
            {
                finalList.Add(possibleItem);
                finalList.AddRange(setToContinue);
            }
            return finalList;
        }
        HashSet<int> rndIndexes = [];
        for (int i = 1; i <= startingNumber - 1; i++)
        {
            rndIndexes.Add(i);
        }
        bool rets;
        if (previousList != null)
        {
            foreach (int index in previousList)
            {
                rets = rndIndexes.Add(index);
                if (rets == false)
                {
                    throw new Exception("Previous List Failed.  Rethink");
                }
            }
        }
        if (setToContinue != null)
        {
            foreach (int index in setToContinue)
            {
                rets = rndIndexes.Add(index);
                if (rets == false)
                {
                    throw new Exception("Set To Continue Failed.  Rethink");
                }
            }
        }
        while (rndIndexes.Count != adjustedMany)
        {
            int index = Next(maxNumber);
            rndIndexes.Add(index + 1);
        }
        for (int i = 1; i <= startingNumber - 1; i++)
        {
            rndIndexes.Remove(i);
        }
        if (previousList != null)
        {
            foreach (int index in previousList)
            {
                rndIndexes.Remove(index);
            }
        }
        BasicList<int> thisList = rndIndexes.ToBasicList();
        if (setToContinue != null && putBefore == false)
        {
            foreach (int index in setToContinue)
            {
                thisList.RemoveSpecificItem(index);
                thisList.Add(index);
            }
        }
        return thisList;
    }
    public BasicList<int> GenerateRandomNumberList(int maximumNumber, int howMany, int startingPoint = 0, int increments = 1)
    {
        BasicList<int> firstList;
        if (increments <= 1)
        {
            increments = 1;
        }
        ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(startingPoint, maximumNumber);
        firstList = GetPossibleIntegerList(startingPoint, maximumNumber, increments);
        if (firstList.Count < 2)
        {
            throw new ArgumentOutOfRangeException("MaximumNumber");
        }
        DoRandomize();
        BasicList<int> finalList = [];
        int x;
        var loopTo = howMany;
        for (x = 1; x <= loopTo; x++)
        {
            // can have repeating numbers
            var ask1 = Next(firstList.Count);
            finalList.Add(firstList[ask1]);
        }
        return finalList;
    }
    private static BasicList<int> GetPossibleIntegerList(int minValue, int maximumValue, int increments)
    {
        BasicList<int> newList =
        [
            minValue
        ];
        int upTo;
        upTo = minValue;
        do
        {
            upTo += increments;
            if (upTo >= maximumValue)
            {
                newList.Add(maximumValue);
                return newList;
            }
            else
            {
                newList.Add(upTo);
            }
        }
        while (true);
    }
    public string GenerateRandomPassword()
    {
        RandomPasswordParameterClass thisPassword = new();
        return GenerateRandomPassword(thisPassword);
    }
    public string GenerateRandomPassword(RandomPasswordParameterClass thisPassword)
    {
        DoRandomize();
        BasicList<int> tempResults = [];
        int x;
        int picked;
        if (thisPassword.HowManyNumbers > 0)
        {
            var numberList = Enumerable.Range(48, 10).ToList();
            if (thisPassword.EliminateSimiliarCharacters == true)
            {
                numberList.Remove(48); // because that is a 0
                numberList.Remove(49); // because 1 is close to l or I
            }

            var loopTo = thisPassword.HowManyNumbers;
            for (x = 1; x <= loopTo; x++)
            {
                picked = Next(numberList.Count);
                tempResults.Add(numberList[picked]); // number picked
            }
        }
        if (thisPassword.UpperCases > 0)
        {
            var upperList = Enumerable.Range(65, 26).ToList();
            if (thisPassword.EliminateSimiliarCharacters == true)
            {
                upperList.Remove(79); // O
                upperList.Remove(73); // I
            }
            var loopTo1 = thisPassword.UpperCases;
            for (x = 1; x <= loopTo1; x++)
            {
                picked = Next(upperList.Count);
                tempResults.Add(upperList[picked]);
            }
        }
        if (thisPassword.LowerCases > 0)
        {
            var lowerList = Enumerable.Range(97, 26).ToList();
            if (thisPassword.EliminateSimiliarCharacters == true)
            {
                lowerList.Remove(111);
                lowerList.Remove(108); // because l is too close to 1
                lowerList.Remove(105);
            }
            var loopTo2 = thisPassword.LowerCases;
            for (x = 1; x <= loopTo2; x++)
            {
                picked = Next(lowerList.Count);
                tempResults.Add(lowerList[picked]);
            }
        }
        if (thisPassword.HowManySymbols > 0)
        {
            var loopTo3 = thisPassword.HowManySymbols;
            for (x = 1; x <= loopTo3; x++)
            {
                picked = Next(thisPassword.SymbolList.Count);
                string thisSym = thisPassword.SymbolList[picked];
                picked = VBCompat.AscW(thisSym);
                tempResults.Add(picked); // i think
            }
        }
        tempResults.ShuffleList(); //i now have this new list.  i might as well use this especially if i need random functions.
        string resultString = "";
        foreach (var item in tempResults)
        {
            resultString += VBCompat.ChrW(item);
        }
        return resultString;
    }
    public int GetRandomNumber(int maxNumber, int startingPoint = 1, BasicList<int>? previousList = null)
    {
        if (previousList != null)
        {
            if (previousList.Count == 0)
            {
                previousList = null;
            }
        }
        DoRandomize();
        int randNum;
        if (startingPoint > maxNumber)
        {
            ShowError();
        }
        if (previousList == null)
        {
            randNum = Next(startingPoint, maxNumber + 1); //plus 1 was worse.  trying -1
            return randNum;
        }
        HashSet<int> rndIndexes = previousList.Where(a => a >= startingPoint && a <= maxNumber).Distinct().ToHashSet();
        int howManyPossible = maxNumber - startingPoint + 1 - rndIndexes.Count;
        if (howManyPossible < 1)
        {
            ShowError();
        }
        for (int i = 1; i <= startingPoint - 1; i++)
        {
            rndIndexes.Add(i);
        }
        bool rets;
        do
        {
            int index = Next(maxNumber);
            rets = rndIndexes.Add(index + 1);
            if (rets == true)
            {
                return index + 1; //because 0 based
            }

        } while (true);
    }
    private static void ShowError()
    {
        throw new CustomBasicException("Random number could not be generated, range to narrow");
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
}