namespace CommonBasicLibraries.AdvancedGeneralFunctionsAndProcesses.FileFunctions;
public static class FileFunctions
{
    public static void PopulateAndroidDesktopApplicationPath(ref string fileName)
    {
        if (bb1.OS == bb1.EnumOS.Android)
        {
            fileName = Path.Combine(GetApplicationDataForMobileDevices(), fileName);
            return;
        }
        if (bb1.OS == bb1.EnumOS.WindowsDT)
        {
            fileName = Path.Combine(aa1.GetApplicationPath(), fileName);
            return;
        }
        throw new CustomBasicException("Only android and desktop are supported for now");
    }
    public static BasicList<string> GetHttpsLinks(string fileSource)
    {
        HtmlParser parses = new();
        parses.Body = AllText(fileSource);
        BasicList<string> temps = parses.GetList("https", Constants.DoubleQuote, showErrors: false);
        BasicList<string> output = temps.Select(x => "https" + x).ToBasicList();
        return output;
    }
    public static string GetApplicationDataForMobileDevices()
    {
        return Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
    }
    public static string GetSDCardReadingPathForAndroid() //keey this because we want the ability to sometimes not use local storage for blazor stuff1.
    {
        if (DirectoryExists("/storage/sdcard1") == true)
        {
            return "/storage/sdcard1";
        }
        else if (DirectoryExists("//storage/0123-4567"))
        {
            return "//storage/0123-4567";
        }
        else if (DirectoryExists("/storage/emulated/0") == true)
        {
            return "/storage/emulated/0";
        }
        else
        {
            throw new CustomBasicException("Either an unusual situation or not even an android device");
        }
    }
    public static string GetWriteLocationForExternalOnAndroid()
    {
        return @"/sdcard";
    }
    public static bool FileExists(string filePath)
    {
        return File.Exists(bb1.GetCleanedPath(filePath));
    }
    public static async Task<bool> NewFileCreatedAsync(string directoryPath, string expectedExtension)
    {
        var firstList = await FileListAsync(directoryPath);
        string? results = firstList.Find(items =>
        {
            return items.ToLower().EndsWith(expectedExtension.ToLower());
        }
        );
        if (string.IsNullOrWhiteSpace(results) == true)
        {
            return false;
        }
        return true;
    }
    public static bool DirectoryExists(string directoryPath)
    {
        return Directory.Exists(bb1.GetCleanedPath(directoryPath));
    }
    public static async Task<string> GetDriveLetterAsync(string label)
    {
        string thisStr = default!;
        await Task.Run(() =>
        {
            var completeDrives = DriveInfo.GetDrives();
            foreach (var temps in completeDrives)
            {
                if (temps.DriveType == DriveType.Fixed)
                {
                    if (temps.IsReady == true)
                    {
                        if (label.Equals(temps.VolumeLabel, StringComparison.CurrentCultureIgnoreCase))
                        {
                            thisStr = temps.Name.Substring(0, 1);
                        }
                        break;
                    }
                }
            }
        });
        if (thisStr == null)
        {
            throw new CustomBasicException("No label for " + label);
        }
        return thisStr;
    }
    public static async Task FilterDirectoryAsync(BasicList<string> firstList)
    {
        BasicList<string> removeList = [];
        await Task.Run(() =>
        {
            firstList.ForEach(items =>
            {
                if (Directory.EnumerateFiles(bb1.GetCleanedPath(items)).Any() == false)
                {
                    removeList.Add(items);
                }
            });

        });
        firstList.RemoveGivenList(removeList);
    }
    public static void FilterDirectory(BasicList<string> firstList)
    {
        BasicList<string> removeList = [];
        firstList.ForEach(items =>
        {
            if (Directory.EnumerateFiles(bb1.GetCleanedPath(items)).Any() == false)
            {
                removeList.Add(items);
            }
        });
        firstList.RemoveGivenList(removeList);
    }
    public static async Task<BasicList<string>> GetDriveListAsync()
    {

        DriveInfo[] completeDrives;
        BasicList<string> newList = [];
        await Task.Run(() =>
        {
            completeDrives = DriveInfo.GetDrives();
            foreach (var temps in completeDrives)
            {
                if (temps.DriveType == DriveType.Fixed)
                {
                    if (temps.IsReady == true)
                    {
                        newList.Add(temps.RootDirectory.FullName);
                    }
                }
            }
        }
        );
        return newList;
    }
    public static BasicList<string> GetDriveList()
    {
        DriveInfo[] completeDrives;
        BasicList<string> newList = [];
        completeDrives = DriveInfo.GetDrives();
        foreach (var temps in completeDrives)
        {
            if (temps.DriveType == DriveType.Fixed)
            {
                if (temps.IsReady == true)
                {
                    newList.Add(temps.RootDirectory.FullName);
                }
            }
        }
        return newList;
    }
    private static DateTime GetCreatedDate(System.IO.FileInfo thisFile)
    {
        if (thisFile.CreationTime.IsDaylightSavingTime() == true)
        {
            return thisFile.CreationTime.AddHours(-1);
        }
        return thisFile.CreationTime;
    }
    private static DateTime GetModifiedDate(System.IO.FileInfo thisFile)
    {
        if (thisFile.LastWriteTime.IsDaylightSavingTime() == true)
        {
            return thisFile.LastWriteTime.AddHours(-1);
        }
        return thisFile.LastWriteTime;
    }
    public static string GetParentPath(string thisPath)
    {
        var thisDir = Directory.GetParent(bb1.GetCleanedPath(thisPath));
        return thisDir!.FullName;
    }
    public static async Task<FileInfo?> GetFileAsync(string filePath)
    {
        System.IO.FileInfo thisFile;
        FileInfo tempFile = default!;
        await Task.Run(() =>
        {
            thisFile = new System.IO.FileInfo(bb1.GetCleanedPath(filePath));
            tempFile = new FileInfo()
            {
                FileSize = thisFile.Length,
                DateCreated = GetCreatedDate(thisFile),
                Directory = thisFile.DirectoryName!,
                DateAccessed = thisFile.LastAccessTime,
                DateModified = GetModifiedDate(thisFile),
                FilePath = filePath
            };
            if (tempFile.DateModified > tempFile.DateAccessed)
            {
                tempFile.DateAccessed = tempFile.DateModified;
            }
        });
        return tempFile;
    }
    public static FileInfo? GetFile(string filePath)
    {
        System.IO.FileInfo thisFile;
        FileInfo tempFile;
        thisFile = new System.IO.FileInfo(bb1.GetCleanedPath(filePath));
        tempFile = new FileInfo()
        {
            FileSize = thisFile.Length,
            DateCreated = GetCreatedDate(thisFile),
            Directory = thisFile.DirectoryName!,
            DateAccessed = thisFile.LastAccessTime,
            DateModified = GetModifiedDate(thisFile),
            FilePath = filePath
        };
        if (tempFile.DateModified > tempFile.DateAccessed)
        {
            tempFile.DateAccessed = tempFile.DateModified;
        }
        return tempFile;
    }
    public static string DirectoryName(string directoryPath)
    {
        var thisItem = new DirectoryInfo(bb1.GetCleanedPath(directoryPath));
        return thisItem.Name;
    }
    public static string FullFile(string filePath)
    {
        var thisItem = new System.IO.FileInfo(bb1.GetCleanedPath(filePath));
        return thisItem.Name;
    }
    public static string FileName(string filePath)
    {
        var thisItem = new System.IO.FileInfo(bb1.GetCleanedPath(filePath));
        var thisName = thisItem.Name;
        return thisName.Substring(0, thisName.Length - thisItem.Extension.Length);
    }
    public static BasicList<string> DirectoryList(string whatPath, SearchOption searchOption = SearchOption.TopDirectoryOnly)
    {
        return Directory.EnumerateDirectories(bb1.GetCleanedPath(whatPath), "*", searchOption).ToBasicList();
    }
    public static BasicList<string> FileList(string directoryPath, SearchOption thisOption = SearchOption.TopDirectoryOnly)
    {
        return Directory.EnumerateFiles(bb1.GetCleanedPath(directoryPath), "*", thisOption).ToBasicList();
    }
    public static BasicList<string> FileList(BasicList<string> directoryList) //this can be just top because you already sent in a directory list
    {
        BasicList<string> newList = [];
        directoryList.ForEach(x =>
        {
            var temps = FileList(x);
            newList.AddRange(temps);
        });
        return newList;
    }
    public static BasicList<string> GetSeveralSpecificFiles(string directoryPath, string extensionEndsAt, SearchOption searchOption = SearchOption.TopDirectoryOnly)
    {
        var tempList = FileList(directoryPath, searchOption);
        tempList.KeepConditionalItems(items => items.ToLower().EndsWith(extensionEndsAt.ToLower()));
        return tempList;
    }
    /// <summary>
    /// This is used in cases where you know what file names are okay and only those will show the complete paths.  i am guessing that if one from the good list is not found, then raise an exception
    /// </summary>
    /// <param name="directoryPath">Directory To Look For</param>
    /// <param name="extensionEndsAt">This is the extension of the file needed</param>
    /// <param name="goodList">This is a list of all files you know you need.  has names without extensions.</param>
    /// <param name="searchOption">default is all directories but can choose top if needed</param>
    /// <returns></returns>
    public static BasicList<string> GetSeveralSpecificFiles(string directoryPath, string extensionEndsAt, BasicList<string> goodList, SearchOption searchOption = SearchOption.AllDirectories)
    {
        BasicList<string> firstList = GetSeveralSpecificFiles(directoryPath, extensionEndsAt, searchOption);
        firstList.KeepConditionalItems(items =>
        {
            string thisName = FileName(items).ToLower();
            if (goodList.Exists(xx => xx.Equals(thisName, StringComparison.CurrentCultureIgnoreCase)) == true)
            {
                return true;
            }
            return false;
        });
        if (firstList.Count != goodList.Count)
        {
            throw new CustomBasicException(@"Does not reconcile because there was an item on the good list that was not found or had duplicates");
        }
        return firstList;
    }
    public static async Task<BasicList<string>> DirectoryListAsync(string whatPath, SearchOption searchOption = SearchOption.TopDirectoryOnly)
    {
        var tDirectoryList = new BasicList<string>();
        await Task.Run(() =>
        {
            tDirectoryList = Directory.EnumerateDirectories(bb1.GetCleanedPath(whatPath), "*", searchOption).ToBasicList();
        });
        return tDirectoryList;
    }
    public static async Task<BasicList<string>> FileListAsync(string directoryPath, SearchOption thisOption = SearchOption.TopDirectoryOnly)
    {
        BasicList<string> tFileList = [];
        await Task.Run(() =>
        {
            tFileList = Directory.EnumerateFiles(bb1.GetCleanedPath(directoryPath), "*", thisOption).ToBasicList();
        });
        return tFileList;
    }
    public static async Task<BasicList<string>> FileListAsync(BasicList<string> directoryList)
    {
        BasicList<string> newList = [];
        await directoryList.ForEachAsync(async x =>
        {
            var temps = await FileListAsync(x);
            newList.AddRange(temps);
        });
        return newList;
    }
    public static async Task<BasicList<string>> GetSeveralSpecificFilesAsync(string directoryPath, string extensionEndsAt, SearchOption searchOption = SearchOption.TopDirectoryOnly)
    {
        var tempList = await FileListAsync(directoryPath, searchOption);
        tempList.KeepConditionalItems(items => items.ToLower().EndsWith(extensionEndsAt.ToLower()));
        return tempList;
    }
    /// <summary>
    /// This is used in cases where you know what file names are okay and only those will show the complete paths.  i am guessing that if one from the good list is not found, then raise an exception
    /// </summary>
    /// <param name="directoryPath">Directory To Look For</param>
    /// <param name="extensionEndsAt">This is the extension of the file needed</param>
    /// <param name="goodList">This is a list of all files you know you need.  has names without extensions.</param>
    /// <param name="searchOption">default is all directories but can choose top if needed</param>
    /// <returns></returns>
    public static async Task<BasicList<string>> GetSeveralSpecificFilesAsync(string directoryPath, string extensionEndsAt, BasicList<string> goodList, SearchOption searchOption = SearchOption.AllDirectories)
    {
        BasicList<string> firstList = await GetSeveralSpecificFilesAsync(directoryPath, extensionEndsAt, searchOption);
        firstList.KeepConditionalItems(items =>
        {
            string thisName = FileName(items).ToLower();
            if (goodList.Exists(xx => xx.Equals(thisName, StringComparison.CurrentCultureIgnoreCase)) == true)
            {
                return true;
            }
            return false;
        });
        if (firstList.Count != goodList.Count)
        {
            throw new CustomBasicException(@"Does not reconcile because there was an item on the good list that was not found or had duplicates");
        }
        return firstList;
    }
    public static async Task<string> GetSpecificFileAsync(string directoryPath, string extensionEndsAt)
    {
        var tempList = await FileListAsync(directoryPath);
        return tempList.FindOnlyOne(xx => xx.ToLower().EndsWith(extensionEndsAt.ToLower()));
    }
    public static string GetSpecificFile(string directoryPath, string extensionEndsAt)
    {
        var tempList = FileList(directoryPath);
        return tempList.FindOnlyOne(xx => xx.ToLower().EndsWith(extensionEndsAt.ToLower()));
    }
    /// <summary>
    /// This searches for files in not only top directory but sub directories as well
    /// </summary>
    /// <param name="directoryPath">Directory Where To Look</param>
    /// <param name="fileName">File Name Including Extension Needed</param>
    /// <returns></returns>
    public static async Task<string> SearchForFileNameAsync(string directoryPath, string fileName)
    {
        BasicList<string> tFileList = [];
        await Task.Run(() =>
        {
            tFileList = Directory.EnumerateFiles(bb1.GetCleanedPath(directoryPath), fileName, SearchOption.AllDirectories).ToBasicList();
        });
        if (tFileList.Count == 0)
        {
            throw new CustomBasicException($"The File Name {fileName} Was Not Found At {directoryPath} Or Even Sub Directories");
        }
        return tFileList.Single();
    }
    /// <summary>
    /// This searches for files in not only top directory but sub directories as well
    /// </summary>
    /// <param name="directoryPath">Directory Where To Look</param>
    /// <param name="fileName">File Name Including Extension Needed</param>
    /// <returns></returns>
    public static string SearchForFileName(string directoryPath, string fileName)
    {
        BasicList<string> tFileList;
        tFileList = Directory.EnumerateFiles(bb1.GetCleanedPath(directoryPath), fileName, SearchOption.AllDirectories).ToBasicList();
        if (tFileList.Count == 0)
        {
            throw new CustomBasicException($"The File Name {fileName} Was Not Found At {directoryPath} Or Even Sub Directories");
        }
        return tFileList.Single();
    }
    private static async Task<string> PrivateAllTextAsync(string filePath, Encoding encodes)
    {
        using StreamReader s = new(bb1.GetCleanedPath(filePath), encodes);
        string thisText = await s.ReadToEndAsync();
        s.Close();
        return thisText;
    }
    private static string PrivateAllText(string filePath, Encoding encodes)
    {
        using StreamReader s = new(bb1.GetCleanedPath(filePath), encodes);
        string thisText = s.ReadToEnd();
        return thisText;
    }
    private static async Task PrivateWriteAllTextAsync(string filePath, string text, bool append, Encoding encoding, bool isLine = false)
    {
        using StreamWriter w = new(bb1.GetCleanedPath(filePath), append, encoding);
        if (isLine == true)
        {
            await w.WriteLineAsync(text);
        }
        else
        {
            await w.WriteAsync(text);
        }
        await w.FlushAsync();
        w.Close();
    }
    private static void PrivateWriteAllText(string filePath, string text, bool append, Encoding encoding, bool isLine = false)
    {
        using StreamWriter w = new(bb1.GetCleanedPath(filePath), append, encoding);
        if (isLine == true)
        {
            w.WriteLine(text);
        }
        else
        {
            w.Write(text);
        }
        w.Flush();
        w.Close();
    }
    public static async Task<int> ReadNumberFromFileAsync(string filePath)
    {
        string text = await PrivateAllTextAsync(filePath, Encoding.Default);
        return int.Parse(text);
    }
    public static async Task WriteNextNumberAsync(string filePath, int previousNumber)
    {
        previousNumber++;
        await PrivateWriteAllTextAsync(filePath, previousNumber.ToString(), false, Encoding.Default);
    }
    public static async Task ReplaceSeveralTextAsync(string filePath, Func<string, string> replaceActions)
    {
        if (FileExists(filePath) == false)
        {
            throw new CustomBasicException($"Failed to replace text because {filePath} does not exist");
        }
        string text = await AllTextAsync(filePath);
        text = replaceActions.Invoke(text);
        await WriteAllTextAsync(filePath, text);
    }
    public static async Task ReplaceSimpleTextAsync(string filePath, string toSearch, string replaceWith)
    {
        if (FileExists(filePath) == false)
        {
            throw new CustomBasicException($"Failed to replace text because {filePath} does not exist");
        }
        string text = await AllTextAsync(filePath);
        if (text.Contains(toSearch) == false)
        {
            throw new CustomBasicException($"{toSearch} does not exist");
        }
        text = text.Replace(toSearch, replaceWith);
        await WriteAllTextAsync(filePath, text);
    }
    public static async Task<string> AllTextAsync(string filePath)
    {
        return await PrivateAllTextAsync(filePath, Encoding.Default);
    }
    public static async Task<string> AllTextAsync(string FilePath, Encoding encoding)
    {
        return await PrivateAllTextAsync(FilePath, encoding);
    }
    public static string AllText(string filePath, Encoding encoding)
    {
        return PrivateAllText(filePath, encoding);
    }
    public static string AllText(string filePath)
    {
        return PrivateAllText(filePath, Encoding.Default);
    }
    public static async Task WriteAllTextAsync(string filePath, string whatText)
    {
        await PrivateWriteAllTextAsync(filePath, whatText, false, Encoding.Default);
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="filePath"></param>
    /// <param name="whatText"></param>
    /// <param name="encoding">Hint  Use UTF-8 in some cases.</param>
    /// <returns></returns>
    public static async Task WriteAllTextAsync(string filePath, string whatText, Encoding encoding)
    {
        await PrivateWriteAllTextAsync(filePath, whatText, false, encoding);
    }
    public static async Task WriteTextAsync(string filePath, string whatText, bool append)
    {
        await PrivateWriteAllTextAsync(filePath, whatText, append, Encoding.Default, true);
    }
    public static void WriteAllText(string filePath, string whatText)
    {
        PrivateWriteAllText(filePath, whatText, false, Encoding.Default);
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="filePath"></param>
    /// <param name="whatText"></param>
    /// <param name="encoding">Hint  Use UTF-8 in some cases.</param>
    /// <returns></returns>
    public static void WriteAllText(string filePath, string whatText, Encoding encoding)
    {
        PrivateWriteAllText(filePath, whatText, false, encoding);
    }
    public static void WriteText(string filePath, string whatText, bool append)
    {
        PrivateWriteAllText(filePath, whatText, append, Encoding.Default, true);
    }
    public static void WriteAllLines(string filePath, BasicList<string> lines)
    {
        File.WriteAllLines(bb1.GetCleanedPath(filePath), lines);
    }
    public static async Task WriteAllLinesAsync(string filePath, BasicList<string> lines)
    {
        await File.WriteAllLinesAsync(bb1.GetCleanedPath(filePath), lines);
    }
    public static void WriteAllLines(string filePath, BasicList<string> lines, Encoding encoding)
    {
        File.WriteAllLines(bb1.GetCleanedPath(filePath), lines, encoding);
    }
    public static async Task WriteAllLinesAsync(string filePath, BasicList<string> lines, Encoding encoding)
    {
        await File.WriteAllLinesAsync(bb1.GetCleanedPath(filePath), lines, encoding);
    }
    public static async Task FileCopyAsync(string originalFile, string newFile)
    {
        await Task.Run(() => File.Copy(bb1.GetCleanedPath(originalFile), bb1.GetCleanedPath(newFile), true));
    }
    public static async Task DeleteFolderAsync(string path)
    {
        await Task.Run(() => Directory.Delete(bb1.GetCleanedPath(path), true));
    }
    public static async Task CreateFolderAsync(string path)
    {
        await Task.Run(() => Directory.CreateDirectory(bb1.GetCleanedPath(path)));
    }
    public static async Task DeleteSeveralFilesAsync(string directoryPath, string stringEnd)
    {
        BasicList<string> thisList = await FileListAsync(directoryPath);
        thisList.KeepConditionalItems(items =>
        items.ToLower().EndsWith(stringEnd.ToLower()));
        await thisList.ForEachAsync(async items => await DeleteFileAsync(items));
    }
    public static async Task DeleteFileAsync(string path)
    {
        await Task.Run(() => File.Delete(bb1.GetCleanedPath(path)));
    }
    public static async Task RenameFileAsync(string oldFile, string newName)
    {
        if (File.Exists(oldFile) == false && File.Exists(newName) && oldFile.Contains("storage/emulated", StringComparison.CurrentCultureIgnoreCase))
        {
            return;
        }
        await Task.Run(() =>
        {
            string finold = bb1.GetCleanedPath(oldFile);
            try
            {
                File.Move(finold, bb1.GetCleanedPath(newName));
            }
            catch
            {

            }
            if (File.Exists(newName) == false)
            {
                throw new CustomBasicException("Failed to rename file.  Most likely permission problem.  Rethink");
            }
            if (File.Exists(finold))
            {
                File.Delete(finold);
            }
        });
    }
    public static async Task RenameDirectoryAsync(string oldDirectory, string newName)
    {
        await Task.Run(() => Directory.Move(bb1.GetCleanedPath(oldDirectory), bb1.GetCleanedPath(newName)));
    }
    public static async Task<BasicList<string>> ReadAllLinesAsync(string filePath)
    {
        var temps = await File.ReadAllLinesAsync(bb1.GetCleanedPath(filePath));
        return temps.ToBasicList();
    }
    public static async Task<byte[]> ReadAllBytesAsync(string path)
    {
        var output = await File.ReadAllBytesAsync(bb1.GetCleanedPath(path));
        return output;
    }
    public static byte[] ReadAllBytes(string path)
    {
        var output = File.ReadAllBytes(bb1.GetCleanedPath(path));
        return output;
    }

    /// <summary>
    /// This will copy the folder and all sub folders and files.
    /// if the destination already exist, then will exit since this cannot replace.
    /// </summary>
    /// <param name="OldFolder"></param>
    /// <param name="NewFolder"></param>
    /// <returns></returns>
    public static async Task CopyFolderAsync(string sourceFolder, string destFolder)
    {
        await Task.Run(async () =>
        {
            string findest = bb1.GetCleanedPath(destFolder);
            if (Directory.Exists(findest))
            {
                return;
            }
            Directory.CreateDirectory(findest);
            string[] files = Directory.GetFiles(bb1.GetCleanedPath(sourceFolder));
            foreach (string file in files)
            {
                string name = Path.GetFileName(file);
                string dest = Path.Combine(findest, name);
                File.Copy(file, dest);
            }
            string[] folders = Directory.GetDirectories(sourceFolder);
            foreach (string folder in folders)
            {
                string name = Path.GetFileName(folder);
                string dest = Path.Combine(findest, name);
                await CopyFolderAsync(folder, dest);
            }
        });
    }
    public static void FileCopy(string originalFile, string newFile)
    {
        File.Copy(bb1.GetCleanedPath(originalFile), bb1.GetCleanedPath(newFile), true);
    }
    public static void DeleteFolder(string path)
    {
        Directory.Delete(bb1.GetCleanedPath(path), true);
    }
    public static void CreateFolder(string path)
    {
        Directory.CreateDirectory(bb1.GetCleanedPath(path));
    }
    public static void DeleteSeveralFiles(string directoryPath, string stringEnd)
    {
        BasicList<string> thisList = FileList(directoryPath);
        thisList.KeepConditionalItems(Items =>
        Items.ToLower().EndsWith(stringEnd.ToLower()));
        thisList.ForEach(items => DeleteFile(items));
    }
    public static void DeleteFile(string path)
    {
        File.Delete(bb1.GetCleanedPath(path));
    }
    public static void RenameFile(string oldFile, string newName)
    {
        string finold = bb1.GetCleanedPath(oldFile);
        string finnew = bb1.GetCleanedPath(newName);
        if (File.Exists(finold) == false && File.Exists(finnew) && oldFile.Contains("storage/emulated", StringComparison.CurrentCultureIgnoreCase))
        {
            return; //because you already renamed.  i will assume this only for sd card situations.
        }
        try
        {
            File.Move(finold, finnew);
        }
        catch { }
        if (File.Exists(finnew) == false)
        {
            throw new CustomBasicException("Failed to rename file.  Most likely permission problem.  Rethink");
        }
        if (File.Exists(finold))
        {
            File.Delete(finold);
        }
    }
    public static void RenameDirectory(string oldDirectory, string newName)
    {
        Directory.Move(bb1.GetCleanedPath(oldDirectory), bb1.GetCleanedPath(newName));
    }
    public static BasicList<string> ReadAllLines(string filePath)
    {
        return File.ReadAllLines(bb1.GetCleanedPath(filePath)).ToBasicList();
    }
    /// <summary>
    /// This will copy the folder and all sub folders and files.
    /// if the destination already exist, then will exit since this cannot replace.
    /// </summary>
    /// <param name="OldFolder"></param>
    /// <param name="NewFolder"></param>
    /// <returns></returns>
    public static void CopyFolder(string sourceFolder, string destFolder)
    {
        string finsource = bb1.GetCleanedPath(sourceFolder);
        string findest = bb1.GetCleanedPath(destFolder);
        if (Directory.Exists(findest))
        {
            return;
        }
        Directory.CreateDirectory(findest);
        string[] files = Directory.GetFiles(finsource);
        foreach (string file in files)
        {
            string name = Path.GetFileName(file);
            string dest = Path.Combine(findest, name);
            File.Copy(file, dest);
        }
        string[] folders = Directory.GetDirectories(finsource);
        foreach (string folder in folders)
        {
            string name = Path.GetFileName(folder);
            string dest = Path.Combine(destFolder, name);
            CopyFolder(folder, dest);
        }
    }
    public static Stream GetStreamForReading(string filePath)
    {
        return new FileStream(filePath, FileMode.Open, FileAccess.Read);
    }
    public static async Task<string> ResourcesAllTextFromFileAsync(object thisObj, string fileName)
    {
        Assembly thisA = Assembly.GetAssembly(thisObj.GetType())!;
        return await thisA.ResourcesAllTextFromFileAsync(fileName);
    }
    public static async Task<Stream?> ResourcesGetStreamAsync(object thisObj, string fileName)
    {
        Assembly thisA = Assembly.GetAssembly(thisObj.GetType())!;
        return await thisA.ResourcesGetStreamAsync(fileName);
    }
    public static string GetMediaURIFromStream(object thisObj, string fileName)
    {
        Assembly ThisA = Assembly.GetAssembly(thisObj.GetType())!;
        return ThisA.GetMediaURIFromStream(fileName);
    }
    public static void CopyFilesRecursively(string sourcePath, string targetPath)
    {
        foreach (string dirPath in Directory.GetDirectories(sourcePath, "*", SearchOption.AllDirectories))
        {
            Directory.CreateDirectory(dirPath.Replace(sourcePath, targetPath));
        }
        foreach (string newPath in Directory.GetFiles(sourcePath, "*.*", SearchOption.AllDirectories))
        {
            File.Copy(newPath, newPath.Replace(sourcePath, targetPath), true);
        }
    }
}