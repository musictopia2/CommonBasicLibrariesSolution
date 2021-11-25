namespace CommonBasicLibraries.DatabaseHelpers.MiscClasses;
public static class GlobalClass
{
    //this is okay because even if single class its okay because no conflicts
    public static ISQLiteConnector? SQLiteConnector { get; set; }
    public static ISQLServerConnector? SQLServerConnector { get; set; }
    public static IMySQLConnector? MySQLConnector { get; set; }
}