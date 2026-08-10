namespace CommonBasicLibraries.AdvancedGeneralFunctionsAndProcesses.BuildHooks;
public sealed record SolutionHookArgs(
    string SolutionFileName,
    string SolutionDir);