namespace CommonBasicLibraries.AdvancedGeneralFunctionsAndProcesses.UploadHelpers;
public static class UploadConfigurationExtensions
{
    public static string GetUploadSavePath(this IConfiguration configuration)
    {
        var value = configuration[UploadConfigurationKeys.UploadSavePathKey];
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ConfigurationKeyNotFoundException("Upload save path is not configured.");
        }

        return value;
    }
}