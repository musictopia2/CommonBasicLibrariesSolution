#if NET7_0_OR_GREATER
namespace CommonBasicLibraries.DatabaseHelpers.AbstractStaticInterfaces;
public interface ISqlDatabaseConfiguration
{
    abstract static string DatabaseName { get; }
}
#endif