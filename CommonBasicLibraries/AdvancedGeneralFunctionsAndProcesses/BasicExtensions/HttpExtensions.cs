namespace CommonBasicLibraries.AdvancedGeneralFunctionsAndProcesses.BasicExtensions;
public static class HttpExtensions
{
    extension (HttpClient client)
    {
        public async Task SaveDownloadFileAsync(string requesturi, string path)
        {
            HttpResponseMessage output = await client.GetAsync(requesturi);
            if (output.IsSuccessStatusCode == false)
            {
                throw new CustomBasicException($"Was not okay.  The code returned was {output.StatusCode}");
            }
            using FileStream stream = new(path, FileMode.Create, FileAccess.ReadWrite, FileShare.None);
            var results = await output.Content.ReadAsByteArrayAsync();
            await stream.WriteAsync(results);
        }
    }
    extension<T>(HttpClient client)
    {
        public async Task<HttpResponseMessage> PostJsonAsync(string uri, T value)
        {
            StringContent content = await value.GetContentAsync();
            return await client.PostAsync(uri, content);
        }
        public async Task<HttpResponseMessage> PostJsonAsync(Uri uri, T value)
        {
            StringContent content = await value.GetContentAsync();
            return await client.PostAsync(uri, content);
        }
        public async Task<HttpResponseMessage> PutJsonAsync(string uri, T value)
        {
            StringContent content = await value.GetContentAsync();
            return await client.PutAsync(uri, content);
        }
        public async Task<HttpResponseMessage> PutJsonAsync(Uri uri, T value)
        {
            StringContent content = await value.GetContentAsync();
            return await client.PutAsync(uri, content);
        }
        public async Task<HttpResponseMessage> PatchJsonAsync(string uri, T value)
        {
            StringContent content = await value.GetContentAsync();
            return await client.PatchAsync(uri, content);
        }
        public async Task<HttpResponseMessage> PatchJsonAsync(Uri uri, T value)
        {
            StringContent content = await value.GetContentAsync();
            return await client.PatchAsync(uri, content);
        }
        public async Task<T> GetJsonAsync(Uri uri, string errorMessage = "Failed to get async json data.  Rethink")
        {
            HttpResponseMessage response = await client.GetAsync(uri);
            return await response.GetJsonAsync<T>(errorMessage);
        }
        public async Task<T> GetJsonAsync(string uri, string errorMessage = "Failed to get async json data.  Rethink")
        {
            HttpResponseMessage response = await client.GetAsync(uri);
            return await response.GetJsonAsync<T>(errorMessage);
        }
    }
    extension<T>(T value)
    {
        internal async Task<StringContent> GetContentAsync() //kept it internal for now to remove the errors where it thinks its not referenced but is referenced.
        {
            string temps = await js1.SerializeObjectAsync(value!);
            StringContent output = new(temps, Encoding.UTF8, "application/json");
            return output;
        }
    }
    extension<T>(HttpResponseMessage response)
    {
        public async Task<T> GetJsonAsync()
        {
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
        public async Task<T> GetJsonAsync(string errorMessage = "Failed to get async json data.  Rethink")
        {
            if (response.IsSuccessStatusCode == false)
            {
                throw new CustomBasicException(errorMessage);
            }
            return await response.GetJsonAsync<T>();
        }
    }
}