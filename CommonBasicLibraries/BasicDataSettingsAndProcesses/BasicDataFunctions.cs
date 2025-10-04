namespace CommonBasicLibraries.BasicDataSettingsAndProcesses;
public static class BasicDataFunctions
{
    public enum EnumOS
    {
        None, WindowsDT, Android, Linux, Macintosh, Wasm, WindowsMaui //this means can even account for webassembly.  we even need windows maui because you may have to do something different in that situation (?)
    }
    //even containers can show windows desktop because its running on my desktop and using my resources.  that is why there is new function for docker.
    public static EnumOS OS { get; set; } = EnumOS.None; //now has to start with none.  can't assume anymore.
    //you have to somehow populate this now.  does not fit using standard di.  if i decided to do standard di, has to put into here.
    public static ISQLServer? SQLServerConnectionClass { get; set; }
    public static IEmojiParserHook? EmojiParserHook { get; set; }
    public static IConfiguration? Configuration { get; set; } //i think best to populate here so if using from console, don't have to hook up dependency injection
    //looks like i need my random stuff now.
    //may need to rethink the random stuff now.
    //this may be needed afterall.
    public delegate Task ActionAsync<in T>(T obj); //this is used so if there is a looping, then it can await it if needed.
    public delegate void UpdateFunct<T>(T thisObj, int expectedItem); //the purpose of this is so i can update.  for this to work, the list has to determine the proper value
    public delegate void ErrorRaisedEventHandler(string message); //this is when i want to make it clear its error data.  anything can use this.  used for media processes
    //i like it done this way i think.  allows anybody to override and figure out themselves if they want special code that only runs in docker situations.
    public static Func<bool> IsDocker { get; set; } = () => false;
    //default is does nothing.  only when its docker would it do anything else.  i already do the function.  might as well do this as well.
    public static Func<string, string> GetCleanedPath { get; set; } = (path) => path;
    public static bool AutoUseMainContainer { get; set; } = true;
    public static bool SetProperty<T>(ref T backingStore, T value)
    {
        if (EqualityComparer<T>.Default.Equals(backingStore, value))
        {
            return false;
        }
        backingStore = value;
        return true;
    }
    public static void SetupAdvancedIConfiguration(Action<IAdvancedConfiguration> options)
    {
        ConfigurationBuilder build = new();
        build.AddInMemoryCollection(options);
        Configuration = build.Build();
    }
    public static void SetupStandardIConfiguration(Action<ConfigurationBuilder> options)
    {
        ConfigurationBuilder build = new();
        options.Invoke(build);
        Configuration = build.Build();
    }
    public static void SetupIConfiguration()
    {
        ConfigurationBuilder build = new();
        Configuration = build.Build();
    }
    //this allows me to add advanced stuff to standard iconfiguration.  since sometimes we need to use the standard but add advanced stuff to it.
    public static void SetupAdvancedIConfiguration(ConfigurationManager config, Action<IAdvancedConfiguration> options)
    {
        config.AddInMemoryCollection(options);
        Configuration = config; //i think.
    }
    public static string OcrDataPath { get; set; } = "";
    public static string OcrLanguage { get; set; } = "eng";
}