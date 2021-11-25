namespace CommonBasicLibraries.BasicDateTimeProcesses;
public class RealDatePicker : IDatePicker, IDateOnlyPicker
{
    DateTime IDatePicker.GetCurrentDate => DateTime.Now;
    DateOnly IDateOnlyPicker.GetCurrentDate => DateTime.Now.ToDateOnly();
}