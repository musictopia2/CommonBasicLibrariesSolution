namespace CommonBasicLibraries.DatabaseHelpers.AbstractStaticInterfaces;
#if NET7_0_OR_GREATER
public interface ISqlDatabaseConfiguration
{
    abstract static string DatabaseName { get; }
}
#endif