namespace CommonBasicLibraries.DatabaseHelpers.MiscClasses
{
    public enum EnumDatabaseCategory
    {
        None, //decided to also include none as well.  sometimes a process needs it which means none was chosen for a process.
        SQLServer,
        SQLite,
        MySQL,
        Future //if its future, then it means i need some rethinking.
    }
}