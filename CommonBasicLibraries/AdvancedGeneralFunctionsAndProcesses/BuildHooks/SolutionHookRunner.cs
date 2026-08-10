namespace CommonBasicLibraries.AdvancedGeneralFunctionsAndProcesses.BuildHooks;

public static class SolutionHookRunner
{
    public static Task RunAsync(string[] args, Func<SolutionHookArgs, Task> action)
        => RunCoreAsync(
            args,
            validate: a => ArgumentValidator.ValidateArguments(a, 2),
            factory: a =>
            {
                string solutionFileName = a[0];
                string solutionDir = a[1];
                return new SolutionHookArgs(solutionFileName, solutionDir);
            },
            action: action);





    private static async Task RunCoreAsync<TArgs>(
        string[] args,
        Action<string[]> validate,
        Func<string[], TArgs> factory,
        Func<TArgs, Task> action)
    {
        validate(args);
        TArgs hookArgs = factory(args);
        await action(hookArgs).ConfigureAwait(false);
    }



    public static Task RunAsync<TCustom>(
    string[] args,
    Func<string[], TCustom> customFactory,
    Func<SolutionHookArgs, TCustom, Task> action)
    => RunCoreAsync(
        args,
        validate: a => ArgumentValidator.ValidateMinimumArguments(a, 2),
        factory: a =>
        {
            string solutionFileName = a[0];
            string solutionDir = a[1];

            SolutionHookArgs solutionArgs =
                new(solutionFileName, solutionDir);

            TCustom customArgs = customFactory(a[2..]);

            return (solutionArgs, customArgs);
        },
        action: x => action(x.solutionArgs, x.customArgs));




}
