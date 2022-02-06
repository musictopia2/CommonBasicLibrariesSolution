namespace CommonBasicLibraries.AdvancedGeneralFunctionsAndProcesses.JsonSerializers;
public interface ICustomJsonContext<T>
{
    T Deserialize(string json);
    string Serialize(T obj);
}