namespace CommonBasicLibraries.AdvancedGeneralFunctionsAndProcesses.ApiServices;

public abstract class InternetApiClientService(HttpClient client) : ApiClientServiceCore(client)
{
    protected virtual bool UseHttps => true;
    protected abstract string Host { get; }     // api.foo.com
    protected virtual string BasePath => "";    // v1/

    protected override Uri BaseUri
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
            var root = $"{scheme}://{host}/";
            return string.IsNullOrWhiteSpace(BasePath)
                ? new Uri(root)
                : new Uri(new Uri(root), BasePath.TrimStart('/') + "/");
        }
    }
}
