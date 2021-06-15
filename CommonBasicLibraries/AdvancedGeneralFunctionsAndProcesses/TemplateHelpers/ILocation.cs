namespace CommonBasicLibraries.AdvancedGeneralFunctionsAndProcesses.TemplateHelpers
{
    //go ahead and still have this interface.  since 2 libraries require it.  this will simply this part.
    public interface ILocation
    {
        string TemplateFrom { get; }
        string TemplateName { get; }
    }
}