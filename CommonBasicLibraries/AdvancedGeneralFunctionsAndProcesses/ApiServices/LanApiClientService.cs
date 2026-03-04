namespace CommonBasicLibraries.AdvancedGeneralFunctionsAndProcesses.ApiServices;
public abstract class LanApiClientService(HttpClient client) : ApiClientServiceCore(client)
{
    protected abstract int Port { get; }
    protected virtual string BasePath => "";
    protected virtual bool UseHttps => false; // default off for LAN
    protected abstract string Host { get; }
    protected override Uri BaseUri
    {
        get
        {
            var scheme = UseHttps ? "https" : "http";
            var root = $"{scheme}://{Host}:{Port}/";
            return string.IsNullOrWhiteSpace(BasePath)
                ? new Uri(root)
                : new Uri(new Uri(root), BasePath.TrimStart('/') + "/");
        }
    }
}