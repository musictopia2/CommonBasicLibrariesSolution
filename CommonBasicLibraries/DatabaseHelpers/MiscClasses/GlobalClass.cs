namespace CommonBasicLibraries.DatabaseHelpers.MiscClasses;
public static class GlobalClass
{
    public static ISQLiteConnector? SQLiteConnector { get; set; }
    public static ISQLServerConnector? SQLServerConnector { get; set; }
    public static IMySQLConnector? MySQLConnector { get; set; }
    public static Action<string>? CreateSqliteDatabase { get; set; }
}