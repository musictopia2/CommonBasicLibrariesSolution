namespace CommonBasicLibraries.AdvancedGeneralFunctionsAndProcesses.Misc;
public interface IWebAPIKey
{
    abstract static string Key { get; } //decided to do as abstract interface.  this will mean that will be easier to set keys for IConfiguration (do in memory for example).
    //this means that in order to inherit from classes, has to implement this interface.  so it can work via IConfiguration.
}