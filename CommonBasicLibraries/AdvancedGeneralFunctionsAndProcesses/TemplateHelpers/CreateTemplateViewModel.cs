using System.ComponentModel.DataAnnotations; //not common enough.
namespace CommonBasicLibraries.AdvancedGeneralFunctionsAndProcesses.TemplateHelpers
{
    public class CreateTemplateViewModel : ICreateTemplateViewModel
    {
        [Required]
        public string AppName { get; set; } = "";
        [Required]
        public string PathDestination { get; set; } = "";
        public async Task SaveAsync()
        {
            await _templateCreater.CreateTemplateAsync(AppName!, PathDestination!);
            UIPlatform.ExitApp();
        }
        private readonly ITemplateCreater _templateCreater;
        public CreateTemplateViewModel(ITemplateCreater templateCreater)
        {
            _templateCreater = templateCreater;
        }
    }
}