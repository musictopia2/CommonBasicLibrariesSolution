namespace CommonBasicLibraries.AdvancedGeneralFunctionsAndProcesses.JsonSerializers;
public static class Extensions
{
    public static void AddDateTimeConvertersAndIndent(this JsonSerializerOptions options)
    {
#if NET6_0_OR_GREATER
        options.Converters.Add(new JsonDateOnlyConverter());
        options.Converters.Add(new JsonTimeOnlyConverter());
#endif
        options.WriteIndented = true;
        options.PropertyNameCaseInsensitive = true; //try this way now (?)
    }
}