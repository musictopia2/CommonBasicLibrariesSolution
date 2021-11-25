namespace CommonBasicLibraries.AdvancedGeneralFunctionsAndProcesses.TemplateHelpers;
public interface ICreateTemplateViewModel
{
    string AppName { get; set; }
    string PathDestination { get; set; }
    Task SaveAsync();
}