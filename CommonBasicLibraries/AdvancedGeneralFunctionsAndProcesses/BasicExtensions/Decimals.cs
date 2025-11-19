using CommonBasicLibraries.AdvancedGeneralFunctionsAndProcesses.BsonHelpers;

namespace CommonBasicLibraries.AdvancedGeneralFunctionsAndProcesses.BasicExtensions;
public static class Decimals
{ 
    extension(decimal amount)
    {
        public bool IsPaidInFull(decimal totalDue)
        {
            decimal remainingToPay = totalDue - amount;
            if (remainingToPay < 0.01M)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public decimal RoundPrice(decimal cents)
        {
            if (cents >= 1)
            {
                throw new CustomBasicException("Must be less than 1 dollar");
            }
            if (cents < 0)
            {
                throw new CustomBasicException("Must be at least 0");
            }
            var newPrice = amount - 1;
            var thisStr = newPrice.ToString("##0.00");
            int newSearch;
            if (thisStr.Contains('.') == false)
            {
                newSearch = -1;
            }
            else
            {
                newSearch = thisStr.IndexOf('.');
            }
            decimal centss;
            decimal diffs;
            if (newSearch == -1)
            {
                diffs = cents * 100;
            }
            else
            {
                centss = decimal.Parse(thisStr.Substring(newSearch)); // try this
                diffs = (cents * 100) - centss;
            }
            diffs *= 0.01m;
            return newPrice + diffs;
        }
        private int HowMany100s(decimal maxValue, decimal howMuchFree)
        {
            int x;
            int y;
            y = 0;
            if (amount > maxValue)
            {
                throw new CustomBasicException("The max value is " + maxValue + ".  However; the declared value is " + amount);
            }
            var loopTo = maxValue;
            for (x = (int)howMuchFree; x <= loopTo; x += 100)
            {
                y += 1;
                if (x >= amount)
                {
                    return y - 1;
                }
            }
            return y;
        }
        public decimal ChargeBy100s(decimal perPoundCharge, decimal maxValue, decimal howMuchFree = 0)
        {
            if (amount <= howMuchFree)
            {
                return 0;
            }
            var tempHowMuch = amount.HowMany100s(maxValue, howMuchFree);
            if (tempHowMuch <= 0)
            {
                throw new CustomBasicException("Cannot multiply by 0 or less in order to calculate the charge per 100");
            }
            return tempHowMuch * perPoundCharge;
        }
        public string ToCurrency(int decimalPlaces = 2, bool useDollarSign = true)
        {
            string output = amount.ToString("c" + decimalPlaces);
            if (useDollarSign)
            {
                return output;
            }
            return output.Substring(1);
        }
        //was previously ConvertToDecimalWords  this means breaking changes.
        public string DecimalWords
        {
            get
            {
                // Split the decimal into integer and fractional parts
                int integerPart = (int)amount;
                int fractionalPart = (int)((amount - integerPart) * 100); // Get the cents as an integer
                                                                         // Convert the integer part to words
                string integerWords = integerPart.ConvertToIntegerWords;
                if (fractionalPart == 0)
                {
                    return integerWords; //that always worked so no problem there.
                }
                string fractionalWords = $"and {fractionalPart}/100";
                return $"{integerWords} {fractionalWords}";
            }
        }  
    }
}