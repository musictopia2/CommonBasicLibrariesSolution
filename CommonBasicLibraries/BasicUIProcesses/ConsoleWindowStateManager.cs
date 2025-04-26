namespace CommonBasicLibraries.BasicUIProcesses;
/// <summary>
/// A console-friendly implementation of <see cref="IWindowStateManager"/> for non-GUI or test environments.
/// </summary>
/// <remarks>
/// This class does not interact with any real windowing system. Instead, it simulates
/// minimize and restore actions using internal flags and outputs messages to the console.
/// 
/// It is useful for running logic in environments like unit tests or console apps
/// without requiring a UI framework.
/// </remarks>
/// <example>
/// Example registration for dependency injection:
/// <code>
/// services.AddSingleton<IWindowStateManager, ConsoleWindowStateManager>();
/// </code>
/// </example>
public class ConsoleWindowStateManager : IWindowStateManager
{
    private bool _isMinimized;
    bool IWindowStateManager.IsMinimized => _isMinimized;
    void IWindowStateManager.MinimizeWindow()
    {
        _isMinimized = true;
        Console.WriteLine("Minimizing fake window");
    }
    void IWindowStateManager.RestoreWindow()
    {
        _isMinimized = false;
        Console.WriteLine("Restoring fake window");
    }
}