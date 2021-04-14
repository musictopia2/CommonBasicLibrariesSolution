namespace CommonBasicLibraries.DatabaseHelpers.EntityInterfaces
{
    public interface IJoinedEntity : ISimpleDapperEntity //hopefully i don't need more than 3 joined.  if so, rethink
    {
        void AddRelationships<E>(E entity);
    }
    public interface IJoin3Entity<D1, D2> : ISimpleDapperEntity
    {
        void AddRelationships(D1 firstEntity, D2 secondEntity);
    }
}