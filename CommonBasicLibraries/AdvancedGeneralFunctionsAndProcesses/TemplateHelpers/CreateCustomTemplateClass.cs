namespace CommonBasicLibraries.AdvancedGeneralFunctionsAndProcesses.TemplateHelpers
{
    public static class CreateCustomTemplateClass
    {
        public static void CreateTemplate(string templateName, string newName, string newLocation)
        {
            Process procs = new();
            ProcessStartInfo starts = new();
            procs.StartInfo = starts;
            starts.FileName = "dotnet";
            starts.CreateNoWindow = true;
            starts.WorkingDirectory = newLocation;
            starts.Arguments = $"new {templateName} -o {newName}";
            starts.UseShellExecute = false;
            starts.RedirectStandardOutput = true;
            procs.OutputDataReceived += Procs_OutputDataReceived;
            procs.Start();
            procs.BeginOutputReadLine();
            procs.WaitForExit();
            procs.Close();
            procs.Dispose();
        }
        private static void Procs_OutputDataReceived(object sender, DataReceivedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(e.Data))
            {
                return;
            }
            Console.WriteLine(e.Data); //probably does not work anyways because from wpf.
        }
    }
}