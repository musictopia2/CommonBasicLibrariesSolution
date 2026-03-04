namespace CommonBasicLibraries.AdvancedGeneralFunctionsAndProcesses.ApiServices;
public abstract class LanApiClientService(HttpClient client) : ApiClientServiceCore(client)
{
    protected abstract int Port { get; }
    protected virtual string BasePath => "";
    protected virtual bool UseHttps => false; // default off for LAN
    protected abstract string Host { get; }
    protected sealed override Uri BaseUri
    {
        get
        {
            var scheme = UseHttps ? "https" : "http";
            var host = Host.Trim();
            if (host.StartsWith("http://", StringComparison.OrdinalIgnoreCase) ||
                host.StartsWith("https://", StringComparison.OrdinalIgnoreCase))
            {
                throw new CustomBasicException($"Host must not include scheme. Host was '{Host}'.");
            }
            var root = $"{scheme}://{host}:{Port}/";
            return string.IsNullOrWhiteSpace(BasePath)
                ? new Uri(root)
                : new Uri(new Uri(root), BasePath.TrimStart('/') + "/");
        }
    }
}