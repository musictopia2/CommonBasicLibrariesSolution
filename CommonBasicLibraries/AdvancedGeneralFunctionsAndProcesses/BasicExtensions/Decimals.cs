namespace CommonBasicLibraries.AdvancedGeneralFunctionsAndProcesses.BasicExtensions;
public static class Decimals
{
    
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