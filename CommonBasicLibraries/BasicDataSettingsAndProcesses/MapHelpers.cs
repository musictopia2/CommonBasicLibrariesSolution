namespace CommonBasicLibraries.BasicDataSettingsAndProcesses;
public static class MapHelpers<T>
{
    internal static Dictionary<Type, Func<IMappable, T>> Maps = new();
#if NET6_0_OR_GREATER
    /// <summary>
    /// source generators will call this function and run the source generated code.  which will remove reflection completedly in this situation.
    /// </summary>
    /// <param name="source"></param>
    /// <param name="func"></param>
    public static void AddMap(Type source, Func<IMappable, T> func)
    {
        Maps.TryAdd(source, func);
    }
#endif  
}