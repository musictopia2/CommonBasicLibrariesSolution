namespace CommonBasicLibraries.BasicUIProcesses;
public static class UIPlatform
{
    public static IUIThread CurrentThread { get; set; } = new DefaultThread(); //if you don't specify, you will get defaultthread
    public static Action<string> DesktopValidationError { get; set; } = Console.WriteLine;
}