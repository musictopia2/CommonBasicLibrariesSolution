namespace CommonBasicLibraries.AdvancedGeneralFunctionsAndProcesses.IConfigurationHelpers;
public static class TextConfigurationExtensions
{
    //decided to go ahead and require the common functions to embrace their system.
    //otherwise, i have to create another library and not sure what to call this other one though.
    public static IConfigurationBuilder AddTextFile(this IConfigurationBuilder builder, Action<TextConfigurationSource>? action)
    {
        return builder.Add(action);
    }
    public static IConfigurationBuilder AddTextFile(this IConfigurationBuilder builder, string path)
    {
        return builder.AddTextFile(provider: null, path: path, delimiter: dd1.VBTab, optional: true, reloadOnChange: false); //i do like the fact that if not specified, then its optional.  so if source code contains my path for iconfig, then will ignore when does not exist
    }
    public static IConfigurationBuilder AddTextFile(this IConfigurationBuilder builder, string path, string delimiter)
    {
        return builder.AddTextFile(provider: null, path: path, delimiter: delimiter, optional: true, reloadOnChange: false); //i do like the fact that if not specified, then its optional.  so if source code contains my path for iconfig, then will ignore when does not exist
    }
    public static IConfigurationBuilder AddTextFile(this IConfigurationBuilder builder, string path, bool optional)
    {
        return builder.AddTextFile(provider: null, path: path, delimiter: dd1.VBTab, optional: optional, reloadOnChange: false); //i do like the fact that if not specified, then its optional.  so if source code contains my path for iconfig, then will ignore when does not exist
    }
    public static IConfigurationBuilder AddTextFile(this IConfigurationBuilder builder, string path, string delimiter, bool optional)
    {
        return builder.AddTextFile(provider: null, path: path, delimiter: delimiter, optional: optional, reloadOnChange: false); //i do like the fact that if not specified, then its optional.  so if source code contains my path for iconfig, then will ignore when does not exist
    }
    public static IConfigurationBuilder AddTextFile(this IConfigurationBuilder builder, string path, bool optional, bool reloadOnChange)
    {
        return builder.AddTextFile(provider: null, path: path, delimiter: dd1.VBTab, optional: optional, reloadOnChange: reloadOnChange); //i do like the fact that if not specified, then its optional.  so if source code contains my path for iconfig, then will ignore when does not exist
    }
    public static IConfigurationBuilder AddTextFile(this IConfigurationBuilder builder, string path, string delimiter, bool optional, bool reloadOnChange)
    {
        return builder.AddTextFile(provider: null, path: path, delimiter: delimiter, optional: optional, reloadOnChange: reloadOnChange); //i do like the fact that if not specified, then its optional.  so if source code contains my path for iconfig, then will ignore when does not exist
    }
    public static IConfigurationBuilder AddTextFile(this IConfigurationBuilder builder, IFileProvider? provider, string path, string delimiter, bool optional, bool reloadOnChange)
    {
        return builder.AddTextFile(s =>
        {
            s.Path = path;
            s.Delimiter = delimiter;
            s.FileProvider = provider;
            s.Optional = optional;
            s.ReloadOnChange = reloadOnChange;
            s.ResolveFileProvider(); //forgot this one.
        });
    }
    public static IConfigurationBuilder AddTextFile(this IConfigurationBuilder builder, Stream stream)
    {
        return builder.AddTextFile(stream: stream, delimiter: dd1.VBTab);
    }
    public static IConfigurationBuilder AddTextFile(this IConfigurationBuilder builder, Stream stream, string delimiter)
    {
        return builder.Add<TextStreamConfigurationSource>(s =>
        {
            s.Stream = stream;
            s.Delimiter = delimiter;
        });
    }
}