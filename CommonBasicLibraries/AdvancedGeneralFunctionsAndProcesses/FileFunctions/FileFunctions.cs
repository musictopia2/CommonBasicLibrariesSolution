using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Reflection;
using CommonBasicLibraries.CollectionClasses;
using CommonBasicLibraries.BasicDataSettingsAndProcesses;
using CommonBasicLibraries.AdvancedGeneralFunctionsAndProcesses.BasicExtensions;
using CommonBasicLibraries.AdvancedGeneralFunctionsAndProcesses.HtmlParserClasses;

namespace CommonBasicLibraries.AdvancedGeneralFunctionsAndProcesses.FileFunctions
{
    public static class FileFunctions
    {

        public static BasicList<string> GetHttpsLinks(string fileSource)
        {
            //HtmlParser parses = new HtmlParser.HtmlParser();
            HtmlParser parses = new();
            parses.Body = AllText(fileSource);
            BasicList<string> temps = parses.GetList("https", Constants.DoubleQuote, showErrors: false); //if not there, then just returns 0 items.
            BasicList<string> output = temps.Select(x => "https" + x).ToBasicList();
            return output; //maybe this simple (not sure).
        }
        public static string GetApplicationDataForMobileDevices()
        {
            return Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData); // i think this is where it should be.
        }
        public static string GetSDCardReadingPathForAndroid() //keey this because we want the ability to sometimes not use local storage for blazor stuff.
        {
            if (DirectoryExists("/storage/sdcard1") == true)
            {
                return "/storage/sdcard1"; // can't assume its always accessing the music folder
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
            return @"/sdcard"; //try this instead of the other.  tried on 2 devices and this worked.  the other did not always work.
            //return "/storage/emulated/0"; // for writing, its always this location.  so for cristina, it will be internal.  for andy, external but the path is still the same
        }
        public static bool FileExists(string filePath)
        {
            return File.Exists(filePath);
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
            return Directory.Exists(directoryPath);
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
                            if (label.ToLower() == temps.VolumeLabel.ToLower())
                            {
                                thisStr = temps.Name.Substring(0, 1);
                            }
                            break;
                        }
                    }   
                }   
            }
            );
            if (thisStr == null)
            {
                throw new CustomBasicException("No label for " + label);
            }
            return thisStr;
        }

        public static async Task FilterDirectoryAsync(BasicList<string> firstList)
        {
            BasicList<string> removeList = new ();
            await Task.Run(() =>
            {
                firstList.ForEach(items =>
                {
                    if (Directory.EnumerateFiles(items).Any() == false)
                    {
                        removeList.Add(items);
                    }
                });

            });
            firstList.RemoveGivenList(removeList); //i think
        }
        public static void FilterDirectory(BasicList<string> firstList)
        {
            BasicList<string> removeList = new ();
            firstList.ForEach(items =>
            {
                if (Directory.EnumerateFiles(items).Any() == false)
                {
                    removeList.Add(items);
                }
            });
            firstList.RemoveGivenList(removeList); //i think
        }
        public static async Task<BasicList<string>> GetDriveListAsync()
        {

            DriveInfo[] completeDrives;
            BasicList<string> newList = new ();
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
            BasicList<string> newList = new ();
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
            var thisDir = Directory.GetParent(thisPath);
            return thisDir!.FullName;
        }
        public static async Task<FileInfo?> GetFileAsync(string filePath)
        {
            System.IO.FileInfo thisFile;
            FileInfo tempFile = default!;
            await Task.Run(() =>
            {
                thisFile = new System.IO.FileInfo(filePath);
                tempFile = new FileInfo()
                {
                    FileSize = thisFile.Length,
                    DateCreated = GetCreatedDate(thisFile),
                    Directory = thisFile.DirectoryName!,
                    DateAccessed = thisFile.LastAccessTime,
                    DateModified = GetModifiedDate(thisFile),
                    FilePath = filePath // this is needed so if i only have the fileinfo, i know the full path.
                };
                if (tempFile.DateModified > tempFile.DateAccessed)
                {
                    tempFile.DateAccessed = tempFile.DateModified;// this means something is wrong.
                }
            });
            return tempFile;
        }
        public static FileInfo? GetFile(string filePath)
        {
            System.IO.FileInfo thisFile;
            FileInfo tempFile;
            thisFile = new System.IO.FileInfo(filePath);
            tempFile = new FileInfo()
            {
                FileSize = thisFile.Length,
                DateCreated = GetCreatedDate(thisFile),
                Directory = thisFile.DirectoryName!,
                DateAccessed = thisFile.LastAccessTime,
                DateModified = GetModifiedDate(thisFile),
                FilePath = filePath // this is needed so if i only have the fileinfo, i know the full path.
            };
            if (tempFile.DateModified > tempFile.DateAccessed)
            {
                tempFile.DateAccessed = tempFile.DateModified;// this means something is wrong.
            }
            return tempFile;
        }

        //since i have more async support now, its best to use more awaits.
        public static string DirectoryName(string directoryPath)
        {
            var thisItem = new DirectoryInfo(directoryPath);
            return thisItem.Name;
        }
        public static string FullFile(string filePath)
        {
            var thisItem = new System.IO.FileInfo(filePath);
            return thisItem.Name;
        }

        public static string FileName(string filePath)
        {
            var thisItem = new System.IO.FileInfo(filePath);
            var thisName = thisItem.Name;
            return thisName.Substring(0, thisName.Length - thisItem.Extension.Length);
        }
        public static BasicList<string> DirectoryList(string whatPath, SearchOption searchOption = SearchOption.TopDirectoryOnly)
        {
            return Directory.EnumerateDirectories(whatPath, "*", searchOption).ToBasicList();
        }
        public static BasicList<string> FileList(string directoryPath, SearchOption thisOption = SearchOption.TopDirectoryOnly)
        {
            return Directory.EnumerateFiles(directoryPath, "*", thisOption).ToBasicList();
        }
        public static BasicList<string> FileList(BasicList<string> directoryList) //this can be just top because you already sent in a directory list
        {
            BasicList<string> newList = new ();
            directoryList.ForEach(x =>
            {
                var temps = FileList(x);
                newList.AddRange(temps);
            });
            return newList;
        }
        public static BasicList<string> GetSeveralSpecificFiles(string directoryPath, string extensionEndsAt, SearchOption searchOption = SearchOption.TopDirectoryOnly) //to not break compatibility
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
            //step 1 is getting the specific files without the good list
            BasicList<string> firstList = GetSeveralSpecificFiles(directoryPath, extensionEndsAt, searchOption);
            firstList.KeepConditionalItems(items =>
            {
                string thisName = FileName(items).ToLower();
                if (goodList.Exists(Temps => Temps.ToLower() == thisName) == true)
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
            var tDirectoryList = new BasicList<string> ();
            await Task.Run(() =>
            {
                tDirectoryList = Directory.EnumerateDirectories(whatPath, "*", searchOption).ToBasicList();
            });
            return tDirectoryList;
        }

        public static async Task<BasicList<string>> FileListAsync(string directoryPath, SearchOption thisOption = SearchOption.TopDirectoryOnly)
        {
            BasicList<string> tFileList = new ();
            await Task.Run(() =>
            {
                tFileList = Directory.EnumerateFiles(directoryPath, "*", thisOption).ToBasicList(); // hopefully will still work
            });
            return tFileList;
        }

        public static async Task<BasicList<string>> FileListAsync(BasicList<string> directoryList) //this can be just top because you already sent in a directory list
        {
            BasicList<string> newList = new ();
            await directoryList.ForEachAsync(async x =>
            {
                var temps = await FileListAsync(x);
                newList.AddRange(temps);
            });
            return newList;
        }
        public static async Task<BasicList<string>> GetSeveralSpecificFilesAsync(string directoryPath, string extensionEndsAt, SearchOption searchOption = SearchOption.TopDirectoryOnly) //to not break compatibility
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
            //step 1 is getting the specific files without the good list
            BasicList<string> firstList = await GetSeveralSpecificFilesAsync(directoryPath, extensionEndsAt, searchOption);
            firstList.KeepConditionalItems(items =>
            {
                string thisName = FileName(items).ToLower();
                if (goodList.Exists(Temps => Temps.ToLower() == thisName) == true)
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
            return tempList.FindOnlyOne(Items => Items.ToLower().EndsWith(extensionEndsAt.ToLower()));
        }
        public static string GetSpecificFile(string directoryPath, string extensionEndsAt)
        {
            var tempList = FileList(directoryPath);
            return tempList.FindOnlyOne(Items => Items.ToLower().EndsWith(extensionEndsAt.ToLower()));
        }
        /// <summary>
        /// This searches for files in not only top directory but sub directories as well
        /// </summary>
        /// <param name="directoryPath">Directory Where To Look</param>
        /// <param name="fileName">File Name Including Extension Needed</param>
        /// <returns></returns>
        public static async Task<string> SearchForFileNameAsync(string directoryPath, string fileName)
        {
            //this time i need first a list of all but even sub folders for it.
            BasicList<string> tFileList = new ();
            await Task.Run(() =>
            {
                tFileList = Directory.EnumerateFiles(directoryPath, fileName, SearchOption.AllDirectories).ToBasicList();
            });
            if (tFileList.Count == 0)
            {
                throw new CustomBasicException($"The File Name {fileName} Was Not Found At {directoryPath} Or Even Sub Directories");
            }
            return tFileList.Single(); //if nothing is found, then will get runtime error
        }
        /// <summary>
        /// This searches for files in not only top directory but sub directories as well
        /// </summary>
        /// <param name="directoryPath">Directory Where To Look</param>
        /// <param name="fileName">File Name Including Extension Needed</param>
        /// <returns></returns>
        public static string SearchForFileName(string directoryPath, string fileName)
        {
            //this time i need first a list of all but even sub folders for it.
            BasicList<string> tFileList;
            tFileList = Directory.EnumerateFiles(directoryPath, fileName, SearchOption.AllDirectories).ToBasicList();
            if (tFileList.Count == 0)
            {
                throw new CustomBasicException($"The File Name {fileName} Was Not Found At {directoryPath} Or Even Sub Directories");
            }
            return tFileList.Single(); //if nothing is found, then will get runtime error
        }
        private static async Task<string> PrivateAllTextAsync(string filePath, Encoding encodes)
        {
            string thisText;
            using (StreamReader s = new(filePath, encodes))
            {
                thisText = await s.ReadToEndAsync();
                s.Close();
            }
            return thisText;
        }
        private static string PrivateAllText(string filePath, Encoding encodes)
        {
            string thisText;
            using (StreamReader s = new (filePath, encodes))
            {
                thisText = s.ReadToEnd();
                s.Close();
            }
            return thisText;
        }
        private static async Task PrivateWriteAllTextAsync(string filePath, string text, bool append, Encoding encoding, bool isLine = false)
        {
            using StreamWriter w = new (filePath, append, encoding);
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
            using StreamWriter w = new (filePath, append, encoding);
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
        public static async Task<string> AllTextAsync(string filePath)
        {
            return await PrivateAllTextAsync(filePath, Encoding.Default); //i think
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
            return PrivateAllText(filePath, Encoding.Default); //i think
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
            File.WriteAllLines(filePath, lines);
        }
        public static void WriteAllLines(string filePath, BasicList<string> lines, Encoding encoding)
        {
            File.WriteAllLines(filePath, lines, encoding);
        }
        public static async Task FileCopyAsync(string originalFile, string newFile)
        {
            await Task.Run(() => File.Copy(originalFile, newFile, true));
        }

        public static async Task DeleteFolderAsync(string path) //i like just delete folder instead
        {
            await Task.Run(() => Directory.Delete(path));
        }

        public static async Task CreateFolderAsync(string path)
        {
            await Task.Run(() => Directory.CreateDirectory(path));
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
            await Task.Run(() => File.Delete(path));
        }
        public static async Task RenameFileAsync(string oldFile, string newName)
        {
            if (File.Exists(oldFile) == false && File.Exists(newName) && oldFile.ToLower().Contains("storage/emulated"))
            {
                return; //because you already renamed.  i will assume this only for sd card situations.
            }
            await Task.Run(() =>
            {
                try
                {
                    File.Move(oldFile, newName);
                }
                catch
                {

                }
                if (File.Exists(newName) == false)
                {
                    throw new CustomBasicException("Failed to rename file.  Most likely permission problem.  Rethink");
                }
                if (File.Exists(oldFile))
                {
                    File.Delete(oldFile);
                }
            });
        }

        public static async Task RenameDirectoryAsync(string oldDirectory, string newName)
        {
            await Task.Run(() => Directory.Move(oldDirectory, newName));
        }

        public static async Task<BasicList<string>> ReadAllLinesAsync(string filePath)
        {
            var temps = await File.ReadAllLinesAsync(filePath);
            return temps.ToBasicList();
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
                if (Directory.Exists(destFolder))
                {
                    return;
                }
                Directory.CreateDirectory(destFolder); //most of the time, can't copy if files are already there.
                string[] files = Directory.GetFiles(sourceFolder);
                foreach (string file in files)
                {
                    string name = Path.GetFileName(file);
                    string dest = Path.Combine(destFolder, name);
                    File.Copy(file, dest);
                }
                string[] folders = Directory.GetDirectories(sourceFolder);
                foreach (string folder in folders)
                {
                    string name = Path.GetFileName(folder);
                    string dest = Path.Combine(destFolder, name);
                    await CopyFolderAsync(folder, dest);
                }
            });
        }
        public static void FileCopy(string originalFile, string newFile)
        {
            File.Copy(originalFile, newFile, true);
        }

        public static void DeleteFolder(string path) //i like just delete folder instead
        {
            Directory.Delete(path);
        }
        public static void CreateFolder(string path)
        {
            Directory.CreateDirectory(path);
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
            File.Delete(path);
        }
        public static void RenameFile(string oldFile, string newName)
        {
            if (File.Exists(oldFile) == false && File.Exists(newName) && oldFile.ToLower().Contains("storage/emulated"))
                return; //because you already renamed.  i will assume this only for sd card situations.
            try
            {
                File.Move(oldFile, newName);
            }
            catch { }
            if (File.Exists(newName) == false)
            {
                throw new CustomBasicException("Failed to rename file.  Most likely permission problem.  Rethink");
            }
            if (File.Exists(oldFile))
            {
                File.Delete(oldFile);
            }
        }
        public static void RenameDirectory(string oldDirectory, string newName)
        {
            Directory.Move(oldDirectory, newName);
        }

        public static BasicList<string> ReadAllLines(string filePath)
        {
            return File.ReadAllLines(filePath).ToBasicList();
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
            if (Directory.Exists(destFolder))
            {
                return;
            }
            Directory.CreateDirectory(destFolder); //most of the time, can't copy if files are already there.

            string[] files = Directory.GetFiles(sourceFolder);
            foreach (string file in files)
            {
                string name = Path.GetFileName(file);
                string dest = Path.Combine(destFolder, name);
                File.Copy(file, dest);
            }
            string[] folders = Directory.GetDirectories(sourceFolder);
            foreach (string folder in folders)
            {
                string name = Path.GetFileName(folder);
                string dest = Path.Combine(destFolder, name);
                CopyFolder(folder, dest);
            }
        }

        public static Stream GetStreamForReading(string filePath) // some classes require the actual stream.  therefore will use this
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
    }
}