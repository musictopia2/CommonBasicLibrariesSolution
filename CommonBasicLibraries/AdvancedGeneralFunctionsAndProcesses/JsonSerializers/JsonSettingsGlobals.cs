using Newtonsoft.Json;
//i think this is the most common things i like to do
namespace CommonBasicLibraries.AdvancedGeneralFunctionsAndProcesses.JsonSerializers
{
    public static class JsonSettingsGlobals
    {
        static internal JsonSerializerSettings _jsonSettingsData = new ();

        static public PreserveReferencesHandling PreserveReferencesHandling { get; set; } = PreserveReferencesHandling.None; //looks like i can preserve references for objects but not arrays  hopefully that works.
        static public TypeNameHandling TypeNameHandling { get; set; } = TypeNameHandling.None; //defaults to it but can change if necessary.

        //will attempt to do none as defaults.
        //warnings.  the music will require changes though.  that is the only exception i found in most cases.
        //otherwise, gets hosed when i have even minor changes.

        //static public PreserveReferencesHandling PreserveReferencesHandling { get; set; } = PreserveReferencesHandling.Objects; //looks like i can preserve references for objects but not arrays  hopefully that works.
        //static public TypeNameHandling TypeNameHandling { get; set; } = TypeNameHandling.All; //defaults to it but can change if necessary.
        static internal void PopulateSettings()
        {
            _jsonSettingsData.PreserveReferencesHandling = PreserveReferencesHandling;
            _jsonSettingsData.TypeNameHandling = TypeNameHandling;
            _jsonSettingsData.Formatting = Formatting.Indented;
        }

    }
}
