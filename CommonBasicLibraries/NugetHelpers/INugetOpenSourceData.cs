namespace CommonBasicLibraries.NugetHelpers;
public interface INugetOpenSourceData
{
    Task<BasicList<string>> GetOpenSourceListAsync();
    Task SaveOpenSourceList(BasicList<string> openSourceList); //if i am going to add one, then would simply get the list.  manually add, then save.
    //this opens the possibility of putting in database for some implementation.
}