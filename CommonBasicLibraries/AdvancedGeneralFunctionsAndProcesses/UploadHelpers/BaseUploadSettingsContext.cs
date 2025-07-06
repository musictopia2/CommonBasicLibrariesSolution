namespace CommonBasicLibraries.AdvancedGeneralFunctionsAndProcesses.UploadHelpers;
public abstract class BaseUploadSettingsContext
{
    public const string ConfigureName = nameof(Configure);
    protected abstract void Configure(IUploadConfig config);
}