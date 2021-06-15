using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonBasicLibraries.BasicDataSettingsAndProcesses
{
    public interface IDatePicker
    {
        DateTime GetCurrentDate { get; } //allows mocking.
    }
}
