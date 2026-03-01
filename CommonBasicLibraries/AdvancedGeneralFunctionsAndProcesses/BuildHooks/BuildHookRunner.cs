namespace CommonBasicLibraries.AdvancedGeneralFunctionsAndProcesses.BuildHooks;
public static class BuildHookRunner
{
    public static async Task RunAsync(
        string[] args,
        Func<BuildHookArgs3, Task> action)
    {
        try
        {
            ArgumentValidator.ValidateArguments(args, 3);
            var model = new BuildHookArgs3(args[0], args[1], args[2]);
            await action(model);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
            Environment.Exit(1);
        }
    }


    public static async Task RunAsync(
        string[] args,
        Func<BuildHookArgs4, Task> action)
    {
        try
        {
            ArgumentValidator.ValidateArguments(args, 3);
            var model = new BuildHookArgs4(args[0], args[1], args[2], args[3]);
            await action(model);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
            Environment.Exit(1);
        }
    }

}