namespace CommonBasicLibraries.AdvancedGeneralFunctionsAndProcesses.JsonSerializers;
public static class JsonConverterRegistrationManager
{
    private static readonly BasicList<Action<JsonSerializerOptions>> _registrations = new();

    /// <summary>
    /// Adds a strongly typed registration step.
    /// </summary>
    public static void Register(Action<JsonSerializerOptions> registration)
    {
        _registrations.Add(registration);
    }

    /// <summary>
    /// Applies all registered actions to the provided options object.
    /// </summary>
    public static void ApplyAll(JsonSerializerOptions options)
    {
        foreach (var reg in _registrations)
        {
            reg(options);
        }
    }
    public static void Clear()
    {
        _registrations.Clear(); //so if i was loading another project can clear out as well (means registering again).
    }
}
