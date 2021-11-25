namespace CommonBasicLibraries.AdvancedGeneralFunctionsAndProcesses.TemplateHelpers;
public interface ITemplateCreater
{
    Task CreateTemplateAsync(string newName, string newLocation);
}