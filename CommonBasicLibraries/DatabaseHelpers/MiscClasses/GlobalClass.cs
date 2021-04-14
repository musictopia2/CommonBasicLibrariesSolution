using CommonBasicLibraries.DatabaseHelpers.MiscInterfaces;
namespace CommonBasicLibraries.DatabaseHelpers.MiscClasses
{
    public static class GlobalClass
    {
        public static ISQLiteConnector? SQLiteConnector { get; set; }
        public static ISQLServerConnector? SQLServerConnector { get; set; }
        public static IMySQLConnector? MySQLConnector { get; set; }
        //most likely i have to create a manuel instance and put to here.
        //dapper helpers needs this.
    }
}