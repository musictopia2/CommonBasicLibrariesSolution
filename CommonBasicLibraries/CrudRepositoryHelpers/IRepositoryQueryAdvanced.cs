namespace CommonBasicLibraries.CrudRepositoryHelpers;
public interface IRepositoryQueryAdvanced<TEntity> : IRepository<TEntity>
    where TEntity : class
{
    Task<IEnumerable<TEntity>> GetAsync(BasicList<ICondition> conditions,
        BasicList<SortInfo>? sortList = null,
        int top = 0);
}