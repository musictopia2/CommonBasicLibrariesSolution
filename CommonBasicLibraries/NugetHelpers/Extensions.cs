namespace CommonBasicLibraries.NugetHelpers;
public static class Extensions
{
    public static async Task<string> GetCsProjAsync(this string directoryPath) => await ff1.GetSpecificFileAsync(directoryPath, ".csproj");
    public static DateTime GetDateModified(this string dllPath)
    {
        var file = ff1.GetFile(dllPath);
        return file!.DateModified;
    }
    //try to not have this method anymore.  not sure if still needed.

    //public static string GetNet6DllPath(this INugetModel project) //this is a temporary method.  eventually may create another library to better figure this out.
    //{
    //    string tempName = $"{ff1.FileName(project.CSPath)}.dll";
    //    string dllPath = $@"{project.ProjectDirectory}\bin\release\net6.0\{tempName}";
    //    string altPath = $@"{project.ProjectDirectory}\bin\release\net6.0-windows\{tempName}";
    //    if (ff1.FileExists(altPath))
    //    {
    //        dllPath = altPath; //this allows the workaround for the windows stuff1.
    //    }
    //    altPath = $@"{project.ProjectDirectory}\bin\release\net6.0-windows10.0.19041.0\{tempName}";
    //    if (ff1.FileExists(altPath))
    //    {
    //        dllPath = altPath; //to support windows 10 (for blazor wpf).
    //    }
    //    if (ff1.FileExists(dllPath) == false)
    //    {
    //        throw new CustomBasicException($"Dll does not even exist at {dllPath}");
    //    }
    //    return dllPath;
    //}
    public static int GetMajorVersion(this string payLoad)
    {
        BasicList<string> output = payLoad.Split(".").ToBasicList();
        return int.Parse(output.First());
    }
    public static int GetMinorVersion(this string payLoad)
    {
        BasicList<string> output = payLoad.Split(".").ToBasicList();
        return int.Parse(output.Last());
    }
    public static string IncrementMinorVersion(this string payLoad)
    {
        if (payLoad == "")
        {
            throw new CustomBasicException("For now, am unable to increment minor version because none is given.  May later do something else");
        }
        int minor = GetMinorVersion(payLoad);
        int major = GetMajorVersion(payLoad);
        minor++;
        return $"{major}.0.{minor}";
    }
    internal static bool HasCom(this string projectPath)
    {
        string text = ff1.AllText(projectPath);
        HtmlParser parses = new();
        parses.Body = text;
        return parses.DoesExist("COMReference Include=");
    }
    public static async Task SaveUpdatedVersionToCsProjAsync(this IProjectModel project)
    {
        string text = await ff1.AllTextAsync(project.CSPath);
        HtmlParser parses = new();
        parses.Body = text;
        string previous = parses.GetSomeInfo("<Version>", "</Version>");
        text = text.Replace($"<Version>{previous}</Version>", $"<Version>{project.LastVersion}</Version>");
        await ff1.WriteAllTextAsync(project.CSPath, text);
    }
    public static string GetCsVersionFromCsProj(this string text)
    {
        HtmlParser parses = new();
        parses.Body = text;
        string lookFor = "<Version>";
        if (parses.DoesExist(lookFor) == false)
        {
            return ""; //this means blank.  so something else can decide what to do about it.
        }
        return parses.GetSomeInfo("<Version>", "</Version>");
    }
}