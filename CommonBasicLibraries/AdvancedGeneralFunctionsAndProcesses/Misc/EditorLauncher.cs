namespace CommonBasicLibraries.AdvancedGeneralFunctionsAndProcesses.Misc;
public static class EditorLauncher
{
    public static bool OpenFileInEditor(string filePath, string? editorPath = null, bool quiet = false)
    {
        if (!File.Exists(filePath))
        {
            if (!quiet)
            {
                ShowError($"File not found: {filePath}");
            }
            return false;
        }

        string editor = editorPath ?? GetDefaultVisualStudioPath()!;
        if (string.IsNullOrWhiteSpace(editor) || !File.Exists(editor))
        {
            if (!quiet)
            {
                ShowError($"Editor not found: {editor}");
            }
            return false;
        }

        try
        {
            var info = new ProcessStartInfo
            {
                FileName = editor,
                Arguments = $"\"{filePath}\"",
                UseShellExecute = true
            };
            Process.Start(info);
            return true;
        }
        catch (Exception ex)
        {
            if (!quiet)
            {
                ShowError($"Failed to launch editor: {ex.Message}");
            }
            return false;
        }
    }

    public static string? GetDefaultVisualStudioPath()
    {
        string vsPath = @"C:\Program Files\Microsoft Visual Studio\2022\Community\Common7\IDE\devenv.exe";
        return File.Exists(vsPath) ? vsPath : null;
    }

    private static void ShowError(string message)
    {
        // Optionally swap out based on context — console fallback
        if (Environment.UserInteractive)
        {
            Console.Error.WriteLine($"Error: {message}");
        }
        else
        {
            throw new CustomBasicException(message);
            // No-op or log somewhere, or integrate with a logger/MessageBox
        }
    }


}
