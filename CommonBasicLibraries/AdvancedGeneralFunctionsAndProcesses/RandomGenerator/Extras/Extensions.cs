namespace CommonBasicLibraries.AdvancedGeneralFunctionsAndProcesses.RandomGenerator;
public static class Extensions
{
    extension(string payLoad)
    {
        public string Capitalize
        => string.Concat(payLoad.ToString().ToUpperInvariant(), payLoad.AsSpan(1));
    }
}