using System.Xml.Linq;
namespace CommonBasicLibraries.AdvancedGeneralFunctionsAndProcesses.TemplateHelpers;
public static class TemplateToolRunner
{
    public static async Task RunAsync(
        string[] args,
        string templateName,
        string? prefix = null)
    {
        try
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(templateName);

            string workingDirectory = Directory.GetCurrentDirectory();

            string visualStudioPath = await FindVisualStudioAsync()
                ?? throw new CustomBasicException(
                    "Visual Studio could not be found.");

            string? name = args.FirstOrDefault();

            while (string.IsNullOrWhiteSpace(name))
            {
                Console.WriteLine("Please provide a name for the project:");
                name = Console.ReadLine();

                if (name is null)
                {
                    throw new CustomBasicException(
                        "No project name was provided.");
                }
            }

            name = name.Trim();
            ValidateProjectName(name);

            string projectDirectory = Path.Combine(
                workingDirectory, name);

            if (Directory.Exists(projectDirectory) ||
                File.Exists(projectDirectory))
            {
                throw new CustomBasicException(
                    $"The destination already exists: {projectDirectory}");
            }

            string templatePackage = string.IsNullOrWhiteSpace(prefix)
                ? templateName
                : $"{prefix.Trim().TrimEnd('.')}.{templateName}";

            await EnsureTemplateInstalledAsync(
                templateName,
                templatePackage,
                workingDirectory);

            Console.WriteLine($"Creating project '{name}'...");

            var creation = await RunProcessAsync(
                "dotnet",
                workingDirectory,
                "new",
                templateName,
                "--name",
                name,
                "--output",
                projectDirectory);

            WriteOutput(creation.Output, creation.Error);

            if (creation.ExitCode != 0)
            {
                throw new CustomBasicException(
                    "Project creation failed.");
            }

            string projectFilePath = Path.Combine(
                projectDirectory, $"{name}.csproj");

            if (File.Exists(projectFilePath) == false)
            {
                throw new CustomBasicException(
                    $"The expected project file was not created: {projectFilePath}");
            }

            string solutionFilePath = Path.Combine(
                projectDirectory, $"{name}.slnx");

            // Preserve a solution if the template already created one.
            if (File.Exists(solutionFilePath) == false)
            {
                XDocument solution = new(
                    new XElement(
                        "Solution",
                        new XElement(
                            "Project",
                            new XAttribute("Path", $"{name}.csproj"))));

                solution.Save(solutionFilePath);
            }

            ProcessStartInfo startInfo = new()
            {
                FileName = visualStudioPath,
                WorkingDirectory = projectDirectory,
                UseShellExecute = false
            };

            startInfo.ArgumentList.Add(solutionFilePath);

            using Process process = Process.Start(startInfo)
                ?? throw new CustomBasicException(
                    "Visual Studio could not be started.");

            Console.WriteLine($"Created project: {projectDirectory}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine(ex.Message);
            Environment.ExitCode = 1;
        }
    }

    private static void ValidateProjectName(string name)
    {
        if (name is "." or ".." ||
            name.IndexOfAny(Path.GetInvalidFileNameChars()) >= 0 ||
            name.Contains('/') ||
            name.Contains('\\') ||
            name.EndsWith('.'))
        {
            throw new CustomBasicException(
                "Enter a project name, without a folder path.");
        }
    }

    private static async Task EnsureTemplateInstalledAsync(
        string templateName,
        string templatePackage,
        string workingDirectory)
    {
        if (await IsTemplateInstalledAsync(templateName, workingDirectory))
        {
            return;
        }

        Console.WriteLine(
            $"Template '{templateName}' is not installed. " +
            $"Installing '{templatePackage}'...");

        var installation = await RunProcessAsync(
            "dotnet",
            workingDirectory,
            "new",
            "install",
            templatePackage);

        WriteOutput(installation.Output, installation.Error);

        if (installation.ExitCode != 0)
        {
            throw new CustomBasicException(
                "Template installation failed.");
        }

        if (await IsTemplateInstalledAsync(
            templateName, workingDirectory) == false)
        {
            throw new CustomBasicException(
                $"The package was installed, but template " +
                $"'{templateName}' could not be found.");
        }
    }

    private static async Task<bool> IsTemplateInstalledAsync(
        string templateName,
        string workingDirectory)
    {
        var result = await RunProcessAsync(
            "dotnet",
            workingDirectory,
            "new",
            "list");

        if (result.ExitCode != 0)
        {
            WriteOutput(result.Output, result.Error);

            throw new CustomBasicException(
                "Could not check the installed templates.");
        }

        // The table contains the template name followed by its short names.
        // Match a complete short name instead of a substring.
        foreach (string line in result.Output.Split('\n'))
        {
            string[] columns = Regex.Split(line.Trim(), @"\s{2,}");

            if (columns.Length < 2)
            {
                continue;
            }

            string[] shortNames = columns[1].Split(
                ',',
                StringSplitOptions.RemoveEmptyEntries |
                StringSplitOptions.TrimEntries);

            if (shortNames.Any(x => string.Equals(
                x,
                templateName,
                StringComparison.OrdinalIgnoreCase)))
            {
                return true;
            }
        }

        return false;
    }

    private static async Task<string?> FindVisualStudioAsync()
    {
        string programFiles = Environment.GetFolderPath(
            Environment.SpecialFolder.ProgramFilesX86);

        string vswherePath = Path.Combine(
            programFiles,
            @"Microsoft Visual Studio\Installer\vswhere.exe");

        if (File.Exists(vswherePath) == false)
        {
            return null;
        }

        var result = await RunProcessAsync(
            vswherePath,
            Directory.GetCurrentDirectory(),
            "-latest",
            "-requires",
            "Microsoft.Component.MSBuild",
            "-products",
            "*",
            "-property",
            "installationPath");

        if (result.ExitCode != 0 ||
            string.IsNullOrWhiteSpace(result.Output))
        {
            return null;
        }

        string visualStudioPath = Path.Combine(
            result.Output.Trim(),
            @"Common7\IDE\devenv.exe");

        return File.Exists(visualStudioPath)
            ? visualStudioPath
            : null;
    }

    private static async Task<(
        int ExitCode,
        string Output,
        string Error)> RunProcessAsync(
            string fileName,
            string workingDirectory,
            params string[] arguments)
    {
        ProcessStartInfo startInfo = new()
        {
            FileName = fileName,
            WorkingDirectory = workingDirectory,
            UseShellExecute = false,
            CreateNoWindow = true,
            RedirectStandardOutput = true,
            RedirectStandardError = true
        };

        foreach (string argument in arguments)
        {
            startInfo.ArgumentList.Add(argument);
        }

        using Process process = Process.Start(startInfo)
            ?? throw new CustomBasicException(
                $"Could not start '{fileName}'.");

        // Read both streams concurrently so neither blocks the process.
        Task<string> outputTask = process.StandardOutput.ReadToEndAsync();
        Task<string> errorTask = process.StandardError.ReadToEndAsync();

        await process.WaitForExitAsync();

        return (
            process.ExitCode,
            await outputTask,
            await errorTask);
    }

    private static void WriteOutput(string output, string error)
    {
        if (string.IsNullOrWhiteSpace(output) == false)
        {
            Console.WriteLine(output.TrimEnd());
        }

        if (string.IsNullOrWhiteSpace(error) == false)
        {
            Console.Error.WriteLine(error.TrimEnd());
        }
    }
}