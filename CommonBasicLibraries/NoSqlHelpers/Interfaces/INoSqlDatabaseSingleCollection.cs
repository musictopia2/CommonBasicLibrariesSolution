namespace CommonBasicLibraries.NoSqlHelpers.Interfaces;
public interface INoSqlDatabaseSingleCollection<T> : INoSqlDatabaseName
{
    //at the minimums, you need a database name if you are doing a single collection.
    //T will not be the mongo model because the source generator will create the mongo model.
    //on the other hand, if you decided to directly do the mongo models, then that should be an option as well
    string CollectionName { get; }
}