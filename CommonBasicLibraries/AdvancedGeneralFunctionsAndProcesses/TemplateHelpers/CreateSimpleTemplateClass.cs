namespace CommonBasicLibraries.AdvancedGeneralFunctionsAndProcesses.TemplateHelpers
{
    public class CreateSimpleTemplateClass : ITemplateCreater
    {
        public static string TemplateName { get; set; } = "";
        Task ITemplateCreater.CreateTemplateAsync(string newName, string newLocation)
        {
            if (TemplateName == "")
            {
                throw new CustomBasicException("There must be a template name to copy from");
            }
            CreateCustomTemplateClass.CreateTemplate(TemplateName, newName, newLocation);
            return Task.CompletedTask;
        }
    }
}