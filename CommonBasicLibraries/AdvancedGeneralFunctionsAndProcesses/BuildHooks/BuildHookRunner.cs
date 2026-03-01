namespace CommonBasicLibraries.AdvancedGeneralFunctionsAndProcesses.BuildHooks;
public static class BuildHookRunner
{
    // Most tools: allow either 3 or 4 args.
    public static Task RunAsync(string[] args, Func<BuildHookArgs, Task> action)
        => RunCoreAsync(
            args,
            validate: a => ValidateArgCount(a, allow3: true, allow4: true),
            factory: a => new BuildHookArgs(a[0], a[1], a[2], a.Length >= 4 ? a[3] : null),
            action: action);

    // Tools that MUST have output folder (docker/updater).
    public static Task RunWithOutputAsync(string[] args, Func<BuildHookArgs, Task> action)
        => RunCoreAsync(
            args,
            validate: a => ArgumentValidator.ValidateArguments(a, 4),
            factory: a => new BuildHookArgs(a[0], a[1], a[2], a[3]),
            action: action);

    private static async Task RunCoreAsync<TArgs>(
        string[] args,
        Action<string[]> validate,
        Func<string[], TArgs> factory,
        Func<TArgs, Task> action)
    {
        try
        {
            validate(args);
            var model = factory(args);
            await action(model);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
            Environment.Exit(1);
        }
    }

    private static void ValidateArgCount(string[] args, bool allow3, bool allow4)
    {
        ArgumentNullException.ThrowIfNull(args);

        if ((allow3 && args.Length == 3) || (allow4 && args.Length == 4))
        {
            return;
        }

        if (allow3 && allow4)
        {
            throw new ArgumentException("Expected 3 or 4 arguments.");
        }

        if (allow3)
        {
            throw new ArgumentException("Expected 3 arguments.");
        }

        if (allow4)
        {
            throw new ArgumentException("Expected 4 arguments.");
        }

        throw new ArgumentException("Invalid argument count.");
    }

}