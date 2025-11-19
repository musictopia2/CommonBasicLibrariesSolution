using System.Numerics;
namespace CommonBasicLibraries.AdvancedGeneralFunctionsAndProcesses.BasicExtensions;
public static class NumberExtensions
{

    extension<T>(T original)
        where T: INumber<T>
    {
        public (int Pounds, int Ounces) PoundsOunces
        {
            get
            {
                var thisItem = original.ToString();
                if (thisItem!.Contains('.') == false)
                {
                    return (int.Parse(thisItem), 0);
                }
                int firsts;
                firsts = int.Parse(thisItem.PartialString(".", true));
                decimal seconds;
                seconds = decimal.Parse("." + thisItem.PartialString(".", false));
                var sec_fin = seconds * 16;
                return (firsts, (int)sec_fin);
            }
        }
       
    }
    extension<T>(T amount)
        where T: IFloatingPoint<T>
    {
        public BasicList<decimal> SplitMoney(int howManyToSplitBy)
        {
            decimal tempAmount;
            tempAmount = decimal.Parse(amount.ToString()!);
            decimal splits = tempAmount / howManyToSplitBy;
            splits = decimal.Round(splits, 2);
            BasicList<decimal> thisList = [];
            decimal totalUsed;
            totalUsed = 0;
            var loopTo = howManyToSplitBy;
            for (var x = 1; x <= loopTo; x++)
            {
                totalUsed += splits;
                thisList.Add(splits);
            }
            if (amount.Equals(totalUsed))
            {
                return thisList;
            }
            decimal Lefts;
            decimal diffs;
            diffs = tempAmount - totalUsed;
            Lefts = Math.Abs(diffs);
            Lefts *= 100;
            decimal addAmount;
            if (diffs < 0)
            {
                addAmount = -0.01m;
            }
            else
            {
                addAmount = 0.01m;
            }
            var loopTo1 = Lefts;
            for (var x = 1; x <= loopTo1; x++)
            {
                thisList[x - 1] += addAmount;
            }
            return thisList;
        }
    }
}