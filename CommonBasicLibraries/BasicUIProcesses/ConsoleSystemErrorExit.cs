namespace CommonBasicLibraries.BasicUIProcesses;
public class ConsoleSystemErrorExit(IExit exit) : ISystemError //because this will also exit however it does it.
{
    void ISystemError.ShowSystemError(string message)
    {
        Console.WriteLine($"There was an error.  The message was {message}");
        exit.ExitApp();
    }
}