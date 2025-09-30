#nullable enable
namespace CommonBasicLibraries.AdvancedGeneralFunctionsAndProcesses.TypeParsingHelpers;
internal class DateOnlyTypeGenerator : ITypeParsingProvider<DateOnly>
{
    BasicList<string> ITypeParsingProvider<DateOnly>.GetSupportedList => [];
    bool ITypeParsingProvider<DateOnly>.TryParse(string input, out DateOnly result, out string? errorMessage)
    {
        if (input.IsValidDate(out DateOnly? possible) == false)
        {
            errorMessage = $"{input} is not a valid DateOnly format";
            result = default;
            return false;
        }
        result = possible!.Value;
        errorMessage = null;
        return true;
    }
}