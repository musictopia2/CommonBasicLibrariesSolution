using CommonBasicLibraries.DatabaseHelpers.SourceGeneratorHelpers; //not common enough
namespace CommonBasicLibraries.CrudRepositoryHelpers;
//i think in the general helpers library is fine.
public interface ISQLRepositoryFactory
{
    object CreateSQLRepository<TEntity>(string connectionString, IDatabaseConnectionManager manager)
        where TEntity : class, ISimpleDatabaseEntity, ICommandQuery<TEntity>, ITableMapper<TEntity>;
}