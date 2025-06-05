namespace CommonBasicLibraries.AdvancedGeneralFunctionsAndProcesses.JsonSerializers;
public static class JsonCustomRegistrationHelpers
{
    public static void AddEnumFlagSerializationOptions<E>()
        where E : struct, Enum
    {
        JsonConverterRegistrationManager.Register(options =>
        {
            options.Converters.Add(new FlagsWrapperJsonConverter<E>());
        });
    }
}