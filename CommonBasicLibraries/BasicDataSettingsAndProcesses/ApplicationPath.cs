namespace CommonBasicLibraries.BasicDataSettingsAndProcesses;
public static class ApplicationPath
{
    public static string GetApplicationPath() // this is the path the application is in
    {
        return AppDomain.CurrentDomain.BaseDirectory;
    }
    public static bool IsDebug()
    {
        return System.Diagnostics.Debugger.IsAttached;
    }
}