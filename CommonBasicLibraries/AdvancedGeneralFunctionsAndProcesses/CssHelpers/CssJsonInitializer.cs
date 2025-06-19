namespace CommonBasicLibraries.AdvancedGeneralFunctionsAndProcesses.CssHelpers;
public static class CssJsonInitializer
{
    public static CssToolConfiguration GenerateInitialConfigAsync(BasicList<string> cssFilePaths, BasicList<string>? excludedClasses = null)
    {
        CssToolConfiguration config = new();
        config.GlobalExcludedClasses = excludedClasses is not null ? [.. excludedClasses] : [];
        foreach (var filePath in cssFilePaths)
        {
            var css = File.ReadAllText(filePath);
            var firsts = CssClassExtractor.ExtractStandaloneClasses(css);
            var patterns = CssClassExtractor.ExtractPatternsFromClasses(firsts);
            var fileConfig = new CssFileConfig
            {
                BasePath = Path.GetDirectoryName(filePath)!,
                FileName = Path.GetFileName(filePath),
                OutputName = Path.GetFileNameWithoutExtension(filePath) + ".xml",
            };
            // Add initial GroupOverrides from patterns
            foreach (var pattern in patterns)
            {
                fileConfig.Overrides.Add(new GroupOverride
                {
                    OriginalName = pattern,
                    DisplayName = pattern // User can edit this later
                });
            }
            config.Files.Add(fileConfig);
        }
        return config;
    }
}