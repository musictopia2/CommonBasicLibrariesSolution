namespace CommonBasicLibraries.AdvancedGeneralFunctionsAndProcesses.BuildHooks;
//this will go into the common functions library eventually.
public static class ArgumentValidator
{
    public static void ValidateArguments(string[] args, int expectedCount)
    {
        if (args.Length == 0)
        {
            Console.WriteLine("No arguments provided.");
            Environment.Exit(1); // Exit with failure
        }

        if (args.Length != expectedCount)
        {
            Console.WriteLine($"Expected {expectedCount} arguments, but got {args.Length}.");
            Environment.Exit(1); // Exit with failure
        }
    }
    public static void ValidateMinimumArguments(
    string[] args,
    int minimumCount)
    {
        if (args.Length == 0)
        {
            Console.WriteLine("No arguments provided.");
            Environment.Exit(1); // Exit with failure
        }
        if (args.Length < minimumCount)
        {
            Console.WriteLine(
                $"Expected at least {minimumCount} arguments, but got {args.Length}.");

            Environment.Exit(1);
        }
    }
}