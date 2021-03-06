namespace CommonBasicLibraries.NugetHelpers;
public class PublicNugetUploader : INugetUploader
{
    private readonly INugetKey _nugetKey;
    public PublicNugetUploader(INugetKey nugetKey)
    {
        _nugetKey = nugetKey;
    }
    private void Process_OutputDataReceived(object sender, DataReceivedEventArgs e)
    {
        if (string.IsNullOrWhiteSpace(e.Data))
        {
            return;
        }
        Console.WriteLine(e.Data);
    }
    async Task<bool> INugetUploader.UploadNugetPackageAsync(string nugetPath)
    {
        Console.WriteLine($"Uploading Package {nugetPath}");
        string key = await _nugetKey.GetKeyAsync();
        string filePath = await ff.GetSpecificFileAsync(nugetPath, ".nupkg");
        string tempName = ff.FullFile(filePath);
        ProcessStartInfo psi = new();
        psi.WorkingDirectory = nugetPath; //hopefully this works.
        psi.FileName = "dotnet";
        psi.Arguments = $"nuget push {tempName} -k {key} -s https://api.nuget.org/v3/index.json";
        psi.RedirectStandardOutput = true;
        psi.CreateNoWindow = true;
        psi.UseShellExecute = false;
        Process process = new();
        process.StartInfo = psi;
        process.EnableRaisingEvents = true;
        process.OutputDataReceived += Process_OutputDataReceived;
        process.Start();
        process.BeginOutputReadLine();
        bool rets = process.WaitForExit(200000);
        return rets;
    }
}