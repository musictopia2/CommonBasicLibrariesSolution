namespace CommonBasicLibraries.AdvancedGeneralFunctionsAndProcesses.CssHelpers;
/// <summary>
/// Represents a custom override for grouping related CSS class patterns 
/// into more meaningful or user-friendly class groupings.
/// </summary>
public class GroupOverride
{
    /// <summary>
    /// The original pattern name found in the CSS, such as "btn" or "alert".
    /// This corresponds to the prefix before the dash in class names (e.g., "btn-primary").
    /// </summary>
    public string OriginalName { get; set; } = "";

    /// <summary>
    /// The desired display name to use in generated code. 
    /// For example, "Button" instead of "Btn".
    /// </summary>
    public string DisplayName { get; set; } = "";

    /// <summary>
    /// An optional override for the C# namespace the generated class should be placed into.
    /// If left blank, a default namespace will be used.
    /// </summary>
    public string Namespace { get; set; } = "";

}