namespace CommonBasicLibraries.AdvancedGeneralFunctionsAndProcesses.BasicExtensions;
public static class Decimals
{
    public static BasicList<decimal> SplitMoney(this decimal amount, int howManyToSplitBy)
    {
        decimal splits = amount / howManyToSplitBy;
        splits = decimal.Round(splits, 2);
        BasicList<decimal> thisList = new();
        decimal totalUsed;
        totalUsed = 0;
        var loopTo = howManyToSplitBy;
        for (var x = 1; x <= loopTo; x++)
        {
            totalUsed += splits;
            thisList.Add(splits);
        }
        if (totalUsed == amount)
        {
            return thisList;
        }
        decimal Lefts;
        decimal diffs;
        diffs = amount - totalUsed;
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
    public static bool IsPaidInFull(this decimal amountPaid, decimal totalDue)
    {
        decimal remainingToPay = totalDue - amountPaid;
        if (remainingToPay < 0.01M)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public static decimal RoundPrice(this decimal thisPrice, decimal centAmount)
    {
        if (centAmount >= 1)
        {
            throw new CustomBasicException("Must be less than 1 dollar");
        }
        if (centAmount < 0)
        {
            throw new CustomBasicException("Must be at least 0");
        }
        var newPrice = thisPrice - 1;
        var thisStr = newPrice.ToString("##0.00");
        int newSearch;
        if (thisStr.Contains(".") == false)
        {
            newSearch = -1;
        }
        else
        {
            newSearch = thisStr.IndexOf(".");
        }
        decimal centss;
        decimal diffs;
        if (newSearch == -1)
        {
            diffs = centAmount * 100;
        }
        else
        {
            centss = decimal.Parse(thisStr.Substring(newSearch)); // try this
            diffs = (centAmount * 100) - centss;
        }
        diffs *= 0.01m;
        return newPrice + diffs;
    }
    private static int HowMany100s(this decimal declaredValue, decimal maxValue, decimal howMuchFree = 0)
    {
        int x;
        int y;
        y = 0;
        if (declaredValue > maxValue)
        {
            throw new CustomBasicException("The max value is " + maxValue + ".  However; the declared value is " + declaredValue);
        }
        var loopTo = maxValue;
        for (x = (int)howMuchFree; x <= loopTo; x += 100)
        {
            y += 1;
            if (x >= declaredValue)
            {
                return y - 1;
            }
        }
        return y;
    }
    public static (int Pounds, int Ounces) PoundsOunces(float grossWeight)
    {
        var thisItem = grossWeight.ToString();
        if (thisItem.Contains(".") == true)
        {
            return ((int)grossWeight, 0);
        }
        int firsts;
        firsts = int.Parse(thisItem.PartialString(".", true));
        decimal seconds;
        seconds = decimal.Parse("." + thisItem.PartialString(".", false));
        int secFin;
        secFin = (int)seconds * 16;
        return (firsts, secFin);
    }
    public static decimal ChargeBy100s(this decimal declaredValue, decimal perPoundCharge, decimal maxValue, decimal howMuchFree = 0)
    {
        if (declaredValue <= howMuchFree)
        {
            return 0;
        }
        var tempHowMuch = declaredValue.HowMany100s(maxValue, howMuchFree);
        if (tempHowMuch <= 0)
        {
            throw new CustomBasicException("Cannot multiply by 0 or less in order to calculate the charge per 100");
        }
        return tempHowMuch * perPoundCharge;
    }
    public static string ToCurrency(this decimal thisDec, int decimalPlaces = 2, bool useDollarSign = true)
    {
        string output = thisDec.ToString("c" + decimalPlaces);
        if (useDollarSign)
        {
            return output;
        }
        return output.Substring(1);
    }
}