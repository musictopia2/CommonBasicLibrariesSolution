namespace CommonBasicLibraries.AdvancedGeneralFunctionsAndProcesses.UploadHelpers;

[AttributeUsage(AttributeTargets.Property)]
public class UploadFileAttribute : Attribute
{
    public bool IsRequired { get; set; } = true;
}