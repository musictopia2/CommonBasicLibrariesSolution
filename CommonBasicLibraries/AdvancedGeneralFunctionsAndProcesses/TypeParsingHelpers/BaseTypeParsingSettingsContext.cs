namespace CommonBasicLibraries.AdvancedGeneralFunctionsAndProcesses.TypeParsingHelpers;
public abstract class BaseTypeParsingSettingsContext
{
    public const string ConfigureName = nameof(Configure);
    public abstract void Configure(ITypeParsingConfig config);
}