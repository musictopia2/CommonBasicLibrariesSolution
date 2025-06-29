namespace CommonBasicLibraries.AdvancedGeneralFunctionsAndProcesses.BlobStorage;
public static class BlobStorageConfigurationExtensions
{
    public static string GetBlobStorageRootPath(this IConfiguration configuration)
    {
        var value = configuration[BlobStorageConfigurationKeys.StorageRootPathKey];
        if (string.IsNullOrEmpty(value))
        {
            throw new ConfigurationKeyNotFoundException("The blob storage root path key is missing.");
        }
        return value;
    }
    public static string GetAzureBlobConnectionString(this IConfiguration configuration)
    {
        var value = configuration[BlobStorageConfigurationKeys.AzureConnectionStringKey];
        if (string.IsNullOrEmpty(value))
        {
            throw new ConfigurationKeyNotFoundException("The Azure Blob Storage connection string is missing.");
        }
        return value;
    }
    public static string GetAzureBlobContainerName(this IConfiguration configuration)
    {
        var value = configuration[BlobStorageConfigurationKeys.AzureContainerNameKey];
        if (string.IsNullOrEmpty(value))
        {
            throw new ConfigurationKeyNotFoundException("The Azure container name is missing.");
        }
        return value;
    }
}