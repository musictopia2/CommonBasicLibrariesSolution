using System;
namespace CommonBasicLibraries.BasicDataSettingsAndProcesses
{
    public class RealDatePicker : IDatePicker
    {
        DateTime IDatePicker.GetCurrentDate => DateTime.Now;
    }
}
