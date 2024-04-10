using System.IO.Compression; //not common enough.
namespace CommonBasicLibraries.AdvancedGeneralFunctionsAndProcesses.ZipClasses;
public class CustomZipClass
{
    private readonly BasicList<PrivateZipInfo> _zipList = [];
    public void Clear()
    {
        _zipList.Clear();
    }
    public void AddFileToZip(string path)
    {
        _zipList.Add(new PrivateZipInfo()
        {
            Path = path
        });
    }
    public void AddFileToZip(string path, string folder)
    {
        _zipList.Add(new PrivateZipInfo()
        {
            Path = path,
            Folder = folder
        });
    }
    public void SaveZipFile(string zipPath, bool clearContents = true)
    {
        if (_zipList.Count == 0)
        {
            throw new CustomBasicException("There are no files for this zip file");
        }
        if (File.Exists(zipPath))
        {
            File.Delete(zipPath);
        }
        using FileStream zipToOpen = new(zipPath, FileMode.Create);
        using ZipArchive archive = new(zipToOpen, ZipArchiveMode.Create);
        _zipList.ForEach(x =>
        {
            string relative;
            string name = ff1.FullFile(x.Path);
            if (x.Folder == "")
            {
                relative = name;
            }
            else
            {
                relative = Path.Combine(x.Folder, name);
            }
            ZipArchiveEntry rr = archive.CreateEntryFromFile(x.Path, relative);
        });
        if (clearContents)
        {
            _zipList.Clear();
        }
    }
    public static BasicList<OpenedZipFile> OpenZipFile(string path)
    {
        using ZipArchive archive = ZipFile.OpenRead(path);
        BasicList<OpenedZipFile> output = [];
        foreach (ZipArchiveEntry entry in archive.Entries)
        {
            OpenedZipFile zip = new();
            zip.ModifyDate = entry.LastWriteTime.DateTime;
            zip.FileName = entry.Name;
            if (entry.FullName == entry.Name)
            {
                zip.FolderName = "";
            }
            else
            {
                zip.FolderName = entry.FullName.Replace(entry.Name, "");
            }
            output.Add(zip);
        }
        if (output.Count == 0)
        {
            throw new CustomBasicException("There are no files found in the zip file");
        }
        return output;
    }
    public static void UnzipAll(string zipFile, string extractPath)
    {
        ZipFile.ExtractToDirectory(zipFile, extractPath, true);
    }
    public static void UnzipFile(string zipFile, string extractPath, BasicList<OpenedZipFile> files)
    {
        using ZipArchive archive = ZipFile.OpenRead(zipFile);
        BasicList<ZipArchiveEntry> entries = [];
        files.ForEach(file =>
        {
            ZipArchiveEntry zip = archive.Entries.Where(x => x.Name == file.FileName).Single();
            entries.Add(zip);
        });

        if (Directory.Exists(extractPath) == false)
        {
            throw new CustomBasicException("Destination does not exist");
        }
        entries.ForEach(x =>
        {
            BasicList<string> folders = x.FullName.Split(@"\").ToBasicList();
            folders.RemoveLastItem();
            string temps = extractPath;
            folders.ForEach(folder =>
            {
                temps = Path.Combine(temps, folder);
                if (Directory.Exists(temps) == false)
                {
                    Directory.CreateDirectory(temps);
                }
            });
            x.ExtractToFile(@$"C:\TempFiles\ResultZip\{x.FullName}", true);
        });
    }
}