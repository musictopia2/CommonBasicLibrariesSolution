namespace CommonBasicLibraries.AdvancedGeneralFunctionsAndProcesses.SendEmailClasses;
public interface ISmptService
{
    Task<SmptInfo> GetSmptInfoAsync();
}