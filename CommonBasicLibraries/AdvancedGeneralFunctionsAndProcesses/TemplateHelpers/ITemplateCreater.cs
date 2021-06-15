using System.Threading.Tasks;
namespace CommonBasicLibraries.AdvancedGeneralFunctionsAndProcesses.TemplateHelpers
{
    public interface ITemplateCreater
    {
        Task CreateTemplateAsync(string newName, string newLocation);
    }
}