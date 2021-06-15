﻿using CommonBasicLibraries.AdvancedGeneralFunctionsAndProcesses.UIHelpers;
using System;
using System.Threading.Tasks;
namespace CommonBasicLibraries.BasicDataSettingsAndProcesses
{
    public static class UIPlatform
    {
        //there are lots of stuff not needed anymore.

        //may not be needed because i already have the execute version.

        public static IUIThread CurrentThread { get; set; } = new DefaultThread(); //if you don't specify, you will get defaultthread

        public static Action ExitApp { get; set; } = () => { Console.WriteLine("Closing"); };
        public static Action<string> ShowSystemError { get; set; } = (message) =>
        {
            Console.WriteLine($"There was an error.  The message was {message}");
            ExitApp();
        };
        public static Func<string, Task> ShowMessageAsync { get; set; } = (message) =>
        {
            Console.WriteLine(message);
            return Task.CompletedTask;
        };
        //i like the idea of showing toast.  because its intended to show up for just a while.
        //for now, wpf blazor can do differently than the rest of the system.  obviously, if doing from console, will be console.writeline.

        public static Action<string> ShowWarningToast { get; set; } = (message) =>
        {
            Console.WriteLine($"Warning { message}");
        };

        public static Action<string> ShowSuccessToast { get; set; } = (message) =>
        {
            Console.WriteLine($"Success { message}");
        };

        public static Action<string> ShowInfoToast { get; set; } = (message) =>
        {
            Console.WriteLine($"Info { message}");
        };
        public static Action<string> ShowUserErrorToast { get; set; } = (message) =>
        {
            Console.WriteLine($"Error { message}");
        };
        //this only works on desktop
        //still needed for possible music validations but can be others as well.
        public static Action<string> DesktopValidationError { get; set; } = (message) =>
        {
            Console.WriteLine(message);
        };
    }
}