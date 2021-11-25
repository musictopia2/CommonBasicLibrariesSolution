namespace CommonBasicLibraries.NugetHelpers;
public interface INugetKey
{
    Task<string> GetKeyAsync(); //you need to provide the key which is used to upload packages.
}