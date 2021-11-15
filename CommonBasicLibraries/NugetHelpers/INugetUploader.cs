namespace CommonBasicLibraries.NugetHelpers
{
    public interface INugetUploader
    {
        Task<bool> UploadNugetPackageAsync(string nugetPath); //this means there can even be a private version of this.
    }
}