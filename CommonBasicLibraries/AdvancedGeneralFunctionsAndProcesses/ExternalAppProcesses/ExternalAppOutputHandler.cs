namespace CommonBasicLibraries.AdvancedGeneralFunctionsAndProcesses.ExternalAppProcesses;
public static class ExternalAppOutputHandler
{
    // Send a success result with the given data
    public static void SendSuccess(string message)
    {
        Console.WriteLine($"[RESULT] {message}");
        Environment.Exit(0); // Indicates success
    }

    // Send an error message
    public static void SendError(string errorMessage)
    {
        Console.WriteLine($"[RESULT] An error occurred: {errorMessage}");
        Environment.Exit(1); // Indicates failure
    }
    public static void SendError(Exception ex)
    {
        Console.WriteLine($"[RESULT] An error occurred: {ex.Message}");
        Environment.Exit(1); // Indicates failure
    }
    // Send a test result to simulate processing
    public static void SendBlankMessage()
    {
        Console.WriteLine("[RESULT]"); // Simulating a blank result (can happen in real scenarios)
        Environment.Exit(0); // Success
    }
}