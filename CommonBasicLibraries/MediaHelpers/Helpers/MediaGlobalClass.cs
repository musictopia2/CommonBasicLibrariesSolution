namespace CommonBasicLibraries.MediaHelpers.Helpers;
public static class MediaGlobalClass
{
    public static bool IsTesting { get; set; } //this is for media stuff but does not care about specific media stuff like movies, music, etc.
    public static Action? ProcessFullScreen { get; set; } //some processes needs to set/run.  like youtube needing to run if any.
    public static string StartProgress = Extensions.StartProgress();
}