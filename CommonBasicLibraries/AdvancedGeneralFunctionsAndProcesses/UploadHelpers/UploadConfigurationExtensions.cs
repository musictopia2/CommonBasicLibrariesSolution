namespace CommonBasicLibraries.AdvancedGeneralFunctionsAndProcesses.UploadHelpers;
public static class UploadConfigurationExtensions
{
    extension (IConfiguration configuration)
    {
        public string GetUploadSavePath
        {
            get
            {
                var value = configuration[UploadConfigurationKeys.UploadSavePathKey];
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ConfigurationKeyNotFoundException("Upload save path is not configured.");
                }
                return value;
            }
        }
    }
}