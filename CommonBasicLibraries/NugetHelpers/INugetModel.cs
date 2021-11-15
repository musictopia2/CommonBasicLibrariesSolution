namespace CommonBasicLibraries.NugetHelpers
{
    public interface INugetModel
    {
        string CSPath { get; set; }
        string ProjectDirectory { get; set; }
        string NugetPath { get; set; }
    }
}