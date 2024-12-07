using System.Globalization;
using System.Text.RegularExpressions;
namespace CommonBasicLibraries.AdvancedGeneralFunctionsAndProcesses.BasicExtensions;
public static partial class Strings
{
    public static string BackSpaceRemoveEnding0s(this string payLoad)
    {
        string output = payLoad.TrimEnd(['0']);
        return output;
    }
    private static string _monthReplace = "";
    public static int FindMonthInStringLine(this string thisStr)
    {
        BasicList<string> possList = ["January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December"];
        int possNum;
        possNum = 0;
        _monthReplace = "";
        int currentNum;
        foreach (var thisPoss in possList)
        {
            if (thisStr.Contains(thisPoss) == true)
            {
                currentNum = thisPoss.GetMonthID();
                if (currentNum > 0)
                {
                    _monthReplace = thisPoss;
                    if (possNum > 0)
                    {
                        throw new CustomBasicException("There should not have been 2 months in the same line.  Rethink");
                    }
                    possNum = currentNum;
                }
            }
        }

        return possNum;
    }
    public static BasicList<string> CommaDelimitedList(string payLoad)
    {
        return payLoad.Split(",").ToBasicList();
    }
    public static bool IsNumeric(this string thisStr)
    {
        return int.TryParse(thisStr, out _);
    }
    public static BasicList<string> SplitStringEliminateMonth(this string thisStr)
    {
        thisStr = thisStr.Trim();
        if (_monthReplace == null == true || _monthReplace == "")
        {
            return [thisStr];
        }
        var thisList = thisStr.Split(_monthReplace);
        if (thisList.Length != 2)
        {
            throw new CustomBasicException("There should be only 2 items splited, not " + thisList.Length); //i am guessing has to be this way now.
        }
        BasicList<string> newList = [];
        foreach (var thisItem in thisList)
        {
            newList.Add(thisItem.Trim());
        }
        return newList;
    }
    public static int GetMonthID(this string monthString)
    {

        return monthString switch
        {
            null => 0,
            "January" => 1,
            "February" => 2,
            "March" => 3,
            "April" => 4,
            "May" => 5,
            "June" => 6,
            "July" => 7,
            "August" => 8,
            "September" => 9,
            "October" => 10,
            "November" => 11,
            "December" => 12,
            _ => 0
        };
    }
    public static BasicList<string> GetSentences(this string sTextToParse)
    {
        BasicList<string> al = [];
        string sTemp = sTextToParse;
        sTemp = sTemp.Replace(Environment.NewLine, " ");
        string[] customSplit = [".", "?", "!", ":"];
        var splits = sTemp.Split(customSplit, StringSplitOptions.RemoveEmptyEntries).ToList();
        int pos;
        foreach (var thisSplit in splits)
        {
            pos = sTemp.IndexOf(thisSplit);
            var thisChar = sTemp.Trim().ToCharArray();
            if (pos + thisSplit.Length <= thisChar.Length - 1)
            {
                char c = thisChar[pos + thisSplit.Length];
                al.Add(thisSplit.Trim() + c.ToString());
                sTemp = sTemp.Replace(thisSplit, ""); // because already accounted for.
            }
            else
            {
                al.Add(thisSplit);
            }
        }
        if (al.First().StartsWith('"') == true)
        {
            throw new CustomBasicException("I don't think the first one can start with quotes");
        }
        int x;
        var loopTo = al.Count - 1; // this is used so the quotes go into the proper places.
        for (x = 1; x <= loopTo; x++)
        {
            string firstItem;
            string secondItem;
            firstItem = al[x - 1];
            secondItem = al[x];
            if (secondItem.StartsWith('"') == true)
            {
                al[x] = al[x].Substring(1); // i think
                al[x] = al[x].Trim();
                al[x - 1] = al[x - 1] + "\"";
                al[x - 1] = al[x - 1].Trim();
            }
            else if (secondItem.StartsWith(')') == true)
            {
                al[x] = al[x].Substring(1); // i think
                al[x] = al[x].Trim();
                al[x - 1] = al[x - 1] + ")";
                al[x - 1] = al[x - 1].Trim();
            }
            else if (secondItem.Length == 1)
            {
                var ThisStr = secondItem.ToString();
                al[x] = al[x].Substring(1); // i think
                al[x] = al[x].Trim();
                al[x - 1] = al[x - 1] + ThisStr;
                al[x - 1] = al[x - 1].Trim();
            }
        }
        int numbers = al.Where(Items => Items == "").Count();
        int opening = al.Where(Items => Items == "(").Count();
        int closing = al.Where(Items => Items == ")").Count();
        foreach (var thisItem in al)
        {
            if (numbers == 1 || numbers == 3)
            {
                throw new CustomBasicException("Quotes are not correct.  Has " + numbers + " Quotes");
            }
            if (opening != closing)
            {
                throw new CustomBasicException("Opening and closing much match for ( and ) characters");
            }
        }
        al = (from xx in al
              where xx != ""
              select xx).ToBasicList();
        return al;
    }
    public static string StripHtml(this string htmlText) //unfortunately not perfect.
    {
        var thisText = MyRegex().Replace(htmlText, "");
        if (thisText.Contains("<sup") == true)
        {
            var index = thisText.IndexOf("<sup");
            thisText = thisText.Substring(0, index);
        }
        if (thisText.Contains("<div class=" + "\"") == true)
        {
            thisText = thisText.Replace("<div class=" + "\"", "");
        }
        if (thisText.Contains("<a") == true)
        {
            var index = thisText.IndexOf("<a");
            thisText = thisText.Substring(0, index);
        }
        thisText = thisText.Replace("[a]", "");
        thisText = thisText.Replace("[b]", ""); // because even if its b, needs to go away as well.
        thisText = thisText.Replace("[c]", "");
        thisText = thisText.Replace("[d]", "");
        thisText = thisText.Replace("[e]", "");
        thisText = thisText.Replace("[f]", "");
        thisText = thisText.Replace("[g]", "");
        thisText = thisText.Replace("[h]", "");
        thisText = thisText.Replace("[i]", "");
        thisText = thisText.Replace("[j]", "");
        thisText = thisText.Replace("[k]", "");
        thisText = thisText.Replace("[l]", "");
        thisText = thisText.Replace("[m]", "");
        thisText = thisText.Replace("[n]", "");
        thisText = thisText.Replace("[o]", "");
        thisText = thisText.Replace("[p]", "");
        thisText = thisText.Replace("[q]", "");
        thisText = thisText.Replace("[r]", "");
        thisText = thisText.Replace("[s]", "");
        thisText = thisText.Replace("[t]", "");
        thisText = thisText.Replace("[u]", "");
        thisText = thisText.Replace("[v]", "");
        thisText = thisText.Replace("[w]", "");
        thisText = thisText.Replace("[x]", "");
        thisText = thisText.Replace("[y]", "");
        thisText = thisText.Replace("[z]", "");
        var nextText = System.Net.WebUtility.HtmlDecode(thisText);
        return nextText.Trim();
    }
    public static string TextWithSpaces(this string thisText)
    {
        string newText = thisText;
        int x = 0;
        string finals = "";
        foreach (var thisChar in newText)
        {
            bool rets = int.TryParse(thisChar.ToString(), out _);
            if (char.IsLower(thisChar) == false && x > 0 && rets == false)
            {
                finals += " " + thisChar;
            }
            else
            {
                finals += thisChar;
            }
            x++;
        }
        return finals;
    }
    public static int GetSeconds(this string timeString)
    {
        var tempList = timeString.Split(":").ToBasicList();
        if (tempList.Count > 3)
        {
            throw new CustomBasicException("Can't handle more than 3 :");
        }
        if (tempList.Count == 3)
        {
            int firstNum;
            int secondNum;
            int thirdNum;
            firstNum = int.Parse(tempList.First().ToString());
            secondNum = int.Parse(tempList[1]);
            thirdNum = int.Parse(tempList.Last());
            int firstSecs;
            firstSecs = firstNum * 60 * 60;
            var secondSecs = secondNum * 60;
            var thirdSecs = thirdNum;
            return firstSecs + secondSecs + thirdSecs;
        }
        if (tempList.Count == 2)
        {
            int firstSecs = int.Parse(tempList.First()) * 60;
            return firstSecs + int.Parse(tempList.Last());
        }
        if (tempList.Count == 0)
        {
            throw new CustomBasicException("I think its wrong");
        }
        if (tempList.Count == 1)
        {
            throw new CustomBasicException("Should just return as is");
        }
        throw new CustomBasicException("Not sure");
    }
    public static bool IsValidDate(this string dateStr, out DateOnly? newDate)
    {
        bool rets = DateOnly.TryParseExact(dateStr, "MMddyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateOnly temps);
        if (rets)
        {
            newDate = temps;
            return true;
        }
        rets = DateOnly.TryParseExact(dateStr, "MMddyyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out temps);
        if (rets)
        {
            newDate = temps;
            return true;
        }
        rets = DateOnly.TryParse(dateStr, out temps);
        if (rets)
        {
            newDate = temps;
            return true;
        }
        newDate = null;
        return false;
    }
    public static BasicList<Tuple<string, int?>> GetStringIntegerCombos(this string thisStr)
    {
        BasicList<Tuple<string, int?>> thisList = [];
        if (thisStr == "")
        {
            return thisList;
        }
        string item1;
        item1 = "";
        string item2;
        item2 = "";
        int x = 0;
        bool hadNumber = false;
        bool hadStr = false;
        bool reject = false;
        bool lastAlpha = false;
        foreach (var thisItem in thisStr.ToList())
        {
            x += 1;
            bool isAlpha;
            isAlpha = thisItem.IsAlpha(true);
            bool isInt;
            isInt = thisItem.IsInteger();
            if (isInt == true)
            {
                hadNumber = true;
            }
            else if (isAlpha == true)
            {
                hadStr = true;
            }
            if (isInt == false && isAlpha == false)
            {
                reject = true;
                break;
            }
            if (isAlpha == true && isInt == true)
            {
                throw new CustomBasicException("Cannot be both alpha and integers.");
            }
            if (x == 1)
            {
                item1 = thisItem.ToString();
            }
            else if (lastAlpha == true && isAlpha == true)
            {
                item1 += thisItem;
            }
            else if (lastAlpha == false && isInt == true)
            {
                item2 += thisItem;
            }
            else if (lastAlpha == true && isInt == true)
            {
                item2 = thisItem.ToString();
            }
            else
            {
                int? thisInt;
                if (item2 == "")
                {
                    thisInt = default;
                }
                else
                {
                    thisInt = int.Parse(item2);
                }
                Tuple<string, int?> thisTuple = new(item1, thisInt);
                thisList.Add(thisTuple);
                item1 = thisItem.ToString();
                item2 = null!;
            }
            lastAlpha = isAlpha;
        }
        if (reject == true)
        {
            thisList = [];
            return thisList;
        }
        if (hadNumber == false && thisList.Count == 0)
        {
            thisList.Add(new Tuple<string, int?>(thisStr, default));
        }
        else if (hadStr == false && thisList.Count == 0 && hadNumber == true)
        {
            thisList.Add(new Tuple<string, int?>("", int.Parse(thisStr)));
        }
        else if (hadStr == true && hadNumber == true)
        {
            int? thisInt;
            if (item2 == "")
            {
                thisInt = default;
            }
            else
            {
                thisInt = int.Parse(item2);
            }
            Tuple<string, int?> thisTuple = new(item1, thisInt);
            thisList.Add(thisTuple);
        }
        else
        {
            thisList = [];
            return thisList;
        }
        return thisList;
    }
    public static bool IsCompleteAlpha(this string thisStr)
    {
        if (thisStr.Where(xx => xx.IsAlpha() == true).Any() == true)
        {
            return false;
        }
        return true;
    }
    public static BasicList<string> GetRange(this BasicList<string> thisList, string startWithString, string endWithString)
    {
        int firstIndex = thisList.IndexOf(startWithString);
        int secondIndex = thisList.IndexOf(endWithString);
        if (firstIndex == -1)
        {
            throw new CustomBasicException(startWithString + " is not found for the start string");
        }
        if (secondIndex == -1)
        {
            throw new CustomBasicException(endWithString + " is not found for the end string");
        }
        if (firstIndex > secondIndex)
        {
            throw new CustomBasicException("The first string appears later in the last than the second string");
        }
        return thisList.Skip(firstIndex).Take(secondIndex - firstIndex + 1).ToBasicList();
    }
    public static BasicList<string> Split(this string thisStr, string words)
    {
        int oldCount = thisStr.Length;
        BasicList<string> tempList = [];
        do
        {
            if (thisStr.Contains(words) == false)
            {
                if (thisStr != "")
                {
                    tempList.Add(thisStr);
                }
                return tempList;
            }
            tempList.Add(thisStr.Substring(0, thisStr.IndexOf(words)));
            if (tempList.Count > oldCount)
            {
                throw new CustomBasicException("Can't be more than " + oldCount);
            }
            thisStr = thisStr.Substring(thisStr.IndexOf(words) + words.Length);
        }
        while (true);
    }
    public static string Join(this BasicList<string> thisList, string words)
    {
        string newWord = "";
        thisList.ForEach(temps =>
        {
            if (newWord == "")
            {
                newWord = temps;
            }
            else
            {
                newWord = newWord + words + temps;
            }
        });
        return newWord;
    }
    public static string Join(this BasicList<string> thisList, string words, int skip, int take)
    {
        var newList = thisList.Skip(skip).ToBasicList();
        if (take > 0)
        {
            newList = thisList.Take(take).ToBasicList();
        }
        return Join(newList, words);
    }
    public static bool ContainNumbers(this string thisStr)
    {
        return thisStr.Where(Items => char.IsNumber(Items) == true).Any();
    }
    public static string PartialString(this string fullString, string searchFor, bool beginning)
    {
        if (fullString.Contains(searchFor) == false)
        {
            throw new CustomBasicException(searchFor + " is not contained in " + fullString);
        }
        if (beginning == true)
        {
            return fullString.Substring(0, fullString.IndexOf(searchFor)).Trim();
        }
        return fullString.Substring(fullString.IndexOf(searchFor) + searchFor.Length).Trim();
    }
    public static bool ContainsFromList(this string thisStr, BasicList<string> thisList)
    {
        string temps;
        temps = thisStr.ToLower();
        foreach (var thisItem in thisList)
        {
            var news = thisItem.ToLower();
            if (temps.Contains(news) == true)
            {
                return true;
            }
        }
        return false;
    }
    public static bool PostalCodeValidForUS(this string postalCode)
    {
        if (postalCode.Length < 5)
        {
            return false;
        }
        if (postalCode.Length == 5)
        {
            return int.TryParse(postalCode, out _);
        }
        if (postalCode.Length == 9)
        {
            return int.TryParse(postalCode, out _);
        }
        if (postalCode.Length == 10)
        {
            int index;
            index = postalCode.IndexOf('-');
            if (index != 5)
            {
                return false;
            }
            postalCode = postalCode.Replace("-", "");
            return int.TryParse(postalCode, out _);
        }
        return false;
    }
    public static string GetWords(this string thisWord) // each upper case will represent a word.  for now; will not publish to bob's server.  if i need this function or needed for bob; then rethink that process
    {
        var tempList = thisWord.ToBasicList();
        int x = 0;
        string newText = "";
        if (thisWord.Contains(' ') == true)
        {
            throw new CustomBasicException(thisWord + " cannot contain spaces already");
        }
        tempList.ForEach(thisItem =>
        {
            if (char.IsUpper(thisItem) == true && x > 0)
            {
                newText = newText + " " + thisItem;
            }
            else
            {
                newText += thisItem;
            }
            x += 1;
        });
        newText = newText.Replace("I P ", " IP ");
        return newText;
    }
    public static string ToTitleCase(this string info, bool replaceUnderstores = true)
    {
        if (replaceUnderstores)
        {
            info = info.Replace("_", " ");
        }
        TextInfo currentTextInfo = CultureInfo.CurrentCulture.TextInfo;
        string output = currentTextInfo.ToTitleCase(info);
        return output;
    }
    public static string ConvertCase(this string info, bool doAll = true)
    {
        string tempStr = "";
        bool isSpaceOrDot = false;
        if (doAll)
        {
            var loopTo = info.Length - 1;
            for (int i = 0; i <= loopTo; i++)
            {
                if (info[i].ToString() != " " & info[i].ToString() != ".")
                {
                    if (i == 0 | isSpaceOrDot)
                    {
                        tempStr += char.ToUpper(info[i]);
                        isSpaceOrDot = false;
                    }
                    else
                    {
                        tempStr += char.ToLower(info[i]);
                    }
                }
                else
                {
                    isSpaceOrDot = true;
                    tempStr += info[i];
                }
            }
        }
        else
        {
            var loopTo1 = info.Length - 1;
            for (int i = 0; i <= loopTo1; i++)
            {
                if (info[i].ToString() != " " & info[i].ToString() != ".")
                {
                    if (isSpaceOrDot)
                    {
                        tempStr += char.ToUpper(info[i]);
                        isSpaceOrDot = false;
                    }
                    else if (i == 0)
                    {
                        tempStr += char.ToUpper(info[0]);
                    }
                    else
                    {
                        tempStr += char.ToLower(info[i]);
                    }
                }
                else
                {
                    if (info[i].ToString() != " ")
                    {
                        isSpaceOrDot = !isSpaceOrDot;
                    }
                    tempStr += info[i];
                }
            }
        }
        return tempStr;
    }
    public static string FixBase64ForFileData(this string str_Image)
    {
        // *** Need to clean up the text in case it got corrupted travelling in an XML file
        // i think its best to have as public.  because its possible its only corrupted because of this.
        // has had the experience before with smart phones.
        // however; with mango and windows phones 7; I can use a compact edition database (which would be very helpful).
        // if doing this; then what would have to happen is I would have to have a method to check back in the music information.
        // maybe needs to be xml afterall (don't know though).  otherwise; may have to do serializing/deserializing.
        // some stuff is iffy at this point.
        StringBuilder sbText = new(str_Image, str_Image.Length);
        sbText.Replace(@"\r\n", string.Empty);
        sbText.Replace(" ", string.Empty);
        return sbText.ToString();
    }
    public static void SaveFile(this string data, string path)
    {
        byte[] Bytes = Convert.FromBase64String(data);

        using FileStream FileStream = new(bb1.GetCleanedPath(path), FileMode.Create, FileAccess.ReadWrite, FileShare.None);
        FileStream.Write(Bytes, 0, Bytes.Length);
        FileStream.Flush();
        FileStream.Close();
    }
    public async static Task SaveFileAsync(this string data, string path)
    {
        byte[] bytes = Convert.FromBase64String(data);
        using FileStream fileStream = new(bb1.GetCleanedPath(path), FileMode.Create, FileAccess.ReadWrite, FileShare.None);
        await fileStream.WriteAsync(bytes);
        await fileStream.FlushAsync();
        fileStream.Close();
    }
    public static string GetFileData(this string path)
    {
        using FileStream fileStream = new(bb1.GetCleanedPath(path), FileMode.Open, FileAccess.ReadWrite, FileShare.None);
        byte[] bytes = new byte[fileStream.Length - 1 + 1];
        fileStream.ReadExactly(bytes, 0, (int)fileStream.Length);
        fileStream.Close();
        return Convert.ToBase64String(bytes);
    }
    public async static Task<string> GetFileDataAsync(this string path)
    {
        using FileStream fileStream = new(bb1.GetCleanedPath(path), FileMode.Open, FileAccess.ReadWrite, FileShare.None);
        byte[] Bytes = new byte[fileStream.Length - 1 + 1];
        await fileStream.ReadExactlyAsync(Bytes.AsMemory(0, (int)fileStream.Length));
        fileStream.Close();
        return Convert.ToBase64String(Bytes);
    }
    public static BasicList<string> GenerateSentenceList(this string entireText)
    {
        return entireText.Split(Constants.VBCrLf).ToBasicList();
    }
    public static string BodyFromStringList(this BasicList<string> thisList)
    {
        if (thisList.Count == 0)
        {
            throw new CustomBasicException("Must have at least one item in order to get the body from the string list");
        }
        StrCat cats = new();
        thisList.ForEach(ThisItem =>
        {
            cats.AddToString(ThisItem, Constants.VBCrLf);
        });
        return cats.GetInfo();
    }
    public static int GetColumnNumber(this string columnString) // will be 0 based
    {
        string newStr = columnString.ToLower();
        BasicList<string> AlphabetList = ["a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z"];
        var tempList = newStr.ToList();
        if (tempList.Count > 2)
        {
            throw new CustomBasicException("Currently; only 2 digit strings can be done for figuring out the column number");
        }
        var index = AlphabetList.IndexOf(tempList.First().ToString());
        if (index == -1)
        {
            throw new CustomBasicException(tempList.First() + " is not part of the alphabet for the first digit of the string");
        }
        if (tempList.Count == 1)
        {
            return index;
        }
        index += 1;
        var finalIndex = AlphabetList.IndexOf(tempList.Last().ToString());
        if (finalIndex == -1)
        {
            throw new CustomBasicException(tempList.Last() + " is not part of the alphabet for the last digit of the string");
        }
        return index * 26 + finalIndex;
    }
    public static (int Days, int Hours, int Minutes) GetTime(this string timeString)
    {
        CustomBasicException thisEx = new("Incorrect.  Should have used validation");
        _previousTime = (0, 0, 0);
        var tempList = timeString.Split(':').ToList();
        bool rets;
        int newInt;
        if (tempList.Count == 1)
        {
            rets = int.TryParse(timeString, out newInt);
            if (rets == false)
            {
                throw thisEx;
            }
            return (0, 0, newInt);
        }
        if (tempList.Count > 3)
        {
            throw thisEx;
        }
        BasicList<int> newList = [];
        foreach (var thisItem in tempList)
        {
            rets = int.TryParse(thisItem, out newInt);
            if (rets == false)
            {
                throw thisEx;
            }
            newList.Add(newInt);
        }
        int d;
        int h;
        int m;
        if (newList.Count == 2)
        {
            d = 0;
            h = newList.First();
            m = newList.Last();
        }
        else
        {
            d = newList.First();
            h = newList[1];
            m = newList.Last();
        }
        _previousString = timeString;
        _previousTime = (d, h, m);
        return (d, h, m);
    }
    private static string _previousString = "";
    private static (int Days, int Hours, int Minutes) _previousTime;
    public static int GetTotalSeconds(this string timeString)
    {
        if (string.IsNullOrWhiteSpace(timeString) == true)
        {
            throw new CustomBasicException("Never got the time using the GetTime format");
        }
        if (timeString != _previousString)
        {
            throw new CustomBasicException("You did not use the sanme string as when using the GetTime function");
        }
        TimeSpan thisSpan = new(_previousTime.Days, _previousTime.Hours, _previousTime.Minutes, 0);
        return (int)thisSpan.TotalSeconds;
    }
    public static string GetDoubleQuoteString(this string value) => $"{Constants.DoubleQuote}{value}{Constants.DoubleQuote}";
    public static T ParseEnum<T>(this string value)
    {
        return (T)Enum.Parse(typeof(T), value, true);
    }

    [GeneratedRegex("<.*?>")]
    private static partial Regex MyRegex();
}