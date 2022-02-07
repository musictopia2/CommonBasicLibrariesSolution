namespace CommonBasicLibraries.AdvancedGeneralFunctionsAndProcesses.MapHelpers;
public interface ICustomMapContext<T> //source generators will implement this interface and use.
{
    public T AutoMap(IMappable payLoad);
}