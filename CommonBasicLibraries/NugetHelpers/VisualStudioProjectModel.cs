namespace CommonBasicLibraries.NugetHelpers
{
    public class VisualStudioProjectModel : INugetModel, IProjectModel
    {
        public EnumStatus Status { get; set; }
        public string ProjectDirectory { get; set; } = "";
        public string LastVersion { get; set; } = "";
        public string CSPath { get; set; } = "";
        public string NugetPath { get; set; } = "";
        public string DLLPath { get; set; } = ""; //these 2 may or may not be needed.  if not needed, then just ignore if somebody decides to use this model.
        public DateTime LastModified { get; set; }
    }
}