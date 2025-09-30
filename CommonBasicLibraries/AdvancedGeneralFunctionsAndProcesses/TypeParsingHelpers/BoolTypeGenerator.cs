#nullable enable
namespace CommonBasicLibraries.AdvancedGeneralFunctionsAndProcesses.TypeParsingHelpers;
internal class BoolTypeGenerator : ITypeParsingProvider<bool>
{
    BasicList<string> ITypeParsingProvider<bool>.GetSupportedList => [];
    bool ITypeParsingProvider<bool>.TryParse(string input, out bool result, out string? errorMessage)
    {
        if (string.Equals(input, "yes", StringComparison.OrdinalIgnoreCase))
        {
            result = true;
            errorMessage = null;
            return true;
        }
        if (string.Equals(input, "true", StringComparison.OrdinalIgnoreCase))
        {
            result = true;
            errorMessage = null;
            return true;
        }
        if (string.Equals(input, "1", StringComparison.OrdinalIgnoreCase))
        {
            result = true;
            errorMessage = null;
            return true;
        }
        if (string.Equals(input, "y", StringComparison.OrdinalIgnoreCase))
        {
            result = true;
            errorMessage = null;
            return true;
        }
        if (string.Equals(input, "t", StringComparison.OrdinalIgnoreCase))
        {
            result = true;
            errorMessage = null;
            return true;
        }
        if (string.Equals(input, "no", StringComparison.OrdinalIgnoreCase))
        {
            result = false;
            errorMessage = null;
            return true;
        }
        if (string.Equals(input, "false", StringComparison.OrdinalIgnoreCase))
        {
            result = false;
            errorMessage = null;
            return true;
        }
        if (string.Equals(input, "0", StringComparison.OrdinalIgnoreCase))
        {
            result = false;
            errorMessage = null;
            return true;
        }
        if (string.Equals(input, "n", StringComparison.OrdinalIgnoreCase))
        {
            result = false;
            errorMessage = null;
            return true;
        }
        if (string.Equals(input, "f", StringComparison.OrdinalIgnoreCase))
        {
            result = false;
            errorMessage = null;
            return true;
        }
        errorMessage = $"Input '{input}' is not a valid boolean representation..";
        result = default;
        return false;
    }
}