namespace CommonBasicLibraries.AdvancedGeneralFunctionsAndProcesses.CssHelpers;
public class CssFileConfig
{
    public string FileName { get; set; } = "";
    public string BasePath { get; set; } = "";
    public string OutputName { get; set; } = "";
    public string DefaultNamespace { get; set; } = "";
    public string DefaultCategory { get; set; } = "";
    /// A list of class names that should be completely excluded from code generation.
    /// Use this to manually suppress valid but undesired classes from IntelliSense.
    /// Example: "clearfix", "visually-hidden"
    public BasicList<string> LocalExcludedClasses { get; set; } = [];


    public BasicList<GroupOverride> Overrides { get; set; } = [];
}