namespace CommonBasicLibraries.NugetHelpers
{
    public interface INugetPacker
    {
        Task<bool> CreateNugetPackageAsync(INugetModel project, bool useVsVersioning);
    }
}