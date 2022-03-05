namespace CommonBasicLibraries.NugetHelpers;
public class VisualStudioProjectModel : INugetModel, IProjectModel
{
    public EnumStatus Status { get; set; }
    public string ProjectDirectory { get; set; } = "";
    public string LastVersion { get; set; } = "";
    public string CSPath { get; set; } = "";
    public string NugetPath { get; set; } = "";
    public string DLLPath { get; set; } = ""; //these 2 may or may not be needed.  if not needed, then just ignore if somebody decides to use this model.
    public DateTime LastModified { get; set; }
    public bool TemporarilyIgnore { get; set; } //decided to have a way to temporarily ignore one for nuget.  this can be useful if you want something on the list but temporarily ignore because of possible issues (which would be resolved in the future).
}