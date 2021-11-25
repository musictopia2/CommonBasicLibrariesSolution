namespace CommonBasicLibraries.NugetHelpers;
public interface INugetModel
{
    string CSPath { get; set; }
    string ProjectDirectory { get; set; }
    string NugetPath { get; set; }
    string LastVersion { get; set; } //i will have the flexibility of deciding whether the version is done via vs projects or if done via custom means.
}