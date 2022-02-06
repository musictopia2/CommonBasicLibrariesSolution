namespace CommonBasicLibraries.AdvancedGeneralFunctionsAndProcesses.JsonSerializers;
//has to be public so source generators can hook into this.
public static class CustomSerializeHelpers<T>
{
    public static ICustomJsonContext<T>? MasterContext { get; set; }
}