namespace CommonBasicLibraries.DatabaseHelpers.AbstractStaticInterfaces;
public interface ISqlDatabaseConfiguration
{
    abstract static string DatabaseName { get; }
}