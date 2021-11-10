namespace CommonBasicLibraries.BasicUIProcesses;
internal class ConsoleMessageBox : IMessageBox
{
    Task IMessageBox.ShowMessageAsync(string message)
    {
        Console.WriteLine(message);
        return Task.CompletedTask;
    }
}