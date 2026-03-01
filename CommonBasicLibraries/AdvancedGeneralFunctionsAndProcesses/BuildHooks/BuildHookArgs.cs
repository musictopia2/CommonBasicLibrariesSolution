namespace CommonBasicLibraries.AdvancedGeneralFunctionsAndProcesses.BuildHooks;
public sealed record BuildHookArgs(
    string ProjectName,
    string ProjectDir,
    string ProjectFileName,
    string? OutputDir)
{
    /// <summary>
    /// True when an output directory was supplied.
    /// </summary>
    public bool HasOutputDir =>
        string.IsNullOrWhiteSpace(OutputDir) == false;

    /// <summary>
    /// Use when the operation requires an output directory.
    /// Throws a clear error if missing.
    /// </summary>
    public string RequireOutputDir()
    {
        if (!HasOutputDir)
        {
            throw new InvalidOperationException(
                "This operation requires an output directory argument.");
        }

        return OutputDir!;
    }
}