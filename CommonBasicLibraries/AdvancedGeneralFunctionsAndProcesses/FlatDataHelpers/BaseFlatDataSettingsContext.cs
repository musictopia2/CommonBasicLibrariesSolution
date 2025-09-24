namespace CommonBasicLibraries.AdvancedGeneralFunctionsAndProcesses.FlatDataHelpers;
public abstract class BaseFlatDataSettingsContext
{
    public const string ConfigureName = nameof(Configure);
    protected abstract void Configure(IFlatDataConfig config);
}