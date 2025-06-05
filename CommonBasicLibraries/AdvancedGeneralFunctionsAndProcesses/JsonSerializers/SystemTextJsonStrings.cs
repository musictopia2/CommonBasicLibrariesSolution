using tt1 = System.Text.Json.JsonSerializer;
namespace CommonBasicLibraries.AdvancedGeneralFunctionsAndProcesses.JsonSerializers;
public static class SystemTextJsonStrings
{
    public static bool RequireCustomSerialization { get; set; } //if you set to true (like for a game package), then if not set, then will raise exception.
    public static JsonSerializerOptions GetOptionsForAspnetCore()
    {
        var options = new JsonSerializerOptions();
        options.AddDateTimeConvertersAndIndent();
        ConvertersHelpers.PopulateConverters(options);
        return options;
    }
    //decided to make it public.  this means if i am doing from client and want the proper options (including using custom source generators, can do).  in this case, has to know the type to use.
    public static JsonSerializerOptions GetCustomJsonSerializerOptions<T>()
    {
        if (JsonOptionsHelpers<T>.Options is not null)
        {
            return JsonOptionsHelpers<T>.Options;
        }
        var options = new JsonSerializerOptions();
        options.AddDateTimeConvertersAndIndent();
        JsonConverterRegistrationManager.ApplyAll(options);
        ConvertersHelpers.PopulateConverters(options);
        Action<JsonSerializerOptions>? action = MultipleContextHelpers<T>.ContextAction;
        action?.Invoke(options);
        JsonOptionsHelpers<T>.Options = options;
        return JsonOptionsHelpers<T>.Options;
    }
    public static async Task<string> SerializeObjectAsync<T>(T thisObj)
    {
        string thisStr = "";
        await Task.Run(() =>
        {
            if (CustomSerializeHelpers<T>.MasterContext is not null)
            {
                thisStr =  CustomSerializeHelpers<T>.MasterContext.Serialize(thisObj);
            }
            else if (RequireCustomSerialization)
            {
                throw new CustomBasicException($"Requires custom serialization.  The type you are trying to serialize was {typeof(T)}");
            }
            else
            {
                JsonSerializerOptions options = GetCustomJsonSerializerOptions<T>();
                thisStr = tt1.Serialize(thisObj, options);
            } 
        });
        return thisStr;
    }
    public static async Task<T> DeserializeObjectAsync<T>(string thisStr)
    {
        T thisT = default!;
        await Task.Run(() =>
        {
            if (CustomSerializeHelpers<T>.MasterContext is not null)
            {
                thisT = CustomSerializeHelpers<T>.MasterContext.Deserialize(thisStr);
            }
            else if (RequireCustomSerialization)
            {
                throw new CustomBasicException($"Requires custom serialization.  The type you are trying to serialize was {typeof(T)}");
            }
            else
            {
                JsonSerializerOptions options = GetCustomJsonSerializerOptions<T>();
                thisT = tt1.Deserialize<T>(thisStr, options)!;
            }
        });
        return thisT!;
    }
    public static D ConvertObject<D, S>(S thisObj)
    {
        string thisStr = SerializeObject(thisObj!);
        return DeserializeObject<D>(thisStr);
    }
    public static string SerializeObject<T>(T thisObj)
    {
        string thisStr;
        if (CustomSerializeHelpers<T>.MasterContext is not null)
        {
            return CustomSerializeHelpers<T>.MasterContext.Serialize(thisObj);
        }
        else if (RequireCustomSerialization)
        {
            throw new CustomBasicException($"Requires custom serialization.  The type you are trying to serialize was {typeof(T)}");
        }
        JsonSerializerOptions options = GetCustomJsonSerializerOptions<T>();
        thisStr = tt1.Serialize(thisObj, options);
        return thisStr;
    }
    public static T DeserializeObject<T>(string thisStr)
    {
        if (CustomSerializeHelpers<T>.MasterContext is not null)
        {
            return CustomSerializeHelpers<T>.MasterContext.Deserialize(thisStr);
        }
        else if (RequireCustomSerialization)
        {
            throw new CustomBasicException($"Requires custom serialization.  The type you are trying to serialize was {typeof(T)}");
        }
        T thisT;
        JsonSerializerOptions options = GetCustomJsonSerializerOptions<T>();
        thisT = tt1.Deserialize<T>(thisStr, options)!;
        return thisT;
    }
    public static async Task<T> ConvertObjectAsync<T>(T thisObj)
    {
        string thisStr = await SerializeObjectAsync(thisObj);
        return await DeserializeObjectAsync<T>(thisStr);
    }
}