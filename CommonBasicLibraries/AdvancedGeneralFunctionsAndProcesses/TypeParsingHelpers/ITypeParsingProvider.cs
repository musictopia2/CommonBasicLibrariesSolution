namespace CommonBasicLibraries.AdvancedGeneralFunctionsAndProcesses.TypeParsingHelpers;
public interface ITypeParsingProvider<T>
    where T : notnull
{
    bool TryParse(string input, out T result, out string? errorMessage);
    BasicList<string> GetSupportedList { get; }
}