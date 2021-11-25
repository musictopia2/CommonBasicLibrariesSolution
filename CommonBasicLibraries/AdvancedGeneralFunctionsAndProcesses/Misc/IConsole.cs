namespace CommonBasicLibraries.AdvancedGeneralFunctionsAndProcesses.Misc;
/// <summary>
/// This is needed for logging.  its like logging
/// except this is intended to use not for storing to files.
/// but to show on some screen.
/// can write to console.
/// but can also write to unit test output
/// so information can be gathered to get hints of things.
/// </summary>
public interface IConsole
{
    bool ExtraSpaces { get; } //test requires special things.
    void WriteLine(object thisObject);
}