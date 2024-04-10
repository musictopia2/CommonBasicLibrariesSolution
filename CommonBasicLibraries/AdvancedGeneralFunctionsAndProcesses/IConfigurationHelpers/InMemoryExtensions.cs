﻿namespace CommonBasicLibraries.AdvancedGeneralFunctionsAndProcesses.IConfigurationHelpers;
public static class InMemoryExtensions
{
    public static IConfigurationBuilder AddInMemoryCollection(this IConfigurationBuilder builder, Action<IAdvancedConfiguration> options )
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