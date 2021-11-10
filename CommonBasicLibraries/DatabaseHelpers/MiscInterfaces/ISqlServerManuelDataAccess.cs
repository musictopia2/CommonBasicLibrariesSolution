namespace CommonBasicLibraries.DatabaseHelpers.MiscInterfaces
{
    /// <summary>
    /// This has methods used for cases where i access data manually and not doing automapping
    /// This is designed for sql server.
    /// The good news a standard library can invoke with no sql server dependency.  something else has to worry about that part.
    /// needs both async and regular methods.
    /// i did like the idea of returning list.  if you need one item, then just do .first().
    /// </summary>
    public interface ISqlServerManuelDataAccess
    {
        BasicList<T> LoadData<T, U>(string sqlStatement,
                               U parameters,
                               bool isStoredProcedure = false);
        Task<BasicList<T>> LoadDataAsync<T, U>(string sqlStatement,
                               U parameters,
                               bool isStoredProcedure = false);
        void SaveData<T>(string sqlStatement,
                               T parameters,
                               bool isStoredProcedure = false);
        Task SaveDataAsync<T>(string sqlStatement,
                               T parameters,
                               bool isStoredProcedure = false);
    }
}