using System.ComponentModel.DataAnnotations; //not common enough.
namespace CommonBasicLibraries.AdvancedGeneralFunctionsAndProcesses.TemplateHelpers;
public class CreateTemplateViewModel(ITemplateCreater templateCreater, IExit exit) : ICreateTemplateViewModel
{
    [Required]
    public string AppName { get; set; } = "";
    [Required]
    public string PathDestination { get; set; } = "";
    public async Task SaveAsync()
    {
        await templateCreater.CreateTemplateAsync(AppName!, PathDestination!);
        exit.ExitApp(); //has to be all or nothing now.
    }
}