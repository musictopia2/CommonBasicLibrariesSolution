namespace CommonBasicLibraries.AdvancedGeneralFunctionsAndProcesses.BlobStorage;
public class LocalFileBlobStorageService : IBlobStorageService
{
    private readonly IConfiguration _configuration = bb1.Configuration ?? throw new CustomBasicException("Needs IConfiguration Registered");
    private string GetBlobStoragePath()
    {
        var path = _configuration.GetBlobStorageRootPath();
        if (ff1.DirectoryExists(path) == false)
        {
            ff1.CreateFolder(path);
        }
        return path;
    }
    async Task IBlobStorageService.DeleteAsync(string blobName)
    {
        string path = GetBlobStoragePath();
        var filePath = Path.Combine(path, blobName);
        if (ff1.FileExists(filePath) == false)
        {
            return;  // no need to throw an exception if the file does not exist, just return.
        }
        await ff1.DeleteFileAsync(filePath);
    }
    async Task<Stream> IBlobStorageService.DownloadAsync(string blobName)
    {
        string path = GetBlobStoragePath();
        var filePath = Path.Combine(path, blobName);
        if (!File.Exists(filePath))
        {
            throw new FileNotFoundException();
        }
        var memory = new MemoryStream();
        using var fileStream = File.OpenRead(filePath);
        await fileStream.CopyToAsync(memory);
        memory.Position = 0;
        return memory;
    }

    Task<bool> IBlobStorageService.ExistsAsync(string blobName)
    {
        string path = GetBlobStoragePath();
        var filePath = Path.Combine(path, blobName);
        return Task.FromResult(ff1.FileExists(filePath));
    }


    //decided to not give out the uri since a person may choose another provider and would break everything.  so instead just show the blob names.
    //obviously, if a person went from local to azure, then they have to upload the blobs to azure.
    async Task IBlobStorageService.UploadAsync(string blobName, Stream data)
    {
        string path = GetBlobStoragePath();
        var filePath = Path.Combine(path, blobName);
        var directory = Path.GetDirectoryName(filePath);
        if (string.IsNullOrWhiteSpace(directory) == false)
        {
            Directory.CreateDirectory(directory);
        }
        using var fileStream = File.Create(filePath);
        await data.CopyToAsync(fileStream);
    }
    Uri IBlobStorageService.GetUri(string blobName)
    {
        string path = GetBlobStoragePath();
        var filePath = Path.Combine(path, blobName);
        return new Uri(filePath, UriKind.Absolute); // returns the local file path as a URI
    }
}