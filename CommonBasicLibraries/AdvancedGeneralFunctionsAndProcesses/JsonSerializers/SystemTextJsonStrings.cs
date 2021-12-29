using tt = System.Text.Json.JsonSerializer;
namespace CommonBasicLibraries.AdvancedGeneralFunctionsAndProcesses.JsonSerializers;
public static class SystemTextJsonStrings
{
    public static JsonSerializerOptions GetOptionsForAspnetCore()
    {
        var options = new JsonSerializerOptions();
        options.AddDateTimeConvertersAndIndent();
        MultipleContextHelpers.PopulateConverters(options);
        return options;
    }
    //decided to make it public.  this means if i am doing from client and want the proper options (including using custom source generators, can do).  in this case, has to know the type to use.
    public static JsonSerializerOptions GetCustomJsonSerializerOptions<T>()
    {
        var options = new JsonSerializerOptions();
        options.AddDateTimeConvertersAndIndent();
        MultipleContextHelpers.PopulateOptions<T>(options);
        return options;
    }
    public static async Task<string> SerializeObjectAsync<T>(T thisObj)
    {
        string thisStr = "";
        await Task.Run(() =>
        {
            JsonSerializerOptions options = GetCustomJsonSerializerOptions<T>();
            thisStr = tt.Serialize(thisObj, options);
        });
        return thisStr;
    }
    public static async Task<T> DeserializeObjectAsync<T>(string thisStr)
    {
        T thisT = default!;
        await Task.Run(() =>
        {
            JsonSerializerOptions options = GetCustomJsonSerializerOptions<T>();
            thisT = tt.Deserialize<T>(thisStr, options)!;
        });
        return thisT!;
    }
    public static D ConvertObject<D, S>(S thisObj)
    {
        string thisStr = SerializeObject<S>(thisObj!);
        return DeserializeObject<D>(thisStr);
    }
    public static string SerializeObject<T>(T thisObj)
    {
        JsonSerializerOptions options = GetCustomJsonSerializerOptions<T>();
        return tt.Serialize(thisObj, options)!;
    }
    public static T DeserializeObject<T>(string thisStr)
    {
        JsonSerializerOptions options = GetCustomJsonSerializerOptions<T>();
        return tt.Deserialize<T>(thisStr, options)!;
    }
    public static async Task<T> ConvertObjectAsync<T>(T thisObj)
    {
        string thisStr = await SerializeObjectAsync(thisObj);
        return await DeserializeObjectAsync<T>(thisStr);
    }
}