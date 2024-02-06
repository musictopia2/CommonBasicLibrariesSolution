namespace CommonBasicLibraries.AdvancedGeneralFunctionsAndProcesses.Misc;
public abstract class CustomMinimumAPIClient : CustomWebAPIClient
{
    protected CustomMinimumAPIClient(IConfiguration config, HttpClient client) : base(config, client)
    {
    }
    protected sealed override string ServicePath => "";
}