namespace CommonBasicLibraries.AdvancedGeneralFunctionsAndProcesses.ExternalAppProcesses;
public static class ToolAliasRunner
{
    public static async Task<int> RunAsync(string[] args, string toolName)
    {
        ProcessStartInfo startInfo = new()
        {
            FileName = toolName,
            UseShellExecute = false
        };

        foreach (string argument in args)
        {
            startInfo.ArgumentList.Add(argument);
        }

        using Process process = Process.Start(startInfo)
            ?? throw new CustomBasicException(
                $"Unable to start tool: {toolName}");

        await process.WaitForExitAsync();
        return process.ExitCode;
    }
}