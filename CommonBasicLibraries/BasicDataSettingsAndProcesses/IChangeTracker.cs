namespace CommonBasicLibraries.BasicDataSettingsAndProcesses;
//hopefully i never need anything related to this.
public interface IChangeTracker
{
    //decided that this can go beyond just databases.  used in any case where you want to do change trackings
    void Initialize(); //if i initialize before reaching client, then it would have to send the dictionary to the client.
    BasicList<string> GetChanges(); //only needs to know the fields that changed.
}