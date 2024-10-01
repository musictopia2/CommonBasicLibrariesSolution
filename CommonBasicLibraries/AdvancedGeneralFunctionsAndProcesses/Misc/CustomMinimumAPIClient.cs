namespace CommonBasicLibraries.AdvancedGeneralFunctionsAndProcesses.Misc;
public abstract class CustomMinimumAPIClient(IConfiguration config, HttpClient client, string key) : CustomWebAPIClient(config, client, key)
{
    protected sealed override string ServicePath => "";
}