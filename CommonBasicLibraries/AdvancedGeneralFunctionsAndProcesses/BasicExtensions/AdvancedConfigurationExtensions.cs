namespace CommonBasicLibraries.AdvancedGeneralFunctionsAndProcesses.BasicExtensions;
public static class AdvancedConfigurationExtensions
{
    extension<T>(IAdvancedConfiguration configuration)
        where T: ISqlDocumentConfiguration
    {
        public IAdvancedConfiguration AddDocumentLocalDB()
        {
            string key = GetDocumentSqlServerKey<T>();
            string value = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=DocumentDatabase;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            return configuration.Add(key, value);
        }
        public IAdvancedConfiguration AddSqliteDocument(string path)
        {
            string key = $"DocumentDatabaseSqlite-{T.DatabaseName}-{T.CollectionName}";
            return configuration.Add(key, path);
        }
    }

    extension<T>(IAdvancedConfiguration configuration)
        where T: ISqlDatabaseConfiguration
    {
        public IAdvancedConfiguration AddLocalDB()
        {
            string key = GetDatabaseKey<T>();
            string value = $@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog={T.DatabaseName};Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            return configuration.Add(key, value);
        }

        public IAdvancedConfiguration AddSqliteStandardPath(string path)
        {
            string key = $"{T.DatabaseName}Path";
            return configuration.Add(key, path);
        }
    }

   
    //for now, looks like i still am forced to do older way.
    //if i do the new extension way, then has to know what type to use in code unfortunately.
    //attempt to do both
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