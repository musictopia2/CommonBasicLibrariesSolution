namespace CommonBasicLibraries.AdvancedGeneralFunctionsAndProcesses.IConfigurationHelpers;
public class AdvancedConfiguration : IAdvancedConfiguration
{
    internal static Dictionary<string, string?> Data { get; set; } = [];
    public IAdvancedConfiguration Add(string key, string value)
    {
        //if the key is already there, not sure what will happen though.
        Data.Add(key, value);
        return this;
        //throw new NotImplementedException();
    }   
}