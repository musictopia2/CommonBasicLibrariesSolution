namespace CommonBasicLibraries.DatabaseHelpers.EntityInterfaces;
//if i ever decided to support 4 tables, then just add another generic argument.
public interface IJoinedEntity<D1> : ISimpleEntity //hopefully i don't need more than 3 joined.  if so, rethink
    where D1: class, ISimpleEntity
{
    void AddRelationships(D1 entity);
}
public interface IJoinEntity<D1, D2> : ISimpleEntity
    where D1: class, ISimpleEntity
    where D2: class, ISimpleEntity
{
    void AddRelationships(D1 firstEntity, D2 secondEntity);
}