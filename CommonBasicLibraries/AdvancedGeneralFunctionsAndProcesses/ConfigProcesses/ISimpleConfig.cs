using System.Threading.Tasks;
namespace CommonBasicLibraries.AdvancedGeneralFunctionsAndProcesses.ConfigProcesses
{
    public interface ISimpleConfig
    {
        public Task<string> GetStringAsync(string key); //decided to make this async.  hopefully i won't regret this decision.
    }
}