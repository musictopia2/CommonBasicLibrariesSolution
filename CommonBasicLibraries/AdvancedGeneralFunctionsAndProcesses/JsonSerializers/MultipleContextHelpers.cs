namespace CommonBasicLibraries.AdvancedGeneralFunctionsAndProcesses.JsonSerializers;
public static class MultipleContextHelpers<T>
{
    public static Action<JsonSerializerOptions>? ContextAction { get; set; }
}