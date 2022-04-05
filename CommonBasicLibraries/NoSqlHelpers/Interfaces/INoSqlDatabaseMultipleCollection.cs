namespace CommonBasicLibraries.NoSqlHelpers.Interfaces;
public interface INoSqlDatabaseMultipleCollection : INoSqlDatabaseName
{
    void Configure(INoSqlMapping config);
}