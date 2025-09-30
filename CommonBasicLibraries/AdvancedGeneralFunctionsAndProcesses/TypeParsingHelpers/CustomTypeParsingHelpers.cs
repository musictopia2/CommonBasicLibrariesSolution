namespace CommonBasicLibraries.AdvancedGeneralFunctionsAndProcesses.TypeParsingHelpers;
public static class CustomTypeParsingHelpers<T>
    where T: notnull
{
    public static ITypeParsingProvider<T>? MasterContext { get; set; }
}