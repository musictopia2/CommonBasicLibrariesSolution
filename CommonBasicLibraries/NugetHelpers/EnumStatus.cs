namespace CommonBasicLibraries.NugetHelpers;
public enum EnumStatus
{
    None, New, NeedCreate, NeedsToUpload, ChangeDetected //added eventually changedtected so if there is a watcher, then can watch for this and act accordingly.
}