namespace CommonBasicLibraries.AdvancedGeneralFunctionsAndProcesses.Misc;
public static class SimpleCommandLineHelpers
{
    public static int StartAt { get; set; } = 1;
    private static BasicList<string> GetUserArgs()
    {
        BasicList<string> arguments = Environment.GetCommandLineArgs().ToBasicList();
        int skip = Math.Max(0, StartAt);
        skip.Times(arguments.RemoveFirstItem);
        return arguments;
    }
    private static string Normalize(string input)
    {
        if (string.IsNullOrWhiteSpace(input))
        {
            return "";
        }
        return input.TrimStart('-', '/'); // strips --option, -option, or /option
    }
    public static string GetCommandName()
    {
        BasicList<string> arguments = GetUserArgs();
        return arguments.Count > 0 ? Normalize(arguments[0]) : "";
    }
    public static bool HasFlag(string argument)
    {
        string normalized = Normalize(argument);
        BasicList<string> arguments = GetUserArgs();
        return arguments.Any(x => Normalize(x).Equals(normalized, StringComparison.OrdinalIgnoreCase));
    }
    public static bool IsCommand(string commandName)
    {
        BasicList<string> arguments = GetUserArgs();
        if (arguments.Count == 0)
        {
            return false;
        }
        return Normalize(arguments[0]).Equals(Normalize(commandName), StringComparison.OrdinalIgnoreCase);
    }
    public static string GetValue(string commandName)
    {
        string normalized = Normalize(commandName);
        BasicList<string> arguments = GetUserArgs();
        for (int i = 0; i < arguments.Count - 1; i++)
        {
            if (Normalize(arguments[i]).Equals(normalized, StringComparison.OrdinalIgnoreCase))
            {
                return arguments[i + 1];
            }
        }
        return "";
    }
}