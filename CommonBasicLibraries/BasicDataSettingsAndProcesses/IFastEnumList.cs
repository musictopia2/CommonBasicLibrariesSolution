namespace CommonBasicLibraries.BasicDataSettingsAndProcesses;
public interface IFastEnumList<A> : IFastEnumSimple
    where A : struct, IFastEnumList<A>
{
    BasicList<A> CompleteList { get; }
}