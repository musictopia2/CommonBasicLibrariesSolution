namespace CommonBasicLibraries.AdvancedGeneralFunctionsAndProcesses.UploadHelpers;
public abstract class BaseUploadSettingsContext
{
    public const string ConfigureName = nameof(Configure);
    protected abstract void Configure(IUploadConfig config);
    public virtual bool ServerAlone { get; } //defaults to false so existing can work without breaking changes.
}