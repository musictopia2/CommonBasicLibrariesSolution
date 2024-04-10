namespace CommonBasicLibraries.DatabaseHelpers.AbstractStaticInterfaces;
public interface ISqlDocumentConfiguration
{
    abstract static string DatabaseName { get; }
    abstract static string CollectionName { get; }
}