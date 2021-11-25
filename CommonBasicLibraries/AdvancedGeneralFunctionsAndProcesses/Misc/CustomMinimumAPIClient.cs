namespace CommonBasicLibraries.AdvancedGeneralFunctionsAndProcesses.Misc;
public abstract class CustomMinimumAPIClient : CustomWebAPIClient
{
    protected CustomMinimumAPIClient(ISimpleConfig sims, HttpClient client) : base(sims, client)
    {
    }
    protected sealed override string ServicePath => "";
}