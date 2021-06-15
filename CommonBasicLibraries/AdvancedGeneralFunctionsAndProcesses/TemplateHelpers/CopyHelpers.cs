﻿using CommonBasicLibraries.CollectionClasses;
using System;
using System.IO;
using System.Threading.Tasks;
using fs = CommonBasicLibraries.AdvancedGeneralFunctionsAndProcesses.FileFunctions.FileFunctions;
namespace CommonBasicLibraries.AdvancedGeneralFunctionsAndProcesses.TemplateHelpers
{
    public static class CopyHelpers
    {
        public static Func<string, string> ExtraReplacements { get; set; } = DefaultReplacements;
        public static string DefaultReplacements(string original) => original; //so if somebody does not specify otherwise, will be default.
        public static async Task CopyInitialFilesAsync(FileInfo currentFile, string templateName)
        {
            BasicList<string> fileList = await fs.FileListAsync(currentFile.OldPath);
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
                FileInfo updated = new();
                updated.NewName = currentFile.NewName;
                updated.OldPath = nextPath;
                updated.NewPath = Path.Combine(currentFile.NewPath, oldFolder);
                var fileList = await fs.FileListAsync(updated.OldPath);
                await fs.CreateFolderAsync(updated.NewPath);
                await fileList.ForEachAsync(async oldFile =>
                {
                    await CopyFileAsync(oldFile, templateName, updated);
                });
            });
        }
        public static async Task CopyExtraFilesAsync(string fileLocation, string templateName, FileInfo currentFile )
        {
            var files = await fs.FileListAsync(fileLocation);
            await files.ForEachAsync(async file =>
            {
                await CopyFileAsync(file, templateName, currentFile);
            });
        }

        private static async Task CopyFileAsync(string oldFile, string templateName, FileInfo currentFile)
        {
            string oldName = fs.FullFile(oldFile);
            string ourName = oldName.Replace(templateName, currentFile.NewName);
            string newPath = Path.Combine(currentFile.NewPath, ourName);
            await CopyFileAsync(oldFile, templateName, currentFile.NewName, newPath);
        }
        private static async Task CopyFileAsync(string oldFile, string templateName, string newName, string newPath)
        {
            if (oldFile.ToLower().EndsWith("png") || oldFile.ToLower().EndsWith("ico"))
            {
                await fs.FileCopyAsync(oldFile, newPath);
                return; //in this case just copy/paste since its not text file.
            }
            string firstText = await fs.AllTextAsync(oldFile);
            firstText = firstText.Replace(templateName, newName);
            firstText = ExtraReplacements.Invoke(firstText);

            //newPath = newPath.Replace(templateName, newName);

            //string oldport = "5000";
            //string newport = _port.ToString();
            //firstText = firstText.Replace(oldport, newport);
            await fs.WriteAllTextAsync(newPath, firstText);
        }
        public static void CopyAll(DirectoryInfo source, DirectoryInfo target)
        {
            Directory.CreateDirectory(target.FullName);
            foreach (System.IO.FileInfo fi in source.GetFiles())
            {
                fi.CopyTo(Path.Combine(target.FullName, fi.Name), true);
            }
            // Copy each subdirectory using recursion.
            foreach (DirectoryInfo diSourceSubDir in source.GetDirectories())
            {
                DirectoryInfo nextTargetSubDir =
                    target.CreateSubdirectory(diSourceSubDir.Name);
                CopyAll(diSourceSubDir, nextTargetSubDir);
            }
        }
    }
}