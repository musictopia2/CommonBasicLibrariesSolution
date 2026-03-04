namespace CommonBasicLibraries.AdvancedGeneralFunctionsAndProcesses.ApiServices;
public abstract class ApiClientServiceCore(HttpClient client)
{
    protected readonly HttpClient Client = client;

    protected abstract Uri BaseUri { get; }

    protected Uri Url(string route) => new(BaseUri, route.TrimStart('/'));

    // helpers (GET/POST/etc) go here...


    protected Task GetEnsureSuccessAsync(string route, string errorMessage)
     => EnsureSuccessAsync(() => Client.GetAsync(Url(route)), errorMessage);



    protected Task<string> GetStringAsync(string route)
        => Client.GetStringAsync(Url(route));



    private static async Task EnsureSuccessAsync(Func<Task<HttpResponseMessage>> call, string errorMessage)
    {
        using var response = await call();
        if (!response.IsSuccessStatusCode)
        {
            throw new CustomBasicException(errorMessage);
        }
    }


    protected async Task<TResponse> PostAsync<TRequest, TResponse>(string route, TRequest body, string errorMessage)
    {
        using var resp = await Client.PostJsonAsync(Url(route), body);
        return await resp.GetJsonAsync<TResponse>(errorMessage);
    }
    protected async Task PostAsync<TRequest>(string route, TRequest body, string errorMessage)
    {
        using var resp = await Client.PostJsonAsync(Url(route), body);
        if (!resp.IsSuccessStatusCode)
        {
            throw new CustomBasicException(errorMessage);
        }
    }
    //before was getresults.
    protected Task<T> GetAsync<T>(string route, string errorMessage)
    {
        // ct not supported by your existing extension; that's okay for now (or add overloads later).
        return Client.GetJsonAsync<T>(Url(route), errorMessage);
    }

    protected async Task<TResponse> PutAsync<TRequest, TResponse>(string route, TRequest body, string errorMessage)
    {
        using var resp = await Client.PutJsonAsync(Url(route), body);
        if (!resp.IsSuccessStatusCode)
        {
            throw new CustomBasicException(errorMessage);
        }
        return await resp.GetJsonAsync<TResponse>(errorMessage);
    }

    


}