namespace CommonBasicLibraries.AdvancedGeneralFunctionsAndProcesses.FileFunctions;
public static class LinuxPathHelper
{
    public static Dictionary<string, string> PathCaseMappings { get; set; } = new();
    // Call this once to hook into your bb1.GetCleanedPath delegate
    public static void SetupLinuxPathMapper()
    {
        bb1.GetCleanedPath = DefaultLinuxPathMapper;
    }
    private static string DefaultLinuxPathMapper(string path)
    {
        var output = path.Replace("\\", "/");

        foreach (var kvp in PathCaseMappings)
        {
            output = output.Replace(kvp.Key, kvp.Value, StringComparison.OrdinalIgnoreCase);
        }

        return output;
    }
}