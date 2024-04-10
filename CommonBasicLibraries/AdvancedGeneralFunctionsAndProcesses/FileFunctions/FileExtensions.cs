namespace CommonBasicLibraries.AdvancedGeneralFunctionsAndProcesses.FileFunctions;
public static class FileExtensions
{
    public static string ResourceLocation = "Resources"; //c# requires the actual folder where you place the resource to read the embedded resource.
    public static async Task<string?> FixBase64ForFileDataAsync(this string str_Image)
    {
        // *** Need to clean up the text in case it got corrupted travelling in an XML file
        // i think its best to have as public.  because its possible its only corrupted because of this.
        // has had the experience before with smart phones.
        // however; with mango and windows phones 7; I can use a compact edition database (which would be very helpful).
        // if doing this; then what would have to happen is I would have to have a method to check back in the music information.
        // maybe needs to be xml afterall (don't know though).  otherwise; may have to do serializing/deserializing.
        // some stuff is iffy at this point.
        // at this point, its proven to work.  so i will keep.  this will be good even for iphones/androids, other operating systems even
        StringBuilder sbText;
        string? thisStr = default;
        await Task.Run(() =>
        {
            sbText = new StringBuilder(str_Image, str_Image.Length);
            sbText.Replace(@"\r\n", string.Empty);
            sbText.Replace(" ", string.Empty);
            thisStr = sbText.ToString();
        });
        return thisStr;
    }
    public static string? FixBase64ForFileData(this string str_Image)
    {
        StringBuilder sbText;
        string? thisStr;
        sbText = new StringBuilder(str_Image, str_Image.Length);
        sbText.Replace(@"\r\n", string.Empty);
        sbText.Replace(" ", string.Empty);
        thisStr = sbText.ToString();
        return thisStr;
    }
    private static async Task<Stream?> GetStreamAsync(Assembly thisAssembly, string fileName)
    {
        Stream? thisStream = default;
        await Task.Run(() =>
        {
            if (fileName.Contains('/') == true || fileName.Contains('\\') == true)
            {
                throw new CustomBasicException(@"Cannot contain the / or \ in the file name.   Its already smart enough to figure out even if put in folders", null!);
            }
            var thisList = thisAssembly.GetManifestResourceNames();
            var firstName = thisAssembly.GetName().Name;
            firstName = firstName!.Replace(" ", "_");
            string internalPath;
            if (ResourceLocation == "")
            {
                internalPath = firstName + "." + fileName;
            }
            else
            {
                internalPath = firstName + "." + ResourceLocation + "." + fileName;
            }
            thisStream = thisAssembly.GetManifestResourceStream(internalPath);
            if (thisStream == null)
            {
                throw new FileNotFoundException(fileName + " does not exist");
            }
        });
        return thisStream;
    }
    public static string GetMediaURIFromStream(this Assembly thisAssembly, string fileName)
    {
        if (fileName.Contains('/') == true || fileName.Contains('\\') == true)
        {
            throw new CustomBasicException(@"Cannot contain the / or \ in the file name.   Its already smart enough to figure out even if put in folders", null!);
        }
        var firstName = thisAssembly.GetName().Name;
        firstName = firstName!.Replace(" ", "_");
        string internalPath;
        internalPath = firstName + "." + fileName;
        return internalPath;
    }
    public static string ResourcesBinaryTextFromFile(this Assembly assembly, string fileName)
    {
        using Stream stream = assembly.ResourcesGetStream(fileName);
        byte[] bb = new byte[stream.Length - 1 + 1];
        stream.Read(bb, 0, (int)stream.Length);
        stream.Close();
        return Convert.ToBase64String(bb);
    }
    public static async Task<string> ResourcesBinaryTextFromFileAsync(this Assembly assembly, string fileName)
    {
        using var stream = await GetStreamAsync(assembly, fileName);
        byte[] bb = new byte[stream!.Length - 1 + 1];
        await stream.ReadAsync(bb.AsMemory(0, (int)stream.Length));
        stream.Close();
        return Convert.ToBase64String(bb);
    }
    public static async Task<string> ResourcesAllTextFromFileAsync(this Assembly thisAssembly, string fileName)
    {
        using var thisStream = await GetStreamAsync(thisAssembly, fileName);
        using var thisRead = new StreamReader(thisStream!);
        return await thisRead.ReadToEndAsync();
    }

    public static string ResourcesAllTextFromFile(this Assembly thisAssembly, string fileName)
    {
        using var thisStream = thisAssembly.ResourcesGetStream(fileName);
        using var thisRead = new StreamReader(thisStream);
        return thisRead.ReadToEnd();
    }
    public static async Task<Stream?> ResourcesGetStreamAsync(this Assembly thisAssembly, string fileName)
    {
        return await GetStreamAsync(thisAssembly, fileName);
    }
    public static Stream ResourcesGetStream(this Assembly thisAssembly, string fileName)
    {
        if (fileName.Contains('/') == true || fileName.Contains('\\') == true)
        {
            throw new CustomBasicException(@"Cannot contain the / or \ in the file name.   Its already smart enough to figure out even if put in folders", null!);
        }
        var firstName = thisAssembly.GetName().Name; // needs 2 things affterall.  looks like simplier in .net standard 2.0
        firstName = firstName!.Replace(" ", "_");
        string internalPath;
        if (ResourceLocation == "")
        {
            internalPath = firstName + "." + fileName;
        }
        else
        {
            internalPath = firstName + "." + ResourceLocation + "." + fileName;
        }
        Stream? thisStream = thisAssembly.GetManifestResourceStream(internalPath) ?? throw new FileNotFoundException(fileName + " does not exist");
        return thisStream;
    }
    public async static Task SaveBinaryDataAsync(this string data, string path)
    {
        using var fins = new FileStream(bb1.GetCleanedPath(path), FileMode.Create);
        byte[] Bytes = Convert.FromBase64String(data);
        await fins.WriteAsync(Bytes);
        await fins.FlushAsync();
    }
}