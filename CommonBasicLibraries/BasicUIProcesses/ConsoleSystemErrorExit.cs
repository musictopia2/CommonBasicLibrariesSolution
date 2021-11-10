namespace CommonBasicLibraries.BasicUIProcesses;
public class ConsoleSystemErrorExit : ISystemError //because this will also exit however it does it.
{
    //if you are using this, then you are using di so di has to figure out the iexit interface
    private readonly IExit _exit;
    public ConsoleSystemErrorExit(IExit exit)
    {
        _exit = exit;
    }
    void ISystemError.ShowSystemError(string message)
    {
        Console.WriteLine($"There was an error.  The message was {message}");
        _exit.ExitApp();
    }
}