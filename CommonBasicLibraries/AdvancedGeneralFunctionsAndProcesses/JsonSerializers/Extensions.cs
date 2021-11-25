namespace CommonBasicLibraries.AdvancedGeneralFunctionsAndProcesses.JsonSerializers;
public static class Extensions
{
    public static void AddConvertersAndIndent(this JsonSerializerOptions options)
    {
        options.Converters.Add(new JsonDateOnlyConverter());
        options.Converters.Add(new LimitedMappableConverter());
        options.Converters.Add(new JsonTimeOnlyConverter());
        options.WriteIndented = true;
        options.PropertyNameCaseInsensitive = true; //try this way now (?)
    }
}