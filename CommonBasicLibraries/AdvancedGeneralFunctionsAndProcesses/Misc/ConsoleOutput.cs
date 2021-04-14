using System;
namespace CommonBasicLibraries.AdvancedGeneralFunctionsAndProcesses.Misc
{
    public class ConsoleOutput : IConsole
    {
        bool IConsole.ExtraSpaces => false;

        void IConsole.WriteLine(object thisObject)
        {
            Console.WriteLine(thisObject.ToString());
        }
    }
}