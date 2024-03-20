namespace CommonBasicLibraries.AdvancedGeneralFunctionsAndProcesses.BasicExtensions;
public static class IConfigurationExtensions
{
    public static void UpdateValue<T>(this IConfiguration config, string key, T value)
    {
        config[key] = value!.ToString();
    }
    public static string GetNetVersion(this IConfiguration config)
    {
        return config.GetValue<string>("NetVersion")!;
    }
}