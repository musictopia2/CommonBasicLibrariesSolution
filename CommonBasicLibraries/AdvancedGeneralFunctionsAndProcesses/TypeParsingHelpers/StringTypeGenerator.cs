#nullable enable
namespace CommonBasicLibraries.AdvancedGeneralFunctionsAndProcesses.TypeParsingHelpers;
internal class StringTypeGenerator : ITypeParsingProvider<string>
{
    BasicList<string> ITypeParsingProvider<string>.GetSupportedList => [];
    bool ITypeParsingProvider<string>.TryParse(string input, out string result, out string? errorMessage)
    {
        result = input;
        errorMessage = null;
        return true;
    }
}