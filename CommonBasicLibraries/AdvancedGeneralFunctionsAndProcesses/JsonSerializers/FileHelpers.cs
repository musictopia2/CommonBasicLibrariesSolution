using System.Threading.Tasks;
using static CommonBasicLibraries.AdvancedGeneralFunctionsAndProcesses.FileFunctions.FileFunctions;
using js = Newtonsoft.Json.JsonConvert;
namespace CommonBasicLibraries.AdvancedGeneralFunctionsAndProcesses.JsonSerializers
{
    public static class FileHelpers
    {
        public static async Task SaveObjectAsync(string path, object thisObject)
        {
            JsonSettingsGlobals.PopulateSettings();
            string? thisText = default;
            await Task.Run(() => thisText = js.SerializeObject(thisObject, JsonSettingsGlobals._jsonSettingsData));
            await WriteTextAsync(path, thisText!, false);
        }
        public static void SaveObject(string path, object thisObject)
        {
            JsonSettingsGlobals.PopulateSettings(); //look slike saveobject when not doing async had a serious problem.
            string thisText = js.SerializeObject(thisObject, JsonSettingsGlobals._jsonSettingsData);
            WriteText(path, thisText!, false);
        }
        public static async Task<T> RetrieveSavedObjectAsync<T>(string path)
        {
            JsonSettingsGlobals.PopulateSettings();
            T thisT = default!;
            string thisText;
            thisText = await AllTextAsync(path);
            await Task.Run(() =>
            {
                thisT = js.DeserializeObject<T>(thisText, JsonSettingsGlobals._jsonSettingsData)!;
            });
            return thisT!;
        }
        public static T RetrieveSavedObject<T>(string path)
        {
            JsonSettingsGlobals.PopulateSettings();
            string thisText = AllText(path);
            return js.DeserializeObject<T>(thisText, JsonSettingsGlobals._jsonSettingsData)!;
        }
    }
}