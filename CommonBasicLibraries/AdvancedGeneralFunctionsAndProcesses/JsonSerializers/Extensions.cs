namespace CommonBasicLibraries.AdvancedGeneralFunctionsAndProcesses.JsonSerializers;
public static class Extensions
{
    extension(JsonSerializerOptions options)
    {
        public void AddDateTimeConvertersAndIndent()
        {
            options.Converters.Add(new JsonDateOnlyConverter());
            options.Converters.Add(new JsonTimeOnlyConverter());
            options.WriteIndented = true;
            options.PropertyNameCaseInsensitive = true; //try this way now (?)
        }
    }
}