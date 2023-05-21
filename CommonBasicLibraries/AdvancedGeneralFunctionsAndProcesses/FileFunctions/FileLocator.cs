namespace CommonBasicLibraries.AdvancedGeneralFunctionsAndProcesses.FileFunctions;
public static class FileLocator
{
    public static string MainLocation { get; set; } = "";
    private static string _lookup = "";
    private static string _previousLocation = "";
    private static string _originalPath = "";
    private static string OldLocation(string name)
    {
        string olds = MainLocation;
        _originalPath = aa1.GetApplicationPath();
        int finds = _originalPath.IndexOf(MainLocation);
        if (finds == -1)
        {
            return "";
        }
        string modified = _originalPath.Substring(0, finds);
        modified += olds += @"\";
        modified += name;
        string other = ff1.GetParentPath(modified);
        if (ff1.DirectoryExists(other) == false)
        {
            return "";
        }
        return modified;
    }
    private static string StartingPath()
    {
        string path = "";
        path = ff1.GetParentPath(_originalPath);
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

            if (name.ToLower() == _lookup)
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
        return $"{folders}/{name}";
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