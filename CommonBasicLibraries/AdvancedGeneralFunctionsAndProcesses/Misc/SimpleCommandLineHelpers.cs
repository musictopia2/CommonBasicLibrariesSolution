namespace CommonBasicLibraries.AdvancedGeneralFunctionsAndProcesses.Misc;
public static class SimpleCommandLineHelpers
{
    public static int StartAt { get; set; } = 1; //i think its common to need to remove the first item (?)
    private static BasicList<string> GetUserArgs()
    {
        BasicList<string> arguments = Environment.GetCommandLineArgs().ToBasicList();
        int skip = Math.Max(0, StartAt); // prevent negatives
        skip.Times(arguments.RemoveFirstItem);
        return arguments;
    }
    public static string GetCommandName()
    {
        BasicList<string> arguments = GetUserArgs();
        return arguments.Count > 0 ? arguments[0] : "";
    }
    public static bool HasFlag(string argument)
    {
        BasicList<string> arguments = GetUserArgs();
        return arguments.Any(x => x.Equals(argument, StringComparison.OrdinalIgnoreCase));
    }
    public static bool IsCommand(string commandName)
    {
        BasicList<string> arguments = GetUserArgs();
        if (arguments.Count == 0)
        {
            return false;
        }

        return string.Equals(arguments[0], commandName, StringComparison.OrdinalIgnoreCase);
    }
    public static string GetValue(string commandName)
    {
        BasicList<string> arguments = GetUserArgs();
        for (int i = 0; i < arguments.Count - 1; i++)
        {
            if (arguments[i].Equals(commandName, StringComparison.OrdinalIgnoreCase))
            {
                return arguments[i + 1];
            }
        }
        return "";
    }
}