namespace CommonBasicLibraries.AdvancedGeneralFunctionsAndProcesses.CssHelpers;
public class CssToolConfiguration
{
    public string DefaultBasePath { get; set; } = "";
    public string DefaultFolderPath { get; set; } = "Resources"; //default if nothing was specified.
    public BasicList<CssFileConfig> Files { get; set; } = [];
    public BasicList<string> GlobalExcludedClasses { get; set; } = [];
}