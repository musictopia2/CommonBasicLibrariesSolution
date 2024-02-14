namespace CommonBasicLibraries.AdvancedGeneralFunctionsAndProcesses.IConfigurationHelpers;
public static class InMemoryExtensions
{
    public static IConfigurationBuilder AddInMemoryCollection(this IConfigurationBuilder builder, Action<IAdvancedConfiguration> options )
    {
        IAdvancedConfiguration config = new AdvancedConfiguration();
        AdvancedConfiguration.Data = []; //do this way.
        if (options is null)
        {
            throw new CustomBasicException("Must have a method to return the advanced configuration stuff");
        }
        options.Invoke(config); //i think this can be fine after all.
        //config = options.Invoke(config);
        return builder.AddInMemoryCollection(AdvancedConfiguration.Data);
    }
}