namespace CommonBasicLibraries.DatabaseHelpers.AbstractStaticInterfaces;
#if NET7_0_OR_GREATER
public interface ISqlDocumentConfiguration : ISqlDatabaseConfiguration
{
    abstract static string CollectionName { get; }
}
#endif