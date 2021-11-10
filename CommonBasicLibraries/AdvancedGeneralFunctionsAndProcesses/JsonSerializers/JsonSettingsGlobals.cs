namespace CommonBasicLibraries.AdvancedGeneralFunctionsAndProcesses.JsonSerializers
{
    public static class JsonSettingsGlobals
    {
        static internal JsonSerializerSettings _jsonSettingsData = new();
        static public PreserveReferencesHandling PreserveReferencesHandling { get; set; } = PreserveReferencesHandling.None; 
        static public TypeNameHandling TypeNameHandling { get; set; } = TypeNameHandling.None;
        static internal void PopulateSettings()
        {
            _jsonSettingsData.PreserveReferencesHandling = PreserveReferencesHandling;
            _jsonSettingsData.TypeNameHandling = TypeNameHandling;
            _jsonSettingsData.Formatting = Formatting.Indented;
        }
        internal static bool NeedsReferences => PreserveReferencesHandling != PreserveReferencesHandling.None || TypeNameHandling != TypeNameHandling.None;
    }
}