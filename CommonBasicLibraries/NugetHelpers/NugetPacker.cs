namespace CommonBasicLibraries.NugetHelpers;
public class NugetPacker : INugetPacker
{
    async Task<bool> INugetPacker.CreateNugetPackageAsync(INugetModel project, bool useVsVersioning, bool noBuild)
    {
        Console.WriteLine($"Creating Package For {project.ProjectDirectory}");
        await ff1.DeleteSeveralFilesAsync(project.NugetPath, ".nupkg");
        ProcessStartInfo starts = new();
        starts.WorkingDirectory = project.ProjectDirectory;
        bool rets = project.CSPath.HasCom();
        if (rets == false)
        {
            starts.FileName = "dotnet";
            if (useVsVersioning)
            {
                if (noBuild)
                {
                    starts.Arguments = "pack -c release --no-build";
                }
                else
                {
                    starts.Arguments = "pack -c release";
                }
                
            }
            else
            {
                if (noBuild)
                {
                    starts.Arguments = $"pack -c release --no-build -p:PackageVersion={project.LastVersion}";
                }
                else
                {
                    starts.Arguments = $"pack -c release -p:PackageVersion={project.LastVersion}";
                }
            }
        }
        else
        {
            if (useVsVersioning == false)
            {
                throw new CustomBasicException("Com objects has to use visual studio's versioning system.  Updates to those libraries should not be that common anyways");
            }
            starts.FileName = "nuget";
            starts.Arguments = @"pack -Properties Configuration=Release -OutputDirectory bin\Release"; //since com is not that common, just do from vs project file
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
        rets = await ff1.NewFileCreatedAsync(project.NugetPath, ".nupkg"); //double check to see if the file is there.
        return rets;
    }
}