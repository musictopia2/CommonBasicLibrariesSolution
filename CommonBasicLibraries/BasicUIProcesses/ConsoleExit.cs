namespace CommonBasicLibraries.BasicUIProcesses;
/// <summary>
/// A console-safe implementation of <see cref="IExit"/> for non-GUI or test environments.
/// </summary>
/// <remarks>
/// When <see cref="IExit.ExitApp"/> is called, this implementation simply writes "Closing"
/// to the console instead of actually terminating the application.
/// 
/// Suitable for use in testing scenarios or environments where UI-based application closing is not available.
/// </remarks>
/// <example>
/// Example registration:
/// <code>
/// services.AddSingleton<IExit, ConsoleExit>();
/// </code>
/// </example>
public class ConsoleExit : IExit
{
    void IExit.ExitApp()
    {
        Console.WriteLine("Closing");
    }
}