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
}