namespace CommonBasicLibraries.AdvancedGeneralFunctionsAndProcesses.BuildHooks;
public static class BuildHookRunner
{
    // Most tools: allow either 3 or 4 args.
    private static string? DeriveProjectName(string? projectName, string projectFileName)
    {
        if (string.IsNullOrWhiteSpace(projectName) == false)
        {
            return projectName;
        }

        // If VS didn't pass ProjectName, derive from the file name: MyLib.csproj -> MyLib
        if (string.IsNullOrWhiteSpace(projectFileName))
        {
            return projectName; // nothing we can do
        }

        return Path.GetFileNameWithoutExtension(projectFileName);
    }

    public static Task RunAsync(string[] args, Func<BuildHookArgs, Task> action)
    => RunCoreAsync(
        args,
        validate: a => ValidateArgCount(a, allow3: true, allow4: true),
        factory: a =>
        {
            string projectName = a[0];
            string projectDir = a[1];
            string projectFileName = a[2];
            string? outputDir = a.Length >= 4 ? a[3] : null;

            projectName = DeriveProjectName(projectName, projectFileName) ?? "";

            return new BuildHookArgs(projectName, projectDir, projectFileName, outputDir);
        },
        action: action);

    public static Task RunWithOutputAsync(string[] args, Func<BuildHookArgs, Task> action)
        => RunCoreAsync(
            args,
            validate: a => ArgumentValidator.ValidateArguments(a, 4),
            factory: a =>
            {
                string projectName = a[0];
                string projectDir = a[1];
                string projectFileName = a[2];
                string outputDir = a[3];

                projectName = DeriveProjectName(projectName, projectFileName) ?? "";

                return new BuildHookArgs(projectName, projectDir, projectFileName, outputDir);
            },
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