namespace CommonBasicLibraries.AdvancedGeneralFunctionsAndProcesses.FileFunctions;
public static class FileLocator
{
    public static string MainLocation { get; set; } = "";
    public static string GetLocation(string name)
    {
        string olds = MainLocation;
        string path = aa.GetApplicationPath();
        int finds = path.IndexOf(MainLocation);
        string modified = path.Substring(0, finds);
        modified += olds += @"\";
        modified += name;
        return modified;
    }
}