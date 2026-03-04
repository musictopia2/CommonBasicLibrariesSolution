namespace CommonBasicLibraries.AdvancedGeneralFunctionsAndProcesses.ApiServices;
public abstract class LocalhostApiClient(HttpClient client) : LanApiClientService(client)
{
    protected sealed override string Host => "localhost";

    // default for localhost
    protected override bool UseHttps => true;
}