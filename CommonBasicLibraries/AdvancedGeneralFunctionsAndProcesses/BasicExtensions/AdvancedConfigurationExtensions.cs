namespace CommonBasicLibraries.AdvancedGeneralFunctionsAndProcesses.BasicExtensions;
public static class AdvancedConfigurationExtensions
{
    public static IAdvancedConfiguration AddLocalDB<T>(this IAdvancedConfiguration configuration)
        where T : ISqlDatabaseConfiguration
    {
        string key = GetDatabaseKey<T>();
        string value = $@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog={T.DatabaseName};Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        return configuration.Add(key, value);
    }
    public static IAdvancedConfiguration AddDocumentLocalDB<T>(this IAdvancedConfiguration configuration)
        where T : ISqlDocumentConfiguration
    {
        string key = GetDocumentSqlServerKey<T>();
        string value = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=DocumentDatabase;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        return configuration.Add(key, value);
    }
    public static IAdvancedConfiguration AddSqliteDocument<T>(this IAdvancedConfiguration configuration, string path)
        where T : ISqlDocumentConfiguration
    {
        string key = $"DocumentDatabaseSqlite-{T.DatabaseName}-{T.CollectionName}";
        return configuration.Add(key, path);
    }
    public static IAdvancedConfiguration AddSqliteStandardPath<T>(this IAdvancedConfiguration configuration, string path)
        where T : ISqlDatabaseConfiguration
    {
        string key = $"{T.DatabaseName}Path";
        return configuration.Add(key, path);
    }
    public static string GetDatabaseKey<T>()
        where T : ISqlDatabaseConfiguration
    {
        string key = $"ConnectionStrings:{T.DatabaseName}";
        return key;
    }
    public static string GetDocumentSqlServerKey<T>()
        where T : ISqlDocumentConfiguration
    {
        string key;
        key = $"ConnectionStrings:DocumentDatabaseSQLServer-{T.DatabaseName}-{T.CollectionName}";
        return key;
    }
}