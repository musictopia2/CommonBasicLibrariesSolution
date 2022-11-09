namespace CommonBasicLibraries.AdvancedGeneralFunctionsAndProcesses.TemplateHelpers;
public static class CopyHelpers
{
    public static Func<string, string> ExtraReplacements { get; set; } = DefaultReplacements;
    public static string DefaultReplacements(string original) => original; //so if somebody does not specify otherwise, will be default.
    public static async Task CopyInitialFilesAsync(FileInfo currentFile, string templateName)
    {
        BasicList<string> fileList = await ff1.FileListAsync(currentFile.OldPath);
        await fileList.ForEachAsync(async oldFile =>
        {
            await CopyFileAsync(oldFile, templateName, currentFile);
        });
    }
    public static async Task CopyFoldersAsync(BasicList<string> folders, string templateName, FileInfo currentFile)
    {
        await folders.ForEachAsync(async oldFolder =>
        {
            string nextPath = Path.Combine(currentFile.OldPath, oldFolder);
            if (ff1.DirectoryExists(nextPath) == true)
            {
                FileInfo updated = new(); //looks like we can have a folder that either is there or is not there.
                updated.NewName = currentFile.NewName;
                updated.OldPath = nextPath;
                updated.NewPath = Path.Combine(currentFile.NewPath, oldFolder);
                var fileList = await ff1.FileListAsync(updated.OldPath);
                await ff1.CreateFolderAsync(updated.NewPath);
                await fileList.ForEachAsync(async oldFile =>
                {
                    await CopyFileAsync(oldFile, templateName, updated);
                });
            }
        });
    }
    public static async Task CopyExtraFilesAsync(string fileLocation, string templateName, FileInfo currentFile)
    {
        var files = await ff1.FileListAsync(fileLocation);
        await files.ForEachAsync(async file =>
        {
            await CopyFileAsync(file, templateName, currentFile);
        });
    }
    private static async Task CopyFileAsync(string oldFile, string templateName, FileInfo currentFile)
    {
        string oldName = ff1.FullFile(oldFile);
        string ourName = oldName.Replace(templateName, currentFile.NewName);
        string newPath = Path.Combine(currentFile.NewPath, ourName);
        await CopyFileAsync(oldFile, templateName, currentFile.NewName, newPath);
    }
    private static async Task CopyFileAsync(string oldFile, string templateName, string newName, string newPath)
    {
        if (oldFile.ToLower().EndsWith("png") || oldFile.ToLower().EndsWith("ico"))
        {
            await ff1.FileCopyAsync(oldFile, newPath);
            return;
        }
        string firstText = await ff1.AllTextAsync(oldFile);
        firstText = firstText.Replace(templateName, newName);
        firstText = ExtraReplacements.Invoke(firstText);
        await ff1.WriteAllTextAsync(newPath, firstText);
    }
    public static void CopyAll(DirectoryInfo source, DirectoryInfo target)
    {
        Directory.CreateDirectory(target.FullName);
        foreach (System.IO.FileInfo fi in source.GetFiles())
        {
            fi.CopyTo(Path.Combine(target.FullName, fi.Name), true);
        }
        foreach (DirectoryInfo diSourceSubDir in source.GetDirectories())
        {
            DirectoryInfo nextTargetSubDir =
                target.CreateSubdirectory(diSourceSubDir.Name);
            CopyAll(diSourceSubDir, nextTargetSubDir);
        }
    }
}