#nullable enable
namespace CommonBasicLibraries.AdvancedGeneralFunctionsAndProcesses.TypeParsingHelpers;
internal class DoubleTypeGenerator : ITypeParsingProvider<double>
{
    BasicList<string> ITypeParsingProvider<double>.GetSupportedList => [];
    bool ITypeParsingProvider<double>.TryParse(string input, out double result, out string? errorMessage)
    {
        if (double.TryParse(input, out double possible) == false)
        {
            errorMessage = $"{input} was not a valid double";
            result = default;
            return false;
        }
        result = possible;
        errorMessage = null;
        return true;
    }
}