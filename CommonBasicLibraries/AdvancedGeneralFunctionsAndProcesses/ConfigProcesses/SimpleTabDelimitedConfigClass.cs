using static CommonBasicLibraries.AdvancedGeneralFunctionsAndProcesses.FileFunctions.FileFunctions;
namespace CommonBasicLibraries.AdvancedGeneralFunctionsAndProcesses.ConfigProcesses
{
    public class SimpleTabDelimitedConfigClass : ISimpleConfig
    {
        private readonly IConfigLocation _locator;
        public SimpleTabDelimitedConfigClass(IConfigLocation locator)
        {
            _locator = locator;
        }
        async Task<string> ISimpleConfig.GetStringAsync(string key)
        {
            string path = await _locator.GetConfigLocationAsync(); //this is intended to get from local disk;
            if (FileExists(path) == false)
            {
                throw new CustomBasicException($"Path at {path} does not exist.");
            }
            if (path.ToLower().EndsWith("txt") == false)
            {
                throw new CustomBasicException(@"Only text files are supported.  Rethink");
            }
            BasicList<string> firstList = await ReadAllLinesAsync(path);
            Dictionary<string, string> output = new();
            firstList.ForEach(row =>
            {
                BasicList<string> nextList = row.Split(Constants.VBTab).ToBasicList();
                if (nextList.Count != 2)
                {
                    throw new CustomBasicException($"Needs 2 items for value pair.  Value or row was {row}");
                }
                bool rets = output.TryAdd(nextList.First(), nextList.Last());
                if (rets == false)
                {
                    throw new CustomBasicException($"{key} was duplicated");
                }
            });
            return output[key];
        }
    }
}