namespace CommonBasicLibraries.AdvancedGeneralFunctionsAndProcesses.JsonSerializers;
public static class ConvertersHelpers
{
    private readonly static HashSet<JsonConverter> _converters = new();
    public static void AddConverter<T>()
        where T : JsonConverter, new()
    {
        _converters.Add(new T());
    }
    internal static void PopulateConverters(JsonSerializerOptions options)
    {
        foreach (var c in _converters)
        {
            options.Converters.Add(c);
        }
    }
}