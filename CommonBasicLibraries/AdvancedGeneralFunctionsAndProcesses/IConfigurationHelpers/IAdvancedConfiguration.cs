namespace CommonBasicLibraries.AdvancedGeneralFunctionsAndProcesses.IConfigurationHelpers;
public interface IAdvancedConfiguration
{
    //only so i can have extensions for this.
    //i like the idea that the value can be null though.
    //Dictionary<string, string?> Data { get; set; }
    IAdvancedConfiguration Add(string key, string value);
}