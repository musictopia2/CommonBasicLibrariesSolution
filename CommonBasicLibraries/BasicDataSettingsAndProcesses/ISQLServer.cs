namespace CommonBasicLibraries.BasicDataSettingsAndProcesses
{
    public interface ISQLServer
    {
        string GetConnectionString(string databaseOrPath); //this way if implemented, then you can easily get the sql server connection. 
    }
}