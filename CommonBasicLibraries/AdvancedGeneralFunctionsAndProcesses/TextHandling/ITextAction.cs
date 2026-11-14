namespace CommonBasicLibraries.AdvancedGeneralFunctionsAndProcesses.TextHandling;
public interface ITextAction
{
    bool CanHandle(string text);
    Task HandleAsync(string text);
}