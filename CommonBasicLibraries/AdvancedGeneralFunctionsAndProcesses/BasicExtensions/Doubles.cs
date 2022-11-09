namespace CommonBasicLibraries.AdvancedGeneralFunctionsAndProcesses.BasicExtensions;
public static class Doubles
{
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