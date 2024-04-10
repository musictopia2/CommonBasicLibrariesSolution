namespace CommonBasicLibraries.DatabaseHelpers.SourceGeneratorHelpers;
public interface ITableMapper<E>
    where E : class, ISimpleDatabaseEntity
{
    //if i need extra information, then provide it.
    abstract static string TableName { get; }
    abstract static string GetForeignKey(string name); //if it returns blank, then means no foreign key.
    abstract static bool HasForeignKey(string name);
    abstract static SourceGeneratedMap GetTableMap(E payLoad, bool isAutoIncremented = true, bool beingJoined = false);
    abstract static SourceGeneratedMap GetTableMap(bool beingJoined = false);
    abstract static bool IsAutoIncremented { get; } //the source generator will figure this out.
}