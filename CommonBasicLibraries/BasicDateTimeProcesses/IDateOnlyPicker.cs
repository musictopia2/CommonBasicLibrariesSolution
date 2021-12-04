#if NET6_0_OR_GREATER
namespace CommonBasicLibraries.BasicDateTimeProcesses;
public interface IDateOnlyPicker
{
    DateOnly GetCurrentDate { get; }
}
#endif