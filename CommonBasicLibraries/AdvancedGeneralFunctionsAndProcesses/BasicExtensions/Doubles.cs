namespace CommonBasicLibraries.AdvancedGeneralFunctionsAndProcesses.BasicExtensions
{
    public static class Doubles
    {
        public static BasicList<decimal> SplitMoney(this double amount, int howManyToSplitBy) //decided to use the new custom list for this now.
        {
            decimal tempAmount = decimal.Parse(amount.ToString());
            decimal splits = tempAmount / howManyToSplitBy;
            splits = decimal.Round(splits, 2);
            BasicList<decimal> list = new();
            decimal totalUsed;
            totalUsed = 0;
            var loopTo = howManyToSplitBy;
            for (var x = 1; x <= loopTo; x++)
            {
                totalUsed += splits;
                list.Add(splits);
            }
            if (totalUsed == tempAmount)
            {
                return list;
            }
            decimal lefts;
            decimal diffs;
            diffs = tempAmount - totalUsed;
            lefts = Math.Abs(diffs);
            lefts *= 100;
            decimal addAmount;
            if (diffs < 0)
            {
                addAmount = -0.01m;
            }
            else
            {
                addAmount = 0.01m;
            }
            var loopTo1 = lefts;
            for (var x = 1; x <= loopTo1; x++)
            {
                list[x - 1] += addAmount;
            }
            return list;
        }
        public static int RoundToHigherNumber(this double thisDou)
        {
            string str = thisDou.ToString();
            if (str.Contains(".") == false)
            {
                return int.Parse(thisDou.ToString());
            }
            BasicList<string> list = str.Split(".").ToBasicList();
            int value = int.Parse(list.First());
            return value + 1;
        }
        public static int RoundToLowerNumber(this double thisDou)
        {
            string thisStr = thisDou.ToString();
            if (thisStr.Contains(".") == false)
            {
                return int.Parse(thisDou.ToString());
            }
            BasicList<string> thisList = thisStr.Split(".").ToBasicList();
            return int.Parse(thisList.First());
        }
        public static double MultiplyAndAdd(this double original, double amount)
        {
            double subs = original * amount;
            return subs + original;
        }
        public static int Multiply(this double thisAmount, int howMuch)
        {
            return (int)Math.Ceiling(thisAmount * howMuch);
        }
    }
}