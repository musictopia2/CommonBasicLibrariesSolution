namespace CommonBasicLibraries.AdvancedGeneralFunctionsAndProcesses.BuildHooks;
public readonly record struct BuildHookArgs4(
    string ProjectName,
    string ProjectDirectory,
    string ProjectFile,
    string OutputDirectory
    );