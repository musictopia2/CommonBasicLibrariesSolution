namespace CommonBasicLibraries.NugetHelpers;
public interface INugetProjectData
{
    Task<BasicList<VisualStudioProjectModel>> GetProjectListAsync();
    Task SaveProjectListAsync(BasicList<VisualStudioProjectModel> list); //to edit, would just do the edits to the list i got.  then save as i go (just like before).
}