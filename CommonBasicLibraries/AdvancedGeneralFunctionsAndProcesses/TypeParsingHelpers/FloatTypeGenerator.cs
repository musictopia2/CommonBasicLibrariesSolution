#nullable enable
namespace CommonBasicLibraries.AdvancedGeneralFunctionsAndProcesses.TypeParsingHelpers;
internal class FloatTypeGenerator : ITypeParsingProvider<float>
{
    BasicList<string> ITypeParsingProvider<float>.GetSupportedList => [];
    bool ITypeParsingProvider<float>.TryParse(string input, out float result, out string? errorMessage)
    {
        if (float.TryParse(input, out float possible) == false)
        {
            errorMessage = $"{input} was not a valid float";
            result = default;
            return false;
        }
        result = possible;
        errorMessage = null;
        return true;
    }
}