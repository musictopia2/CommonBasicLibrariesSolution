namespace CommonBasicLibraries.AdvancedGeneralFunctionsAndProcesses.FileFunctions;
public static class FileLocator
{
    public static string MainLocation { get; set; } = "";
    private static string _lookup = "";
    private static string _previousLocation = "";
    private static string _originalPath = "";

    private static string BuildPath(string root, string name)
    {
        if (string.IsNullOrWhiteSpace(root))
        {
            return name;
        }

        string cleanedRoot = root.TrimEnd('\\', '/');
        string cleanedName = name.TrimStart('\\', '/');

        if (string.IsNullOrWhiteSpace(MainLocation))
        {
            return $@"{cleanedRoot}\{cleanedName}";
        }

        string cleanedMain = MainLocation.Trim('\\', '/');

        // If name already starts with MainLocation, do not add it again
        if (cleanedName.StartsWith(cleanedMain + @"\", StringComparison.CurrentCultureIgnoreCase) ||
            cleanedName.StartsWith(cleanedMain + @"/", StringComparison.CurrentCultureIgnoreCase) ||
            cleanedName.Equals(cleanedMain, StringComparison.CurrentCultureIgnoreCase))
        {
            return $@"{cleanedRoot}\{cleanedName}";
        }

        return $@"{cleanedRoot}\{cleanedMain}\{cleanedName}";
    }

    private static string OldLocation(string name)
    {
        _originalPath = aa1.GetApplicationPath();
        int finds = _originalPath.IndexOf(MainLocation, StringComparison.CurrentCultureIgnoreCase);
        if (finds == -1)
        {
            return "";
        }

        string modified = _originalPath.Substring(0, finds).TrimEnd('\\', '/');
        string result = BuildPath(modified, name);

        string other = ff1.GetParentPath(result);
        if (ff1.DirectoryExists(other) == false)
        {
            return "";
        }

        return result;
    }

    private static string StartingPath()
    {
        string path = ff1.GetParentPath(_originalPath);
        4.Times(() =>
        {
            path = ff1.GetParentPath(path);
        });
        return path;
    }

    private static void StartLookup()
    {
        if (_previousLocation == MainLocation)
        {
            return;
        }
        _lookup = MainLocation.ToLower();
        _lookup = _lookup.Replace(@"\", "");
        _lookup = _lookup.Replace(@"/", "");
        _previousLocation = MainLocation;
    }

    private static string GetFolderPath(string starting)
    {
        string path = starting;
        do
        {
            if (FolderExist(path))
            {
                return path;
            }
            if (path == @"C:\")
            {
                throw new CustomBasicException($"Path not found for getting location.  Location attempted was {MainLocation}");
            }
            path = ff1.GetParentPath(path);
        } while (true);
    }

    private static bool FolderExist(string path)
    {
        BasicList<string> list = ff1.DirectoryList(path);
        foreach (var item in list)
        {
            string name = ff1.FileName(item);
            if (name.Equals(_lookup, StringComparison.CurrentCultureIgnoreCase))
            {
                return true;
            }
        }
        return false;
    }

    private static string NewLocation(string name)
    {
        if (_originalPath == "")
        {
            _originalPath = aa1.GetApplicationPath();
        }

        string firsts = StartingPath();
        string folders = GetFolderPath(firsts);

        string poss = BuildPath(folders, name);
        return poss;
    }

    public static string GetLocation(string name)
    {
        string originalProcesses = OldLocation(name);
        if (originalProcesses == "")
        {
            StartLookup();
            return NewLocation(name);
        }
        return originalProcesses;
    }
}