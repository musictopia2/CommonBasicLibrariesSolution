namespace CommonBasicLibraries.AdvancedGeneralFunctionsAndProcesses.JsonSerializers;
public static class MultipleContextHelpers
{
    private static Dictionary<Type, Action<JsonSerializerOptions>> _contexts = new();
    private static HashSet<JsonConverter> _converters = new();
    public static void AddConverter<T>()
        where T : JsonConverter, new()
    {
        _converters.Add(new T());
    }
#if NET6_0_OR_GREATER
    //.net standard 2.0 does not have tryadd unfortunately.  just means the library that will add the context can't use .net standard 2.0.
    public static void AddContext<T>(Action<JsonSerializerOptions> process)
    {
        _contexts.TryAdd(typeof(T), process);
    }
#endif
    internal static void PopulateOptions<T>(JsonSerializerOptions options)
    {
        Type type = typeof(T);
        PopulateConverters(options);
        PopulateContexts(type, options);
    }
    internal static void PopulateConverters(JsonSerializerOptions options)
    {
        foreach (var c in _converters)
        {
            options.Converters.Add(c);
        }
    }
    private static void PopulateContexts(Type type, JsonSerializerOptions options)
    {
        foreach (var item in _contexts)
        {
            if (item.Key == type)
            {
                item.Value.Invoke(options); //this function is responsible for populating it.
                return;
            }
        }
    }
}