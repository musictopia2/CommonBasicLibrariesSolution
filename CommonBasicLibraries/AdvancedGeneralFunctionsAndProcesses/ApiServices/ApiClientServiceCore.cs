namespace CommonBasicLibraries.AdvancedGeneralFunctionsAndProcesses.ApiServices;
public abstract class ApiClientServiceCore(HttpClient client)
{
    protected readonly HttpClient Client = client;

    protected abstract Uri BaseUri { get; }

    protected Uri Url(string route)
    {
        if (Uri.TryCreate(route, UriKind.Absolute, out _))
        {
            throw new CustomBasicException($"Route must be relative, not absolute: '{route}'.");
        }

        return new Uri(BaseUri, route.TrimStart('/'));
    }

    // helpers (GET/POST/etc) go here...

    //befor was SaveResultsAsync
    protected async Task GetEnsureSuccessAsync(string route, string errorMessage, CancellationToken ct = default)
    {
        using var resp = await Client.GetAsync(Url(route), ct);
        await EnsureSuccessAsync(resp, errorMessage, ct);
    }


    //before was GetDownloadDataAsync
    protected Task<string> GetStringAsync(string route, CancellationToken ct = default)
        => Client.GetStringAsync(Url(route), ct);





    protected async Task<TResponse> PostAsync<TRequest, TResponse>(
        string route, TRequest body, string errorMessage, CancellationToken ct = default)
    {
        using var resp = await Client.PostJsonAsync(Url(route), body, ct);
        await EnsureSuccessAsync(resp, errorMessage, ct);
        return await resp.GetJsonAsync<TResponse>();
    }
    //before was PostResultsAsync
    protected async Task PostAsync<TRequest>(string route, TRequest body, string errorMessage, CancellationToken ct = default)
    {
        using var resp = await Client.PostJsonAsync(Url(route), body, ct);
        await EnsureSuccessAsync(resp, errorMessage, ct);
    }
    //before was getresults.
    protected async Task<T> GetAsync<T>(string route, string errorMessage, CancellationToken ct = default)
    {
        using var resp = await Client.GetAsync(Url(route), ct);
        await EnsureSuccessAsync(resp, errorMessage, ct);
        return await resp.GetJsonAsync<T>();
    }

    protected async Task<TResponse> PutAsync<TRequest, TResponse>(string route, TRequest body, string errorMessage, CancellationToken ct = default)
    {
        using var resp = await Client.PutJsonAsync(Url(route), body, ct);
        await EnsureSuccessAsync(resp, errorMessage, ct);
        return await resp.GetJsonAsync<TResponse>();
    }

    private static async Task EnsureSuccessAsync(HttpResponseMessage resp, string errorMessage, CancellationToken ct)
    {
        if (resp.IsSuccessStatusCode)
        {
            return;
        }

        var text = await resp.Content.ReadAsStringAsync(ct);
        throw new CustomBasicException($"{errorMessage}. Status: {resp.StatusCode}. {text}");
    }

    protected async Task<byte[]> GetBytesAsync(string route, string errorMessage, CancellationToken ct = default)
    {
        using var resp = await Client.GetAsync(Url(route), ct);
        await EnsureSuccessAsync(resp, errorMessage, ct);
        return await resp.Content.ReadAsByteArrayAsync(ct);
    }
}