namespace CommonBasicLibraries.NugetHelpers
{
    public class NugetPacker : INugetPacker
    {
        async Task<bool> INugetPacker.CreateNugetPackageAsync(INugetModel project)
        {
            Console.WriteLine($"Creating Package For {project.ProjectDirectory}");
            await ff.DeleteSeveralFilesAsync(project.NugetPath, ".nupkg");
            ProcessStartInfo starts = new();
            starts.WorkingDirectory = project.ProjectDirectory;
            bool rets = project.CSPath.HasCom();
            if (rets == false)
            {
                starts.FileName = "dotnet";
                starts.Arguments = "pack -c release --no-build";
            }
            else
            {
                starts.FileName = "nuget";
                starts.Arguments = @"pack -Properties Configuration=Release -OutputDirectory bin\Release";
            }
            starts.CreateNoWindow = true;
            starts.WindowStyle = ProcessWindowStyle.Hidden;
            starts.UseShellExecute = true;
            Process process = new();
            process.StartInfo = starts;
            process.Start();
            rets = process.WaitForExit(10000);
            if (rets == false)
            {
                throw new CustomBasicException("Failed To Create Nuget Package");
            }
            rets = await ff.NewFileCreatedAsync(project.NugetPath, ".nupkg"); //double check to see if the file is there.
            return rets;
        }
    }
}