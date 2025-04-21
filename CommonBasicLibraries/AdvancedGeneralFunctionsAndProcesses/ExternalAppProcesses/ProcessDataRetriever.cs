namespace CommonBasicLibraries.AdvancedGeneralFunctionsAndProcesses.ExternalAppProcesses;
public static class ProcessDataRetriever
{
    public static async Task<string> GetResultFromExternalAppAsync(string exePath, string arguments, CancellationToken token = default)
    {
        var startInfo = new ProcessStartInfo
        {
            FileName = exePath,
            Arguments = arguments,
            UseShellExecute = false,
            RedirectStandardOutput = true,
            CreateNoWindow = true,
            WindowStyle = ProcessWindowStyle.Hidden
        };
        using var process = Process.Start(startInfo);
        string resultLine = "";
        while (!process!.StandardOutput.EndOfStream)
        {
            string? line = await process.StandardOutput.ReadLineAsync(token)!;
            if (line != null && line.StartsWith("[RESULT]"))
            {
                resultLine = line.Substring("[RESULT]".Length).Trim();
            }
        }
        await process.WaitForExitAsync(token);
        if (process.ExitCode != 0)
        {
            throw new Exception($"There was an error of {resultLine}");
        }
        if (resultLine != null)
        {
            return resultLine;
        }
        else
        {
            return "";
        }
    }
}