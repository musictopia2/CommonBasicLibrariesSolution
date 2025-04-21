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
            RedirectStandardError = true,
            CreateNoWindow = true,
            WindowStyle = ProcessWindowStyle.Hidden
        };
        try
        {
            using var process = Process.Start(startInfo) ?? throw new Exception($"Failed to start the process: {exePath} with arguments: {arguments}");

            // Read the full output
            string fullOutput = await process!.StandardOutput.ReadToEndAsync(token);
            string fullError = await process.StandardError.ReadToEndAsync(token);


            

            // Wait for the process to exit
            await process.WaitForExitAsync(token);

            // If the process exits with a non-zero code, use the output from the app
            if (process.ExitCode != 0)
            {
                throw new CustomBasicException($"External process failed with exit code {process.ExitCode}. Error: {fullError}");
            }

            string resultMarker = "[RESULT]";
            int resultIndex = fullOutput.IndexOf(resultMarker);
            if (resultIndex >= 0)
            {
                string result = fullOutput.Substring(resultIndex + resultMarker.Length).Trim();
                return result;
            }
            else
            {
                throw new CustomBasicException("No result.  means did not use a compatible app");
            }
        }
        catch (OperationCanceledException)
        {
            // Handle the cancellation specifically
            Console.WriteLine("Process was canceled by the user.");
            return string.Empty;  // Return empty string to indicate cancellation
        }
        catch (Exception ex)
        {
            // Catch any other exceptions that happen during the execution of the process
            throw new CustomBasicException($"An error occurred while executing the external process: {ex.Message}");
        }
    }
}