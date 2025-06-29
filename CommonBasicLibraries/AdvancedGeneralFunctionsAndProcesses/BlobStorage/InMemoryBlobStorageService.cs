using System.Collections.Concurrent; //not common enough for now.
namespace CommonBasicLibraries.AdvancedGeneralFunctionsAndProcesses.BlobStorage;
public class InMemoryBlobStorageService : IBlobStorageService
{
    // Static so all instances across the process share the same "blob store"
    private static readonly ConcurrentDictionary<string, byte[]> _blobs = new();
    Task IBlobStorageService.DeleteAsync(string blobName)
    {
        _blobs.TryRemove(blobName, out _);
        return Task.CompletedTask;
    }
    Task<Stream> IBlobStorageService.DownloadAsync(string blobName)
    {
        if (_blobs.TryGetValue(blobName, out var content))
        {
            var memoryStream = new MemoryStream(content);
            return Task.FromResult<Stream>(memoryStream);
        }

        throw new FileNotFoundException($"Blob '{blobName}' not found in in-memory storage.");
    }

    Task<bool> IBlobStorageService.ExistsAsync(string blobName)
    {
        return Task.FromResult(_blobs.ContainsKey(blobName));
    }

    Uri IBlobStorageService.GetUri(string blobName)
    {
        // This URI is fake — just used for consistency or logging.
        return new Uri($"inmemory://{Uri.EscapeDataString(blobName)}");
    }

    Task IBlobStorageService.UploadAsync(string blobName, Stream data)
    {
        using var memoryStream = new MemoryStream();
        data.CopyTo(memoryStream);
        _blobs[blobName] = memoryStream.ToArray();
        return Task.CompletedTask;
    }
}