namespace CommonBasicLibraries.AdvancedGeneralFunctionsAndProcesses.RandomGenerator;
public static class RandomHelpers
{
    private static IRandomNumberList? _rs;
    private static IRandomData? _data;
    public static void SetUpRandom(IRandomNumberList random)
    {
        _rs = random;
    }
    public static void SetUpData(IRandomData data)
    {
        _data = data; //it should be okay to be static.
    }
    public static IRandomNumberList GetRandomGenerator()
    {
        //has to set up the data now too.  if this is not provided, then default implementation will be provided.
        _data ??= new BasicRandomDataClass();
        _rs ??= new RandomGenerator();
        return _rs;
    }
    internal static IRandomData GetRandomDataClass => _data!;
}