namespace CommonBasicLibraries.CrudRepositoryHelpers;
public interface IRepository<TEntity>
    where TEntity : class
{
    //this is what i propose for now (not sure what will change)
    Task<TEntity> GetByIdAsync(object id);
    Task<IEnumerable<TEntity>> GetAsync(QueryFilter<TEntity> filter);
    Task<bool> DeleteAsync(TEntity EntityToDelete);
    Task<bool> DeleteByIdAsync(object Id);
    Task DeleteAllAsync(); // Be Careful!!!
    Task<IEnumerable<TEntity>> GetAllAsync();
    Task<TEntity> InsertAsync(TEntity Entity);
    Task<TEntity> UpdateAsync(TEntity EntityToUpdate);
}