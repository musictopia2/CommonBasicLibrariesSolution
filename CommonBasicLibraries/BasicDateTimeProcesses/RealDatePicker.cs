namespace CommonBasicLibraries.BasicDateTimeProcesses;
#if NET6_0_OR_GREATER
public class RealDatePicker : IDatePicker, IDateOnlyPicker
{
    DateTime IDatePicker.GetCurrentDate => DateTime.Now;
    DateOnly IDateOnlyPicker.GetCurrentDate => DateTime.Now.ToDateOnly();
}
#endif
#if NETSTANDARD2_0
public class RealDatePicker : IDatePicker
{
    DateTime IDatePicker.GetCurrentDate => DateTime.Now;
}
#endif