namespace CommonBasicLibraries.AdvancedGeneralFunctionsAndProcesses.UploadHelpers;
public interface IUploadSettingsPropertyConfig<T>
{
    IUploadSettingsPropertyConfig<T> SetUploadFile<P>(
        Func<T, P> propertySelector,
        long maxSize,
        BasicList<string> contentTypes,
        bool required = true);
}