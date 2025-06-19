using CommonBasicLibraries.AdvancedGeneralFunctionsAndProcesses.JsonSerializers; //not common enough for global usings
namespace CommonBasicLibraries.AdvancedGeneralFunctionsAndProcesses.CssHelpers;
public static class CssJsonInitializer
{
    public static async Task GenerateInitialConfig(string originalPath, BasicList<string> cssFilePaths, BasicList<string>? excludedClasses = null)
    {
        CssToolConfiguration config;
        if (ff1.FileExists(originalPath))
        {
            config = await FileHelpers.RetrieveSavedObjectAsync<CssToolConfiguration>(originalPath);
        }
        else
        {
            config = new();
        }
        if (config.GlobalExcludedClasses.Count == 0)
        {
            //if i already have the list, keep it.
            config.GlobalExcludedClasses = excludedClasses is not null ? [.. excludedClasses] : [];
        }
        foreach (var filePath in cssFilePaths)
        {
            var css = File.ReadAllText(filePath);
            var firsts = CssClassExtractor.ExtractStandaloneClasses(css);
            var patterns = CssClassExtractor.ExtractPatternsFromClasses(firsts);
            var basePath = Path.GetDirectoryName(filePath)!;
            var fileName = Path.GetFileName(filePath);

            // Find existing file config or create new
            var fileConfig = config.Files.FirstOrDefault(f =>
                f.FileName == fileName && f.BasePath == basePath);
            if (fileConfig == null)
            {
                fileConfig = new CssFileConfig
                {
                    BasePath = basePath,
                    FileName = fileName,
                    OutputName = Path.GetFileNameWithoutExtension(fileName) + ".xml",
                };
                config.Files.Add(fileConfig);
            }
            foreach (var pattern in patterns)
            {
                if (fileConfig.Overrides.Any(o => o.OriginalName == pattern) == false)
                {
                    fileConfig.Overrides.Add(new GroupOverride
                    {
                        OriginalName = pattern,
                        DisplayName = pattern // or empty string or default
                    });
                }
            }
        }
        await FileHelpers.SaveObjectAsync(originalPath, config);
    }
}