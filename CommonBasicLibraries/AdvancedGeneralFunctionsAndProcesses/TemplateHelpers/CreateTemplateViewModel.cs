using System.ComponentModel.DataAnnotations; //not common enough.
namespace CommonBasicLibraries.AdvancedGeneralFunctionsAndProcesses.TemplateHelpers;
public class CreateTemplateViewModel : ICreateTemplateViewModel
{
    [Required]
    public string AppName { get; set; } = "";
    [Required]
    public string PathDestination { get; set; } = "";
    public async Task SaveAsync()
    {
        await _templateCreater.CreateTemplateAsync(AppName!, PathDestination!);
        _exit.ExitApp(); //has to be all or nothing now.
    }
    private readonly ITemplateCreater _templateCreater;
    private readonly IExit _exit;
    public CreateTemplateViewModel(ITemplateCreater templateCreater, IExit exit)
    {
        _templateCreater = templateCreater;
        _exit = exit;
    }
}