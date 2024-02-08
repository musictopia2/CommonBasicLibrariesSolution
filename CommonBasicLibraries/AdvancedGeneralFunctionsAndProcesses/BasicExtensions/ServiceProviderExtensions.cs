namespace CommonBasicLibraries.AdvancedGeneralFunctionsAndProcesses.BasicExtensions;
public static class ServiceProviderExtensions
{
    //this is common to use now.  since i could use from console apps.
    public static void SetUpConfiguration(this IServiceProvider provider)
    {
        bb1.Configuration = provider.GetRequiredService<IConfiguration>();
    }
}