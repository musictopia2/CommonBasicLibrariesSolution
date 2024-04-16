using static CommonBasicLibraries.AdvancedGeneralFunctionsAndProcesses.RandomGenerator.RandomGenerator;
namespace CommonBasicLibraries.AdvancedGeneralFunctionsAndProcesses.RandomGenerator.Advanced;
public static class RandomExtensions
{
    public static string NextAddress(this IRandomNumberList random, int syllables = 2, bool shortSuffix = true) => $"{random.GetRandomNumber(6000, 100)} {NextStreet(random, syllables, shortSuffix)}";
    public static int NextAge(this IRandomNumberList random, EnumAgeRanges types = EnumAgeRanges.Adult) //this means if somebody wants to create a new version and have different rules it can.
    {
        var range = types switch
        {
            EnumAgeRanges.Child => [1, 12],
            EnumAgeRanges.Teen => [13, 19],
            EnumAgeRanges.Senior => [65, 100],
            EnumAgeRanges.All => [1, 100],
            _ => new[] { 18, 65 },
        };
        return random.GetRandomNumber(range[1], range[0]);
    }
    public static string NextEmail(this IRandomNumberList random, string? domain = null, int length = 7) => random.NextWord(length: length, syllablesCount: null) + "@" + (domain ?? random.NextDomainName());
    public static string NextGender(this IRandomNumberList random)
    {
        BasicList<string> thisList = ["Male", "Female"];
        thisList.SendRandoms(random);
        return thisList.GetRandomItem();
    }
    public static string NextGeohash(this IRandomNumberList random, int length = 7) => random.NextString(length, "0123456789bcdefghjkmnpqrstuvwxyz");
    public static string NextGUID(this IRandomNumberList random, int version = 5)
    {
        const string guidPool = "abcdef1234567890";
        const string variantPool = "ab89";
        string strFn(string pool, int len) => random.NextString(len, pool);
        return strFn(guidPool, 8) + "-" +
               strFn(guidPool, 4) + "-" +
               version +
               strFn(guidPool, 3) + "-" +
               strFn(variantPool, 1) +
               strFn(guidPool, 3) + "-" +
               strFn(guidPool, 12);
    }


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
    public static int NextHour(this IRandomNumberList random, bool twentyfourHours = true, int? min = null, int? max = null)
    {
        if (min < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(min), "min cannot be less than 0.");
        }
        if (twentyfourHours && max > 23)
        {
            throw new ArgumentOutOfRangeException(nameof(max), "max cannot be greater than 23 for twentyfourHours option.");
        }
        if (!twentyfourHours && max > 12)
        {
            throw new ArgumentOutOfRangeException(nameof(max), "max cannot be greater than 12.");
        }
        if (min > max)
        {
            throw new ArgumentOutOfRangeException(nameof(min), "min cannot be greater than max.");
        }
        min ??= (twentyfourHours ? 0 : 1);
        max ??= (twentyfourHours ? 23 : 12);
        return random.GetRandomNumber(max.Value, min.Value);
    }
    /// <summary>
    /// Returns a random IP Address.
    /// </summary>
    /// <returns>Returns a random IP Address.</returns>
    public static string NextIP(this IRandomNumberList random) => $"{random.GetRandomNumber(254, 1)}.{random.GetRandomNumber(255, 0)}.{random.GetRandomNumber(255, 0)}.{random.GetRandomNumber(254, 0)}";
    
    

    /// <summary>
    /// Generates a random millisecond.
    /// </summary>
    /// <returns>Returns random generated millisecond.</returns>
    public static int NextMillisecond(this IRandomNumberList random) => random.GetRandomNumber(999);

    /// <summary>
    /// Generates a random minute.
    /// </summary>
    /// <param name="min">Min value.</param>
    /// <param name="max">Max value.</param>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when <c>min</c> is less than 0.</exception>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when <c>max</c> is greater than 59.</exception>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when <c>min</c> is greater than <c>max</c>.</exception>
    /// <returns>Returns random generated minute.</returns>
    public static int NextMinute(this IRandomNumberList random, int min = 0, int max = 59)
    {
        if (min < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(min), "min cannot be less than 0.");
        }
        if (max > 59)
        {
            throw new ArgumentOutOfRangeException(nameof(max), "max cannot be greater than 59.");
        }
        if (min > max)
        {
            throw new ArgumentOutOfRangeException(nameof(min), "min cannot be greater than max.");
        }
        return random.GetRandomNumber(max, min);
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
    public static string NextMonth(this IRandomNumberList random, int min = 1, int max = 12)
    {
        if (min < 1)
        {
            throw new ArgumentOutOfRangeException(nameof(min), "min cannot be less than 1.");
        }
        if (max > 12)
        {
            throw new ArgumentOutOfRangeException(nameof(max), "max cannot be greater than 12.");
        }
        if (min > max)
        {
            throw new ArgumentOutOfRangeException(nameof(min), "min cannot be greater than max.");
        }
        var firsts = Months.Skip(min - 1).Take(max - 1).ToBasicList();
        firsts.SendRandoms(random);
        return firsts.GetRandomItem().Month;
    }


    /// <summary>
    /// Generates a random second.
    /// </summary>
    /// <returns>Returns random generated second.</returns>
    public static int NextSecond(this IRandomNumberList random) => random.GetRandomNumber(59, 0);

    /// <summary>
    /// Generates a random social security number.
    /// </summary>
    /// <param name="ssnFour">True to generate last 4 digits.</param>
    /// <param name="dashes">False to remove dashes.</param>
    /// <returns>Returns random generated social security number.</returns>
    public static string NextSSN(this IRandomNumberList random, bool ssnFour = false, bool dashes = true)
    {
        const string ssnPool = "1234567890";
        string ssn, dash = dashes ? "-" : "";
        if (!ssnFour)
        {
            ssn = random.NextString(3, ssnPool) + dash + random.NextString(2, ssnPool) +
                  dash + random.NextString(4, ssnPool);
        }
        else
        {
            ssn = random.NextString(4, ssnPool);
        }

        return ssn;
    }

    /// <summary>
    /// Generates a random street.
    /// </summary>
    /// <param name="syllables">Number of syllables.</param>
    /// <param name="shortSuffix">True to use short suffix.</param>
    /// <returns>Returns random generated street name.</returns>
    public static string NextStreet(this IRandomNumberList random, int syllables = 2, bool shortSuffix = true) => NextWord(random, syllablesCount: syllables).Capitalize() + " " + (shortSuffix
            ? StreetSuffix(random).Name
            : StreetSuffix(random).Abb);
    public static string NextString(this IRandomNumberList random, int howMany, string stringsToPick)
    {
        BasicList<char> tempList = stringsToPick.ToBasicList();
        tempList.SendRandoms(random); //make sure its this instance.
        StringBuilder thisStr = new();
        for (int i = 0; i < howMany; i++)
        {
            thisStr.Append(tempList.GetRandomItem());
        }
        return thisStr.ToString();
    }
    /// <summary>
    /// Returns a random twitter handle.
    /// </summary>
    /// <returns>Returns a random twitter handle.</returns>
    public static string NextTwitterName(this IRandomNumberList random) => $"@{random.NextWord()}";
    public static string NextHashtag(IRandomNumberList random) => $"#{random.NextWord()}";
    public static string NextDomainName(this IRandomNumberList random, string? tld = null) => NextWord(random) + "." + (tld ?? NextTopLevelDomain(random));
    public static string NextTopLevelDomain(this IRandomNumberList random)
    {
        Tlds.SendRandoms(random);
        return Tlds.GetRandomItem();
    }

    /// <summary>
    /// Generates a random year.
    /// </summary>
    /// <param name="min">Min value.</param>
    /// <param name="max">Max value.</param>
    /// <returns>Returns random generated year.</returns>
    public static int NextYear(this IRandomNumberList random, int min = 2000, int max = -1)
    {
        if (max == -1)
        {
            max = DateTime.Now.Year;
        }
        return random.GetRandomNumber(max, min);
    }
    /// <summary>
    /// Generates a random (U.S.) zip code.
    /// </summary>
    /// <param name="plusfour">True to return a Zip+4.</param>
    /// <returns>Returns random generated U.S. zip code.</returns>
    public static string NextZipCode(this IRandomNumberList random, bool plusfour = true)
    {
        var zip = random.GetDigits(5);
        if (!plusfour)
        {
            return zip;
        }
        zip += "-";
        string others = random.GetDigits(4);
        zip += others;
        return zip;
    }
    /// <summary>
    /// Returns a random url.
    /// </summary>
    /// <param name="protocol">Custom protocol.</param>
    /// <param name="domain">Custom domain.</param>
    /// <param name="domainPrefix">Custom domain prefix.</param>
    /// <param name="path">Url path.</param>
    /// <param name="extensions">A list of Url extensions to pick one from.</param>
    /// <returns>Returns a random url.</returns>
    public static string NextUrl(this IRandomNumberList random, string protocol = "http", string? domain = null, string? domainPrefix = null, string? path = null, BasicList<string>? extensions = null)
    {
        domain ??= random.NextDomainName();
        extensions?.SendRandoms(random);
        var ext = extensions != null && extensions.Count != 0 ? "." + extensions.GetRandomItem() : "";
        var dom = !string.IsNullOrEmpty(domainPrefix) ? domainPrefix + "." + domain : domain;
        return $"{protocol}://{dom}/{path}{ext}";
    }
    public static string GetDigits(this IRandomNumberList random, int howMany, int startAt = 0, int endAt = 9)
    {
        StringBuilder str = new();
        for (int i = 0; i < howMany; i++)
        {
            str.Append(random.GetRandomNumber(endAt, startAt));
        }
        return str.ToString();
    }

    #region Finance
    public static long NextCreditCardNumber(this IRandomNumberList random, string? cardType = null)
    {
        var (_, _, Code, Digits) = random.CcType(cardType);
        var toGenerate = Digits - Code.Length - 1;
        var number = Code;
        string group = random.GetDigits(toGenerate);
        number += group;
        number += CreditCardUtils.LuhnCalcualte(long.Parse(number));
        return long.Parse(number);
    }
    private static (string Company, string Abb, string Code, int Digits) CcType(this IRandomNumberList random, string? name = null)
    {
        (string Company, string Abb, string Code, int Digits) cc;
        CcTypes.SendRandoms(random);
        if (!string.IsNullOrEmpty(name))
        {
            cc = CcTypes.FirstOrDefault(tcc => tcc.Company == name || tcc.Abb == name);
            if (cc == default)
            {
                throw new ArgumentOutOfRangeException(nameof(name),
                    "Credit card type '" + name + "'' is not supported");
            }
        }
        else
        {
            cc = CcTypes.GetRandomItem();
        }

        return cc;
    }
    #endregion


    /// <summary>
    /// Returns a random character.
    /// </summary>
    /// <param name="pool">Characters pool</param>
    /// <param name="alpha">Set to True to use only an alphanumeric character.</param>
    /// <param name="symbols">Set to true to return only symbols.</param>
    /// <param name="casing">Default casing.</param>
    /// <returns>Returns a random character.</returns>
    private static char NextChar(this IRandomNumberList random, string? pool = null, bool alpha = false, bool symbols = false, EnumCasingRules casing = EnumCasingRules.MixedCase)
    {
        const string s = "!@#$%^&*()[]";
        string letters, p;
        if (alpha && symbols)
        {
            throw new ArgumentException("Cannot specify both alpha and symbols.");
        }
        if (casing == EnumCasingRules.LowerCase)
        {
            letters = CharsLower;
        }
        else if (casing == EnumCasingRules.UpperCase)
        {
            letters = CharsUpper;
        }
        else
        {
            letters = CharsLower + CharsUpper;
        }
        if (!string.IsNullOrEmpty(pool))
        {
            p = pool!;
        }
        else if (alpha)
        {
            p = letters;
        }
        else if (symbols)
        {
            p = s;
        }
        else
        {
            p = letters + Numbers + s;
        }
        BasicList<char> list = p.ToBasicList();
        list.SendRandoms(random);
        return list.GetRandomItem();
    }
    /// <summary>
    /// Return a semi-speakable syllable, 2 or 3 letters.
    /// </summary>
    /// <param name="length">Length of a syllable.</param>
    /// <param name="capitalize">True to capitalize a syllable.</param>
    /// <returns>Returns random generated syllable.</returns>
    private static string NextSyllable(this IRandomNumberList random, int length = 2, bool capitalize = false)
    {
        const string consonats = "bcdfghjklmnprstvwz";
        const string vowels = "aeiou";
        const string all = consonats + vowels;
        var text = "";
        var chr = -1;
        for (var i = 0; i < length; i++)
        {
            if (i == 0)
            {
                chr = NextChar(random,all);
            }
            else if (consonats.IndexOf((char)chr) == -1) //(consonats[chr] == -1)
            {
                chr = NextChar(random, consonats);
            }
            else
            {
                chr = NextChar(random, vowels);
            }

            text += (char)chr;
        }
        if (capitalize)
        {
            text = text.Capitalize();
        }
        return text;
    }
    /// <summary>
    /// Return a semi-pronounceable random (nonsense) word.
    /// </summary>
    /// <param name="capitalize">True to capitalize a word.</param>
    /// <param name="syllablesCount">Number of syllables which the word will have.</param>
    /// <param name="length">Length of a word.</param>
    /// <returns>Returns random generated word.</returns>
    private static string NextWord(this IRandomNumberList random, bool capitalize = false, int? syllablesCount = 2, int? length = null)
    {
        if (syllablesCount != null && length != null)
        {
            throw new ArgumentException("Cannot specify both syllablesCount AND length.");
        }
        var text = "";
        if (length.HasValue)
        {
            do
            {
                text += NextSyllable(random);
            } while (text.Length < length.Value);

            text = text.Substring(0, length.Value);
        }
        else if (syllablesCount.HasValue)
        {
            for (var i = 0; i < syllablesCount.Value; i++)
            {
                text += NextSyllable(random);
            }
        }
        if (capitalize)
        {
            text = text.Capitalize();
        }

        return text;
    }
    private static (string Name, string Abb) StreetSuffix(this IRandomNumberList random)
    {
        StreetSuffixes.SendRandoms(random);
        return StreetSuffixes.GetRandomItem();
    }
}