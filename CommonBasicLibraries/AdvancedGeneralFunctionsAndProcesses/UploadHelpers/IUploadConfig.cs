namespace CommonBasicLibraries.AdvancedGeneralFunctionsAndProcesses.UploadHelpers;
public interface IUploadConfig
{
    IUploadConfig Make<T>(Action<IUploadSettingsPropertyConfig<T>> action);
}