﻿namespace CommonBasicLibraries.AdvancedGeneralFunctionsAndProcesses.Misc;
public static class SimpleCommandLineHelpers
{
    public static int StartAt { get; set; }
    public static string GetValue(string commandName)
    {
        BasicList<string> arguments = Environment.GetCommandLineArgs().ToBasicList();
        StartAt.Times(arguments.RemoveFirstItem);
        int x = 0;
        int y = 1;


        do
        {
            string usedName = arguments[x];
            string value = arguments[y];
            if (usedName == commandName)
            {
                return value;
            }
            x += 2;
            y += 2;
            if (x + 1 >= arguments.Count)
            {
                return ""; //does not have to be there.  if not there, then something else can decide what to do.
            }
        } while (true);
    }
}
