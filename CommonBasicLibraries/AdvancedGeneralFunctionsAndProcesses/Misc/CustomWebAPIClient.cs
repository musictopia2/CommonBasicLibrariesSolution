namespace CommonBasicLibraries.AdvancedGeneralFunctionsAndProcesses.Misc;
//for now i am unable to have a simplewebpage class.  if needed, rethinking is required.  since i am using something declared as obsolete.
public abstract class CustomWebAPIClient
{
    //this is intended to have more simple processes for creating web api clients.
    /// <summary>
    /// this is for the path of the base part of the service.  examples include
    /// hotelservice/api/
    /// </summary>
    protected abstract string ServicePath { get; }
    protected Uri? BaseAddress;
    /// <summary>
    /// This is the key to get out to figure out the base url.
    /// </summary>
    protected abstract string Key { get; }
    protected HttpClient Client;
    public CustomWebAPIClient(IConfiguration config, HttpClient client)
    {
        Client = client;
        //Microsoft.Extensions.Configuration.ConfigurationBinder
        string firstPart = config.GetValue<string>(Key) ?? throw new CustomBasicException("Failed to get key from appsettings.  Use appsettings instead of ISimpleConfig");
        Uri secondPart = new(firstPart);
        BaseAddress = new(secondPart, ServicePath);
    }
    protected async Task SaveResults(string extras, string errorMessage)
    {
        Uri finalAddress = new(BaseAddress!, extras);
        var results = await Client.GetAsync(finalAddress);
        if (results.IsSuccessStatusCode == false)
        {
            throw new CustomBasicException(errorMessage);
        }
    }
    protected async Task<T> GetResults<T>(string extras, string errorMessage)
    {
        Uri finalAddress = new(BaseAddress!, extras);
        return await Client.GetJsonAsync<T>(finalAddress, errorMessage);
    }
}