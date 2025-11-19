namespace CommonBasicLibraries.AdvancedGeneralFunctionsAndProcesses.BasicExtensions;
public static class Characters
{
    extension(char thisChar)
    {
        public bool IsInteger
        {
            get
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
        public bool IsAlphaNoDots => thisChar.IsAlpha();
        public bool IsAlphaAllowingDots => thisChar.IsAlpha(true);
        internal bool IsAlpha(bool allowDots = false)
        {
            if (AscW(thisChar) >= 97 && AscW(thisChar) <= 122)
            {
                return true;//
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
    }
}