namespace CommonBasicLibraries.AdvancedGeneralFunctionsAndProcesses.UploadHelpers;
public record UploadFileInfo(string PropertyName,
        long MaxSize,
        BasicList<string> AllowedContentTypes);