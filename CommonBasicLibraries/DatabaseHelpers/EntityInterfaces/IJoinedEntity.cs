namespace CommonBasicLibraries.DatabaseHelpers.EntityInterfaces;
//if i ever decided to support 4 tables, then just add another generic argument.
public interface IJoinedEntity<D1> : ISimpleDatabaseEntity //hopefully i don't need more than 3 joined.  if so, rethink
    where D1: class, ISimpleDatabaseEntity
{
    void AddRelationships(D1 entity);
}
public interface IJoinEntity<D1, D2> : ISimpleDatabaseEntity
    where D1: class, ISimpleDatabaseEntity
    where D2: class, ISimpleDatabaseEntity
{
    void AddRelationships(D1 firstEntity, D2 secondEntity);
}