namespace CommonBasicLibraries.DatabaseHelpers.MiscInterfaces;
/// <summary>
/// this is a helper that all it does is decides what connection to return.
/// the purpose of this is so database helpers don't need to rely on sql server or sqlite.
/// i think that most likely will have 2 classes in 2 libraries so i don't have to have references to both sql server and sqlite"
/// di can put in the proper one.
/// </summary>
public interface IDbConnector
{
    IDbConnection GetConnection(EnumDatabaseCategory category, string connectionString); //by this time, it should have already gotten the connection string
    //this is like a custom factory pattern.
    EnumDatabaseCategory GetCategory(IDbConnection connection); //this way i don't need a reference to sql lite or sql server in my helpers.
    IDbCommand GetCommand(); //take case by case what is needed.
    DbParameter GetParameter();
}