namespace CommonBasicLibraries.BasicUIProcesses;
public interface IWindowStateManager
{
    void MinimizeWindow();
    void RestoreWindow();
    bool IsMinimized { get; }
}