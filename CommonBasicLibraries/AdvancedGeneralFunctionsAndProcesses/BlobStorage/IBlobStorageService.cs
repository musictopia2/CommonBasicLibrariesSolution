namespace CommonBasicLibraries.AdvancedGeneralFunctionsAndProcesses.BlobStorage;
public interface IBlobStorageService
{
    Task UploadAsync(string blobName, Stream data);
    Task<Stream> DownloadAsync(string blobName);
    Task DeleteAsync(string blobName);
    Task<bool> ExistsAsync(string blobName);
    /// <summary>
    /// Gets a public or logical URI for accessing the blob, depending on provider.
    /// </summary>
    Uri GetUri(string blobName);
}