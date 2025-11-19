using System.Diagnostics.CodeAnalysis;
namespace CommonBasicLibraries.AdvancedGeneralFunctionsAndProcesses.BasicExtensions;
public static class Exceptions
{
    extension(CustomBasicException)
    {
        public static void ThrowIfNull([NotNull] object? value)
        {
            if (value is null)
            {
                throw new CustomBasicException("Was null.  Not correct");
            }
        }
        
    }
}
