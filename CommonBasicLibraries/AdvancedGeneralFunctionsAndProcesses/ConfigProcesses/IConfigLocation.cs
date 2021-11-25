namespace CommonBasicLibraries.AdvancedGeneralFunctionsAndProcesses.ConfigProcesses;
public interface IConfigLocation
{
    Task<string> GetConfigLocationAsync(); //decided to make it async.  so you can do await in the method if necessary to get the path for config.
}