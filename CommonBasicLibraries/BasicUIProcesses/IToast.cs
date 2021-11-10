namespace CommonBasicLibraries.BasicUIProcesses;
public interface IToast
{
    void ShowInfoToast(string message);
    void ShowWarningToast(string message);
    void ShowUserErrorToast(string message);
    void ShowSuccessToast(string message);
}