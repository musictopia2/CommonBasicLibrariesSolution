#nullable enable
namespace CommonBasicLibraries.AdvancedGeneralFunctionsAndProcesses.TypeParsingHelpers;
internal class CharTypeGenerator : ITypeParsingProvider<char>
{
    BasicList<string> ITypeParsingProvider<char>.GetSupportedList => [];
    bool ITypeParsingProvider<char>.TryParse(string input, out char result, out string? errorMessage)
    {
        if (char.TryParse(input, out char possible) == false)
        {
            errorMessage = $"{input} was not a valid char";
            result = default;
            return false;
        }
        result = possible;
        errorMessage = null;
        return true;
    }
}