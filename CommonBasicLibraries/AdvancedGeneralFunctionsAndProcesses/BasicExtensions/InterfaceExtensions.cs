namespace CommonBasicLibraries.AdvancedGeneralFunctionsAndProcesses.BasicExtensions;
public static class InterfaceExtensions
{
    public static T AutoMap<T>(this IMappable payLoad)
        where T: new()
    {
        ICustomMapContext<T>? context = CustomMapperHelpers<T>.MasterContext;
        if (context is null)
        {
            throw new CustomBasicException($"There was no mappings found at {typeof(T)}.  Try to use source generators to create it");
        }
        return context.AutoMap(payLoad); //decided to do away with the dictionary way of doing it to improve performance.
    }
}