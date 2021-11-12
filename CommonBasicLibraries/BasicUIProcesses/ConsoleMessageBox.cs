namespace CommonBasicLibraries.BasicUIProcesses;
public class ConsoleMessageBox : IMessageBox
{
    Task IMessageBox.ShowMessageAsync(string message)
    {
        Console.WriteLine(message);
        return Task.CompletedTask;
    }
}