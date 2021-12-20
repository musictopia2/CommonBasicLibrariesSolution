namespace CommonBasicLibraries.BasicDataSettingsAndProcesses;
public interface IFastEnumSimple
{
    int Value { get; }
    string Name { get; }
    string Words { get; } //i like the idea of words too.  this means i don't have to worry about performance problems when getting words.
    bool IsNull { get; }
}