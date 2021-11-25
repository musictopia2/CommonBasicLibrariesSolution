namespace CommonBasicLibraries.DatabaseHelpers.MiscInterfaces;
/// <summary>
/// This has methods used for cases where i access data manually and not doing automapping
/// This is designed for sqlite.
/// The good news a standard library can invoke with no sqlite dependency.  something else has to worry about that part.
/// needs both async and regular methods.
/// i did like the idea of returning list.  if you need one item, then just do .first().
/// </summary>
public interface ISqliteManuelDataAccess
{
    //this assumes you already have a connection string.
    BasicList<T> LoadData<T, U>(string sqlStatement,
                           U parameters);
    Task<BasicList<T>> LoadDataAsync<T, U>(string sqlStatement,
                           U parameters);
    void SaveData<T>(string sqlStatement,
                           T parameters);
    Task SaveDataAsync<T>(string sqlStatement,
                           T parameters);
}