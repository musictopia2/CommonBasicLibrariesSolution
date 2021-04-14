namespace CommonBasicLibraries.DatabaseHelpers.MiscClasses
{
    public enum EnumDatabaseCategory
    {
        None, //decided to also include none as well.  sometimes a process needs it which means none was chosen for a process.
        SQLServer,
        SQLite,
        MySQL, //this is brand new being added.  did not think about this ahead of time.
        Future //if its future, then it means i need some rethinking.
        //bad news is unable to figure out enough long term to see what other databases would be supported that uses the idbinterface.
        //if that ever happened, best is probably raise exception.
        //then add new category
        //plus
    }
}