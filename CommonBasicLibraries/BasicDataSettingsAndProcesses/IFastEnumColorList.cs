namespace CommonBasicLibraries.BasicDataSettingsAndProcesses;
public interface IFastEnumColorList<A> : IFastEnumColorSimple, IFastEnumList<A>
    where A : struct, IFastEnumColorList<A>
{
    BasicList<A> ColorList { get; } //this will return the complete list off of that.  i guess filtering can happen on top of that.
}