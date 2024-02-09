namespace CommonBasicLibraries.DatabaseHelpers.AbstractStaticInterfaces;
#if NET7_0_OR_GREATER
public interface ISqlDocumentConfiguration
{
    abstract static string DatabaseName { get; }
    abstract static string CollectionName { get; }
}
#endif