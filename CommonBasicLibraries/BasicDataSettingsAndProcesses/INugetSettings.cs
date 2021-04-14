using CommonBasicLibraries.CollectionClasses; //i really want people to use my new custom class for the lists
using System.Threading.Tasks;
namespace CommonBasicLibraries.BasicDataSettingsAndProcesses
{
    //was going to do separate.  However, this part can be here.  so i can have one library to implement this as well.
    public interface INugetSettings
    {
        /// <summary>
        /// Need to provide a list of the paths for the directories you are using with this system.  will be a function
        /// </summary>
        /// <returns></returns>
        Task<BasicList<string>> GetProjectListsAsync();
        Task<string> GetDataPathAsync(); //this is where you will store all the files needed for the system to work.
        Task<string> GetKeyAsync(); //you need to provide the key which is used to upload packages.
        //decided to make it clear its async.  the good thing about this interface is if i decided to get the keys via keyvault, should still work.
    }
}