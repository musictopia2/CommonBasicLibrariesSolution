namespace CommonBasicLibraries.BasicDataSettingsAndProcesses
{
    public interface INugetSettings
    {
        string OpenSourcePath(); //useful for monitoring processes.
        //was going to remove this and do something else.  decided against it after all.  does mean the main data has to be a file.  needs file so it can monitor anyways.
        string GetDataPath(); //this is where you will store all the files needed for the system to work.  decided no need to async this anyways now.
        Task<string> GetKeyAsync(); //you need to provide the key which is used to upload packages.
        //decided to make it clear its async.  the good thing about this interface is if i decided to get the keys via keyvault, should still work.
    }
}