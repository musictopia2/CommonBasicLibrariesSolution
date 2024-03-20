namespace CommonBasicLibraries.AdvancedGeneralFunctionsAndProcesses.BasicExtensions;
public static class IConfigurationExtensions
{
    public static void UpdateValue<T>(this IConfiguration config, string key, T value)
    {
        config[key] = value!.ToString();
    }
    public static string GetNetVersion(this IConfiguration config)
    {
        string? output = config.GetValue<string>("NetVersion");
        if (output is null)
        {
            throw new CustomBasicException("Did not find the key NetVersion.  Try registering the source to get NetVersion");
        }
        return output;
    }
}