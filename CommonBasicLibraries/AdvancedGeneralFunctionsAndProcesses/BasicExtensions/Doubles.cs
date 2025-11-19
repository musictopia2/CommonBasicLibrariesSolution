namespace CommonBasicLibraries.AdvancedGeneralFunctionsAndProcesses.BasicExtensions;
public static class Doubles
{
    extension(double original)
    {
        public int RoundToHigherNumber
        {
            get
            {
                int truncated = (int)original;// drops fractional part
                return original > truncated ? truncated + 1 : truncated;
            }
        }

        public int RoundToLowerNumber
        {
            get
            {
                return (int)original;// simply truncates fractional part
            }
        }
        public double MultiplyAndAdd(double amount)
        {
            double subs = original * amount;
            return subs + original;
        }
        public int Multiply(int howMuch)
        {
            return (int)Math.Ceiling(original * howMuch);
        }
    }
}