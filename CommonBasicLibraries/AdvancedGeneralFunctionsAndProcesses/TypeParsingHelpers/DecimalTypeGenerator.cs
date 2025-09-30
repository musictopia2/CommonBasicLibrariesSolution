#nullable enable
namespace CommonBasicLibraries.AdvancedGeneralFunctionsAndProcesses.TypeParsingHelpers;
internal class DecimalTypeGenerator : ITypeParsingProvider<decimal>
{
    BasicList<string> ITypeParsingProvider<decimal>.GetSupportedList => [];
    bool ITypeParsingProvider<decimal>.TryParse(string input, out decimal result, out string? errorMessage)
    {
        if (decimal.TryParse(input, out decimal possible) == false)
        {
            errorMessage = $"{input} was not a valid decimal";
            result = default;
            return false;
        }
        result = possible;
        errorMessage = null;
        return true;
    }
}