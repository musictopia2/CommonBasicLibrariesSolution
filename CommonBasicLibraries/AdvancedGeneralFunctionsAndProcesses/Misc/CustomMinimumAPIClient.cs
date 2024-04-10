namespace CommonBasicLibraries.AdvancedGeneralFunctionsAndProcesses.Misc;
public abstract class CustomMinimumAPIClient(IConfiguration config, HttpClient client) : CustomWebAPIClient(config, client)
{
    protected sealed override string ServicePath => "";
}