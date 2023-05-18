namespace CommonBasicLibraries.AdvancedGeneralFunctionsAndProcesses.Misc;
public abstract class LocalHostedApiService
{
    protected abstract string Name { get; } //i like the idea of naming.  so several apis can be hosted with same project (more organized)
    protected Uri ServiceAddress;
    protected HttpClient Client;
    public LocalHostedApiService(HttpClient client)
    {
        Client = client;
        ServiceAddress = GetBaseAddress;
    }
    private Uri GetBaseAddress => Client.BaseAddress!;
    protected Uri GetFullAddress(string extras)
    {
        string content = ServiceAddress.ToString();
        content = Path.Combine(content, Name, extras);
        return new(content);
    }
    protected async Task SaveResultsAsync(string extras, string errorMessage)
    {
        Uri finalAddress = GetFullAddress(extras);
        var results = await Client.GetAsync(finalAddress);
        if (results.IsSuccessStatusCode == false)
        {
            throw new CustomBasicException(errorMessage);
        }
    }
    protected async Task<T> GetResultsAsync<T>(string extras, string errorMessage)
    {
        Uri finalAddress = GetFullAddress(extras);
        return await Client.GetJsonAsync<T>(finalAddress, errorMessage);
    }
    protected async Task<string> GetDownloadDataAsync(string extras)
    {
        Uri finalAddress = GetFullAddress(extras);
        string output = await Client.GetStringAsync(finalAddress);
        return output;
    }
    protected async Task PostResultsAsync<T>(string extras, T data, string errorMessage)
    {
        Uri finalAddress = GetFullAddress(extras);
        var response = await Client.PostJsonAsync<T>(finalAddress, data);
        if (response.IsSuccessStatusCode == false)
        {
            throw new CustomBasicException(errorMessage);
        }
    }
}