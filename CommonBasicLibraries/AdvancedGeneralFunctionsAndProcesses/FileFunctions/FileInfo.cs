namespace CommonBasicLibraries.AdvancedGeneralFunctionsAndProcesses.FileFunctions
{
    public class FileInfo
    {
        //decided to keep time because it can be important of when it was accessed in time (for possible autoupdate processes)
        public DateTime DateCreated { get; set; }
        public long FileSize { get; set; }
        public DateTime DateModified { get; set; }
        public DateTime DateAccessed { get; set; } // there are some cases where the last time the file was accessed was the most important.  especially when the file can come from git hub.
        public string Directory { get; set; } = "";
        public string FilePath { get; set; } = "";
    }
}