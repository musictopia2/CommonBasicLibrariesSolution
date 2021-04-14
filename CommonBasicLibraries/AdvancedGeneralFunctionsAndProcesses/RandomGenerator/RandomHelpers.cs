namespace CommonBasicLibraries.AdvancedGeneralFunctionsAndProcesses.RandomGenerator
{
    public static class RandomHelpers
    {
        //public static IRandomGenerator Randoms { get; }

        private static IRandomGenerator? _rs;
        private static IRandomData? _data;
        public static void SetUpRandom(IRandomGenerator random)
        {
            _rs = random;
        }

        public static void SetUpData(IRandomData data)
        {
            _data = data; //it should be okay to be static.
        }

        public static IRandomGenerator GetRandomGenerator()
        {
            if (_data == null)
            {
                //has to set up the data now too.  if this is not provided, then default implementation will be provided.
                _data = new BasicRandomDataClass();
            }
            if (_rs == null)
            {
                _rs = new RandomGenerator();
            }
            return _rs;
        }
        internal static IRandomData GetRandomDataClass => _data!;
    }
}