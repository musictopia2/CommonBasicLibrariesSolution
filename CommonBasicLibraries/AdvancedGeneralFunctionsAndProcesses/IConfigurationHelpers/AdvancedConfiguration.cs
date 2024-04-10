namespace CommonBasicLibraries.AdvancedGeneralFunctionsAndProcesses.IConfigurationHelpers;
public class AdvancedConfiguration : IAdvancedConfiguration
{
    internal static Dictionary<string, string?> Data { get; set; } = [];
    public IAdvancedConfiguration Add(string key, string value)
    {
        Data.Add(key, value);
        return this;
    }   
}