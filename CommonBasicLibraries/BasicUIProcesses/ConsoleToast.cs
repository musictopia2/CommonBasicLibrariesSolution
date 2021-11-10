namespace CommonBasicLibraries.BasicUIProcesses;
public class ConsoleToast : IToast
{
    void IToast.ShowUserErrorToast(string message)
    {
        Console.WriteLine($"Error { message}");
    }
    void IToast.ShowSuccessToast(string message)
    {
        Console.WriteLine($"Success { message}");
    }
    void IToast.ShowWarningToast(string message)
    {
        Console.WriteLine($"Warning { message}");
    }
    void IToast.ShowInfoToast(string message)
    {
        Console.WriteLine($"Info { message}");
    }
}