#if NET7_0_OR_GREATER
namespace CommonBasicLibraries.DatabaseHelpers.AbstractStaticInterfaces;

public interface ISqlitePathConfiguration
{
    abstract static string Path { get; } //this will help for source generators so the source generator can create the proper database.
    //obviously if i have something in iconfiguration, will use that instead.
    //but if not that, then use this one.
}
#endif