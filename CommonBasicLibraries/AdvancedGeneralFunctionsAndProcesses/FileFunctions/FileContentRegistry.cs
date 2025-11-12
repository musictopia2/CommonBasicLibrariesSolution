namespace CommonBasicLibraries.AdvancedGeneralFunctionsAndProcesses.FileFunctions;
public static class FileContentRegistry
{
    private static readonly Dictionary<string, string> _files = new(StringComparer.OrdinalIgnoreCase);
    public static string GetFile(string key)
    {
        if (_files.TryGetValue(key, out var data))
        {
            return data;
        }
        throw new CustomBasicException($"{key} file content was not found");
    }
    //something can clear if i go to another game.
    public static void ClearFiles()
    {
        _files.Clear();
    }
    public static void RegisterFile(string key, string data)
    {
        if (_files.ContainsKey(key))
        {
            //if already there, then ignore it.
            return;
        }
        _files[key] = data;
        //tools would register it.
    }
}