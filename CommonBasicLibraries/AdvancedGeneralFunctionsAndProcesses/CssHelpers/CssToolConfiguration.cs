namespace CommonBasicLibraries.AdvancedGeneralFunctionsAndProcesses.CssHelpers;
public class CssToolConfiguration
{
    public string DefaultBasePath { get; set; } = "";
    public BasicList<CssFileConfig> Files { get; set; } = [];
    public BasicList<string> GlobalExcludedClasses { get; set; } = [];
}