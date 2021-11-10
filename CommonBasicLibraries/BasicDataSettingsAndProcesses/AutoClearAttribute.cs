namespace CommonBasicLibraries.BasicDataSettingsAndProcesses //may include some from dapperhelpers (?)  basic enough to include here now.
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class AutoClearAttribute : Attribute { }
}