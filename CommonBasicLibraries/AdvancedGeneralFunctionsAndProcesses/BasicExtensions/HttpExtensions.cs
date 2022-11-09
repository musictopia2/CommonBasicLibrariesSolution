namespace CommonBasicLibraries.AdvancedGeneralFunctionsAndProcesses.BasicExtensions;
public static class HttpExtensions
{
    public static async Task SaveDownloadFileAsync(this HttpClient client, string requesturi, string path)
    {
        HttpResponseMessage output = await client.GetAsync(requesturi);
        if (output.IsSuccessStatusCode == false)
        {
            throw new CustomBasicException($"Was not okay.  The code returned was {output.StatusCode}");
        }
        using FileStream stream = new(path, FileMode.Create, FileAccess.ReadWrite, FileShare.None);
        var results = await output.Content.ReadAsByteArrayAsync();
#if NET6_0_OR_GREATER
        await stream.WriteAsync(results);
#endif
#if NETSTANDARD2_0
await stream.WriteAsync(results, 0, results.Length);
#endif
    }
    private static async Task<StringContent> GetContentAsync<T>(this T value) //hopefully still this simple.
    {
        string temps = await js1.SerializeObjectAsync(value!);
        StringContent output = new(temps, Encoding.UTF8, "application/json");
        return output;
    }
    public static async Task<HttpResponseMessage> PostJsonAsync<T>(this HttpClient client, string uri, T value)
    {
        StringContent content = await GetContentAsync(value);
        return await client.PostAsync(uri, content);
    }
    public static async Task<HttpResponseMessage> PostJsonAsync<T>(this HttpClient client, Uri uri, T value) //so you have a choice.
    {
        StringContent content = await GetContentAsync(value);
        return await client.PostAsync(uri, content);
    }
    public static async Task<HttpResponseMessage> PutJsonAsync<T>(this HttpClient client, string uri, T value)
    {
        StringContent content = await GetContentAsync(value);
        return await client.PutAsync(uri, content);
    }

    //hopefully i don't need the putcustomjsonasync anymore (?)

    ///// <summary>
    ///// use the custom method if this contains a custom list and using put command.
    ///// </summary>
    ///// <typeparam name="T"></typeparam>
    ///// <param name="client"></param>
    ///// <param name="uri"></param>
    ///// <param name="value"></param>
    ///// <returns></returns>
    //public static async Task<HttpResponseMessage> PutCustomJsonAsync<T>(this HttpClient client, string uri, T value)
    //{
    //    string thisStr = await js.SerializeObjectAsync(value!);
    //    StringContent content = new(thisStr); //this is exception.
    //    return await client.PutAsync(uri, content);
    //}

    private static async Task<T> GetJsonAsync<T>(this HttpResponseMessage response, string errorMessage = "Failed to get async json data.  Rethink")
    {
        if (response.IsSuccessStatusCode == false)
        {
            throw new CustomBasicException(errorMessage);
        }
        string res = await response.Content.ReadAsStringAsync();
        response.Dispose();
        try
        {
            return await js1.DeserializeObjectAsync<T>(res);
        }
        catch (Exception ex)
        {
            throw new CustomBasicException($"Failed to get the json.  Error was {ex.Message}");
        }
    }
    public static async Task<T> GetJsonAsync<T>(this HttpClient client, Uri uri, string errorMessage = "Failed to get async json data.  Rethink")
    {
        HttpResponseMessage response = await client.GetAsync(uri);
        return await response.GetJsonAsync<T>(errorMessage);
    }
    public static async Task<T> GetJsonAsync<T>(this HttpClient client, string uri, string errorMessage = "Failed to get async json data.  Rethink") //looks like delete is no problem.  not sure what patch is about anyways.
    {

        HttpResponseMessage response = await client.GetAsync(uri);
        return await response.GetJsonAsync<T>(errorMessage);
    }
}