namespace CommonBasicLibraries.MediaHelpers.Models;
public class SkipSceneClass : IComparable<SkipSceneClass>
{
    public int StartTime { get; set; }
    public int EndTime { get; set; }
    public int HowLong { get; set; }
    int IComparable<SkipSceneClass>.CompareTo(SkipSceneClass? other)
    {
        return StartTime.CompareTo(other!.StartTime);
    }
}