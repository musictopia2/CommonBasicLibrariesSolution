namespace CommonBasicLibraries.BasicUIProcesses;
public class ConsoleExit : IExit
{
    void IExit.ExitApp()
    {
        Console.WriteLine("Closing");
    }
}