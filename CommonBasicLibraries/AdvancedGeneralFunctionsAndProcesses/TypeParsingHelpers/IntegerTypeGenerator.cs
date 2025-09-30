#nullable enable
namespace CommonBasicLibraries.AdvancedGeneralFunctionsAndProcesses.TypeParsingHelpers;
internal class IntegerTypeGenerator : ITypeParsingProvider<int>
{
    BasicList<string> ITypeParsingProvider<int>.GetSupportedList => [];
    bool ITypeParsingProvider<int>.TryParse(string input, out int result, out string? errorMessage)
    {
        if (int.TryParse(input, out int possible) == false)
        {
            errorMessage = $"{input} was not a valid integer";
            result = default;
            return false;
        }
        result = possible;
        errorMessage = null;
        return true;
    }
}