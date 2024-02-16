#if NET7_0_OR_GREATER
namespace CommonBasicLibraries.DatabaseHelpers.AbstractStaticInterfaces;
public interface ISqlDocumentConfiguration
{
    abstract static string DatabaseName { get; }
    abstract static string CollectionName { get; }
}
#endif