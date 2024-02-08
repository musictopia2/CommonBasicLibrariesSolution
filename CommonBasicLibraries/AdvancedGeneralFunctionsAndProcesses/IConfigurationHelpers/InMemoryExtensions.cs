namespace CommonBasicLibraries.AdvancedGeneralFunctionsAndProcesses.IConfigurationHelpers;
public static class InMemoryExtensions
{
    public static IConfigurationBuilder AddInMemoryCollection(this IConfigurationBuilder builder, Func<IAdvancedConfiguration, IAdvancedConfiguration> options )
    {
        //KeyValuePair key = new
        IAdvancedConfiguration config = new AdvancedConfiguration();
        AdvancedConfiguration.Data = []; //do this way.
        if (options is null)
        {
            throw new CustomBasicException("Must have a method to return the advanced configuration stuff");
        }
        config = options.Invoke(config);
        return builder.AddInMemoryCollection(AdvancedConfiguration.Data);
    }
}