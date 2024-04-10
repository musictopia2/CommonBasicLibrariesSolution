namespace CommonBasicLibraries.NugetHelpers;
public static class PackageUtilities
{
    public static void RestorePackagesForProject(string projectPath)
    {
        ProcessStartInfo psi = new();
        psi.FileName = "dotnet";
        psi.Arguments = $"dotnet restore {projectPath}";
        psi.CreateNoWindow = true;
        psi.UseShellExecute = false;
        psi.RedirectStandardOutput = true;
        Process process = new();
        process.StartInfo = psi;
        process.EnableRaisingEvents = true;
        process.OutputDataReceived += Process_OutputDataReceived;
        process.Start();
        process.BeginOutputReadLine();
        process.WaitForExit(200000);
        static void Process_OutputDataReceived(object sender, DataReceivedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(e.Data))
            {
                return;
            }
            if (e.Data.Contains("error", StringComparison.CurrentCultureIgnoreCase))
            {
                //this is used to double check after adding all latest versions of packages.
                throw new CustomBasicException($"There was probably a compatibility error.  The message returned was {e.Data}");
            }
            Console.WriteLine(e.Data);
        }
    }
    public static void UpdateAllPackagesInProjectToLatest(string projectPath)
    {
        var list = ListAllPackagesInProject(projectPath);
        Console.WriteLine($"Updating All Packages From {projectPath}");
        foreach (var package in list)
        {
            UpdatePackageToLatest(projectPath, package, false);
        }
    }

    public static void UpdatePackageToLatest(string projectPath, string packageName, bool showProgress = true)
    {
        ProcessStartInfo psi = new();
        psi.FileName = "dotnet";
        psi.Arguments = $"dotnet add {projectPath} package {packageName}"; //forced to try to restore.  otherwise, gets hosed in the project files.
        psi.CreateNoWindow = true;
        psi.UseShellExecute = false;
        if (showProgress)
        {
            Console.WriteLine($"Trying to update package for name of {packageName} and path {projectPath}");
        }
        Process process = new();
        process.StartInfo = psi;
        process.Start();
        process.WaitForExit(200000);
    }
    public static BasicList<string> ListAllPackagesInProject(string projectPath)
    {
        ProcessStartInfo psi = new();
        psi.FileName = "dotnet";
        psi.Arguments = $"dotnet list {projectPath} package"; //just for the sample.
        psi.CreateNoWindow = true;
        psi.UseShellExecute = false;
        psi.RedirectStandardOutput = true;
        Process process = new();
        process.StartInfo = psi;
        process.EnableRaisingEvents = true;
        BasicList<string> output = [];
        process.OutputDataReceived += Process_OutputDataReceived;
        process.Start();
        process.BeginOutputReadLine();
        process.WaitForExit(200000);
        return output;
        void Process_OutputDataReceived(object sender, DataReceivedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(e.Data))
            {
                return;
            }
            HtmlParser parses = new();
            parses.Body = e.Data;
            if (parses.DoesExist(">") == false)
            {
                return;
            }
            if (e.Data.Contains('>') == false)
            {
                return;
            }
            output.Add(parses.GetSomeInfo("> ", " "));
        }
    }
}