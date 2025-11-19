namespace CommonBasicLibraries.AdvancedGeneralFunctionsAndProcesses.BasicExtensions;
public static class IConfigurationExtensions
{
    extension(IConfiguration config)
    {
        public void UpdateValue<T>(string key, T value)
        {
            config[key] = value!.ToString();
        }
        public string NetVersion
        {
            get
            {
                string? output = config.GetValue<string>("NetVersion") ?? throw new CustomBasicException("Did not find the key NetVersion.  Try registering the source to get NetVersion");
                return output;
            }
        }
    }
}