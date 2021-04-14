using static CommonBasicLibraries.BasicDataSettingsAndProcesses.VBCompat;
namespace CommonBasicLibraries.AdvancedGeneralFunctionsAndProcesses.BasicExtensions
{
    public static class Characters
    {
        public static bool IsAlpha(this char thisChar, bool allowDots = false)
        {
            if (AscW(thisChar) >= 97 && AscW(thisChar) <= 122)
            {
                return true;// because lower case
            }
            if (AscW(thisChar) >= 65 && AscW(thisChar) <= 90)
            {
                return true;
            }
            if (allowDots == true)
            {
                if (AscW(thisChar) == 46)
                {
                    return true;
                }
            }
            return false;
        }
        public static bool IsInteger(this char thisChar)
        {
            if (AscW(thisChar) >= 48 && AscW(thisChar) <= 57)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}