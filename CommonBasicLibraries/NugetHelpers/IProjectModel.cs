namespace CommonBasicLibraries.NugetHelpers;
public interface IProjectModel
{
    string CSPath { get; set; }
    string LastVersion { get; set; }
}