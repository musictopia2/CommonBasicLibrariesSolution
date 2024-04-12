namespace CommonBasicLibraries.DatabaseHelpers.MiscInterfaces;
public interface ICaptureCommandParameter
{
    //needs to be public so i can use extensions that use it.
    //needs to be on the common functions library so the music core can use this as well.

    DbParameter GetParameter();
    IDbCommand GetCommand();
    EnumDatabaseCategory Category { get; }
    IDbConnection CurrentConnection { get; }
}