namespace CommonBasicLibraries.CrudRepositoryHelpers;
//has to be in the general helpers library (since the sql classes needs it).
public interface IDatabaseConnectionManager
{
    EnumDatabaseCategory PrepareDatabase();
}