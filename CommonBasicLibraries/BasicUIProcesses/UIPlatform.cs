namespace CommonBasicLibraries.BasicUIProcesses
{
    public static class UIPlatform
    {
        //there are lots of stuff not needed anymore.
        //may not be needed because i already have the execute version.
        //go ahead and keep.  hopefully will just work when i do blazor server side
        public static IUIThread CurrentThread { get; set; } = new DefaultThread(); //if you don't specify, you will get defaultthread

        //trying without the other delegates.  unfortunately, there is nothing i can do about it now.


        //public static Action ExitApp { get; set; } = () => { Console.WriteLine("Closing"); };
        //public static Action<string> ShowSystemError { get; set; } = (message) =>
        //{
        //    Console.WriteLine($"There was an error.  The message was {message}");
        //    ExitApp();
        //};
        //public static Func<string, Task> ShowMessageAsync { get; set; } = (message) =>
        //{
        //    Console.WriteLine(message);
        //    return Task.CompletedTask;
        //};
        //public static Action<string> ShowWarningToast { get; set; } = (message) =>
        //{
        //    Console.WriteLine($"Warning { message}");
        //};

        //public static Action<string> ShowSuccessToast { get; set; } = (message) =>
        //{
        //    Console.WriteLine($"Success { message}");
        //};

        //public static Action<string> ShowInfoToast { get; set; } = (message) =>
        //{
        //    Console.WriteLine($"Info { message}");
        //};
        //public static Action<string> ShowUserErrorToast { get; set; } = (message) =>
        //{
        //    Console.WriteLine($"Error { message}");
        //};
        //this only works on desktop
        //still needed for possible music validations but can be others as well.
        public static Action<string> DesktopValidationError { get; set; } = (message) =>
        {
            Console.WriteLine(message);
        };
    }
}