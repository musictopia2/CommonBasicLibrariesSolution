using System.Globalization;
namespace CommonBasicLibraries.AdvancedGeneralFunctionsAndProcesses.BasicExtensions;
public static partial class Strings
{
    private static string _monthReplace = "";
    private static string _previousString = "";
    private static (int Days, int Hours, int Minutes) _previousTime;

    extension(string payLoad)
    {
        public string BackSpaceRemoveEnding0s
        {
            get
            {
                string output = payLoad.TrimEnd(['0']);
                return output;
            }
        }
        //i chose this time to keep my code and still keep as property.
        public int FindMonthInStringLine
        {
            get
            {
                BasicList<string> possList = ["January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December"];
                int possNum;
                possNum = 0;
                _monthReplace = "";
                int currentNum;
                foreach (var thisPoss in possList)
                {
                    if (payLoad.Contains(thisPoss) == true)
                    {
                        currentNum = thisPoss.GetMonthID;
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
        }
        public BasicList<string> CommaDelimitedList => payLoad.Split(",").ToBasicList();
        public bool IsNumeric => int.TryParse(payLoad, out _);
        public BasicList<string> SplitStringEliminateMonth
        {
            get
            {
                string tempPayLoad = payLoad.Trim(); // work on local copy
                if (string.IsNullOrEmpty(_monthReplace))
                {
                    return new BasicList<string> { tempPayLoad };
                }

                var thisList = tempPayLoad.Split(_monthReplace);
                if (thisList.Length != 2)
                {
                    throw new CustomBasicException("There should be only 2 items split, not " + thisList.Length);
                }

                BasicList<string> newList = new BasicList<string>();
                foreach (var thisItem in thisList)
                {
                    newList.Add(thisItem.Trim());
                }
                return newList;
            }
        }
        public int GetMonthID
        {
            get
            {
                return payLoad switch
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
        }
        public BasicList<string> GetSentences()
        {
            BasicList<string> al = [];
            string sTemp = payLoad;
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
        public string StripHtml()
        {
            var thisText = MyRegex().Replace(payLoad, "");
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
        public string TextWithSpaces
        {
            get
            {
                string newText = payLoad;
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
        }
        public int GetSeconds()
        {
            var tempList = payLoad.Split(":").ToBasicList();
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
        public bool IsValidDate(out DateOnly? newDate)
        {
            bool rets = DateOnly.TryParseExact(payLoad, "MMddyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateOnly temps);
            if (rets)
            {
                newDate = temps;
                return true;
            }
            rets = DateOnly.TryParseExact(payLoad, "MMddyyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out temps);
            if (rets)
            {
                newDate = temps;
                return true;
            }
            rets = DateOnly.TryParse(payLoad, out temps);
            if (rets)
            {
                newDate = temps;
                return true;
            }
            newDate = null;
            return false;
        }
        public BasicList<Tuple<string, int?>> GetStringIntegerCombos()
        {
            BasicList<Tuple<string, int?>> thisList = [];
            if (payLoad == "")
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
            foreach (var thisItem in payLoad.ToList())
            {
                x += 1;
                bool isAlpha;
                isAlpha = thisItem.IsAlpha(true);
                bool isInt;
                isInt = thisItem.IsInteger;
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
                thisList.Add(new Tuple<string, int?>(payLoad, default));
            }
            else if (hadStr == false && thisList.Count == 0 && hadNumber == true)
            {
                thisList.Add(new Tuple<string, int?>("", int.Parse(payLoad)));
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
        public bool IsCompleteAlpha => payLoad.All(x => x.IsAlphaNoDots);
        public BasicList<string> Split(string words)
        {
            int oldCount = payLoad.Length;
            BasicList<string> tempList = [];
            do
            {
                if (payLoad.Contains(words) == false)
                {
                    if (payLoad != "")
                    {
                        tempList.Add(payLoad);
                    }
                    return tempList;
                }
                tempList.Add(payLoad.Substring(0, payLoad.IndexOf(words)));
                if (tempList.Count > oldCount)
                {
                    throw new CustomBasicException("Can't be more than " + oldCount);
                }
                payLoad = payLoad.Substring(payLoad.IndexOf(words) + words.Length);
            }
            while (true);
        }
        public bool ContainNumbers => payLoad.Where(Items => char.IsNumber(Items) == true).Any();
        public string PartialString(string searchFor, bool beginning)
        {
            if (payLoad.Contains(searchFor) == false)
            {
                throw new CustomBasicException(searchFor + " is not contained in " + payLoad);
            }
            if (beginning == true)
            {
                return payLoad.Substring(0, payLoad.IndexOf(searchFor)).Trim();
            }
            return payLoad.Substring(payLoad.IndexOf(searchFor) + searchFor.Length).Trim();
        }
        public bool ContainsFromList(BasicList<string> thisList)
        {
            string temps;
            temps = payLoad.ToLower();
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
        public bool PostalCodeValidForUS
        {
            get
            {
                if (payLoad.Length == 5 || payLoad.Length == 9)
                {
                    return int.TryParse(payLoad, out _);
                }
                if (payLoad.Length == 10 && payLoad[5] == '-')
                {
                    string temp = payLoad.Replace("-", "");
                    return int.TryParse(temp, out _);
                }
                return false;
            }
        }
        public string GetWords
        {
            get
            {
                // Already human-readable → return as-is
                if (payLoad.Contains(' '))
                {
                    return payLoad;
                }

                // Convert PascalCase / CamelCase to spaced words
                string newText = string.Concat(
                    payLoad.Select((c, i) =>
                        i > 0 && char.IsUpper(c)
                            ? " " + c
                            : c.ToString()
                    )
                );

                // Preserve special acronyms
                return newText.Replace("I P ", " IP ");
            }
        }
        public string ToTitleCase(bool replaceUnderstores = true)
        {
            if (replaceUnderstores)
            {
                payLoad = payLoad.Replace("_", " ");
            }
            TextInfo currentTextInfo = CultureInfo.CurrentCulture.TextInfo;
            string output = currentTextInfo.ToTitleCase(payLoad);
            return output;
        }
        public string CapitalizeFirstLetter =>
            string.IsNullOrEmpty(payLoad) || char.IsUpper(payLoad[0])
                ? payLoad
                : char.ToUpper(payLoad[0]) + payLoad.Substring(1);
        public string ConvertCase(bool doAll = true)
        {
            string tempStr = "";
            bool isSpaceOrDot = false;
            if (doAll)
            {
                var loopTo = payLoad.Length - 1;
                for (int i = 0; i <= loopTo; i++)
                {
                    if (payLoad[i].ToString() != " " & payLoad[i].ToString() != ".")
                    {
                        if (i == 0 | isSpaceOrDot)
                        {
                            tempStr += char.ToUpper(payLoad[i]);
                            isSpaceOrDot = false;
                        }
                        else
                        {
                            tempStr += char.ToLower(payLoad[i]);
                        }
                    }
                    else
                    {
                        isSpaceOrDot = true;
                        tempStr += payLoad[i];
                    }
                }
            }
            else
            {
                var loopTo1 = payLoad.Length - 1;
                for (int i = 0; i <= loopTo1; i++)
                {
                    if (payLoad[i].ToString() != " " & payLoad[i].ToString() != ".")
                    {
                        if (isSpaceOrDot)
                        {
                            tempStr += char.ToUpper(payLoad[i]);
                            isSpaceOrDot = false;
                        }
                        else if (i == 0)
                        {
                            tempStr += char.ToUpper(payLoad[0]);
                        }
                        else
                        {
                            tempStr += char.ToLower(payLoad[i]);
                        }
                    }
                    else
                    {
                        if (payLoad[i].ToString() != " ")
                        {
                            isSpaceOrDot = !isSpaceOrDot;
                        }
                        tempStr += payLoad[i];
                    }
                }
            }
            return tempStr;
        }
        public string FixBase64ForFileData =>
            payLoad.Replace(@"\r\n", "").Replace(" ", "");
        public void SaveFile(string path)
        {
            byte[] Bytes = Convert.FromBase64String(payLoad);

            using FileStream FileStream = new(bb1.GetCleanedPath(path), FileMode.Create, FileAccess.ReadWrite, FileShare.None);
            FileStream.Write(Bytes, 0, Bytes.Length);
            FileStream.Flush();
            FileStream.Close();
        }
        public async Task SaveFileAsync(string path)
        {
            byte[] bytes = Convert.FromBase64String(payLoad);
            using FileStream fileStream = new(bb1.GetCleanedPath(path), FileMode.Create, FileAccess.ReadWrite, FileShare.None);
            await fileStream.WriteAsync(bytes);
            await fileStream.FlushAsync();
            fileStream.Close();
        }
        public string GetFileData()
        {
            using FileStream fileStream = new(bb1.GetCleanedPath(payLoad), FileMode.Open, FileAccess.ReadWrite, FileShare.None);
            byte[] bytes = new byte[fileStream.Length - 1 + 1];
            fileStream.ReadExactly(bytes, 0, (int)fileStream.Length);
            fileStream.Close();
            return Convert.ToBase64String(bytes);
        }
        public async Task<string> GetFileDataAsync()
        {
            using FileStream fileStream = new(bb1.GetCleanedPath(payLoad), FileMode.Open, FileAccess.ReadWrite, FileShare.None);
            byte[] Bytes = new byte[fileStream.Length - 1 + 1];
            await fileStream.ReadExactlyAsync(Bytes.AsMemory(0, (int)fileStream.Length));
            fileStream.Close();
            return Convert.ToBase64String(Bytes);
        }
        public BasicList<string> GenerateSentenceList => payLoad.Split(Constants.VBCrLf).ToBasicList();
        //was get but now not necessary since this is a property now.
        public int ColumnNumber
        {
            get
            {
                string newStr = payLoad.ToLower();
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
        }
        public (int Days, int Hours, int Minutes) Time
        {
            get
            {
                CustomBasicException thisEx = new("Incorrect.  Should have used validation");
                _previousTime = (0, 0, 0);
                var tempList = payLoad.Split(':').ToList();
                bool rets;
                int newInt;
                if (tempList.Count == 1)
                {
                    rets = int.TryParse(payLoad, out newInt);
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
                _previousString = payLoad;
                _previousTime = (d, h, m);
                return (d, h, m);
            }
        }
        public int TotalSeconds
        {
            get
            {
                if (string.IsNullOrWhiteSpace(payLoad) == true)
                {
                    throw new CustomBasicException("Never got the time using the GetTime format");
                }
                if (payLoad != _previousString)
                {
                    throw new CustomBasicException("You did not use the sanme string as when using the GetTime function");
                }
                TimeSpan thisSpan = new(_previousTime.Days, _previousTime.Hours, _previousTime.Minutes, 0);
                return (int)thisSpan.TotalSeconds;
            }
        }
        public string DoubleQuoteString => $"{Constants.DoubleQuote}{payLoad}{Constants.DoubleQuote}";
        public T ParseEnum<T>() => (T)Enum.Parse(typeof(T), payLoad, true);
    }
    extension(IList<string> list)
    {
        public IList<string> GetRange(string startWithString, string endWithString)
        {
            int firstIndex = list.IndexOf(startWithString);
            int secondIndex = list.IndexOf(endWithString);
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
            return list.Skip(firstIndex).Take(secondIndex - firstIndex + 1).ToBasicList();
        }
        public string Join(string words)
        {
            string newWord = "";
            foreach (var item in list)
            {
                if (newWord == "")
                {
                    newWord = item;
                }
                else
                {
                    newWord = newWord + words + item;
                }
            }
            return newWord;
        }
        public string Join(string words, int skip, int take)
        {
            var newList = list.Skip(skip).ToBasicList();
            if (take > 0)
            {
                newList = list.Take(take).ToBasicList();
            }
            return newList.Join(words);
        }
        public string BodyFromStringList
        {
            get
            {
                if (list.Count == 0)
                {
                    throw new CustomBasicException("Must have at least one item in order to get the body from the string list");
                }
                StrCat cats = new();
                foreach (var item in list)
                {
                    cats.AddToString(item, Constants.VBCrLf);
                }
                return cats.GetInfo();
            }
        }
    }
    [GeneratedRegex("<.*?>")]
    private static partial Regex MyRegex();
}