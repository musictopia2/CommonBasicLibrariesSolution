namespace CommonBasicLibraries.BasicUIProcesses;
public interface IMessageBox
{
    Task ShowMessageAsync(string message);
}