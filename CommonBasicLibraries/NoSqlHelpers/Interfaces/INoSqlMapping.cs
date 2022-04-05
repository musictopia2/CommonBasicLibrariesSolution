namespace CommonBasicLibraries.NoSqlHelpers.Interfaces;
public interface INoSqlMapping
{
    INoSqlMapping Make<T>(string collectionName);
}