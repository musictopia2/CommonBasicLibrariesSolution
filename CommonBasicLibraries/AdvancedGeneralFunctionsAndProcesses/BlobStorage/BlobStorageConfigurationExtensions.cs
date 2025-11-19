namespace CommonBasicLibraries.AdvancedGeneralFunctionsAndProcesses.BlobStorage;
public static class BlobStorageConfigurationExtensions
{
    extension(IConfiguration configuration)
    {
        public string BlobStorageRootPath
        {
            get
            {
                var value = configuration[BlobStorageConfigurationKeys.StorageRootPathKey];
                if (string.IsNullOrEmpty(value))
                {
                    throw new ConfigurationKeyNotFoundException("The blob storage root path key is missing.");
                }
                return value;
            }
        }
        public string AzureBlobConnectionString
        {
            get
            {
                var value = configuration[BlobStorageConfigurationKeys.AzureConnectionStringKey];
                if (string.IsNullOrEmpty(value))
                {
                    throw new ConfigurationKeyNotFoundException("The Azure Blob Storage connection string is missing.");
                }
                return value;
            }
        }
        public string AzureBlobContainerName
        {
            get
            {
                var value = configuration[BlobStorageConfigurationKeys.AzureContainerNameKey];
                if (string.IsNullOrEmpty(value))
                {
                    throw new ConfigurationKeyNotFoundException("The Azure container name is missing.");
                }
                return value;
            }
        }
    }
}