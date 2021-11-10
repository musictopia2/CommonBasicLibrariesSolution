using CommonBasicLibraries.AdvancedGeneralFunctionsAndProcesses.BasicExtensions;
namespace CommonBasicLibraries.BasicDateTimeProcesses;
public class RealDatePicker : IDatePicker, IDateOnlyPicker
{
    DateTime IDatePicker.GetCurrentDate => DateTime.Now;
    //maybe tim showed how to convert.  however, my works too though.
    DateOnly IDateOnlyPicker.GetCurrentDate => DateTime.Now.ToDateOnly();
}