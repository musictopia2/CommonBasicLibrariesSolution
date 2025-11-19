namespace CommonBasicLibraries.AdvancedGeneralFunctionsAndProcesses.IConfigurationHelpers;
public static class InMemoryExtensions
{
    extension(IConfigurationBuilder builder)
    {
        public IConfigurationBuilder AddInMemoryCollection(Action<IAdvancedConfiguration> options)
        {
            IAdvancedConfiguration config = new AdvancedConfiguration();
            AdvancedConfiguration.Data = [];
            if (options is null)
            {
                throw new CustomBasicException("Must have a method to return the advanced configuration stuff");
            }
            options.Invoke(config);
            return builder.AddInMemoryCollection(AdvancedConfiguration.Data);
        }
    }
}