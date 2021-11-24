namespace CommonBasicLibraries.AdvancedGeneralFunctionsAndProcesses.JsonSerializers
{
    public static class FileHelpers
    {
        public static async Task SaveObjectAsync(string path, object thisObject)
        {
            //figure out the settings stuff (?)
            string thisText = await js.SerializeObjectAsync(thisObject);
            //JsonSettingsGlobals.PopulateSettings();
            //string? thisText = default;
            //if (JsonSettingsGlobals.NeedsReferences)
            //{
            //    await Task.Run(() => thisText = jo.SerializeObject(thisObject, JsonSettingsGlobals._jsonSettingsData));
            //}
            //else
            //{
            //    await Task.Run(() => thisText = jo.SerializeObject(thisObject, Newtonsoft.Json.Formatting.Indented, new JsonDateOnlyConverter()));
            //}
            await ff.WriteTextAsync(path, thisText!, false);
        }
        public static void SaveObject(string path, object thisObject)
        {
            string text = js.SerializeObject(thisObject);
            //JsonSettingsGlobals.PopulateSettings(); //look slike saveobject when not doing async had a serious problem.
            //string text;
            //if (JsonSettingsGlobals.NeedsReferences)
            //{
            //    text = jo.SerializeObject(thisObject, JsonSettingsGlobals._jsonSettingsData);
            //}
            //else
            //{
            //    text = jo.SerializeObject(thisObject, Newtonsoft.Json.Formatting.Indented, new JsonDateOnlyConverter());
            //}
            ff.WriteText(path, text!, false);
        }
        public static async Task<T> RetrieveSavedObjectAsync<T>(string path)
        {
            //JsonSettingsGlobals.PopulateSettings();
            T thisT = default!;
            string thisText;
            thisText = await ff.AllTextAsync(path);
            thisT = await js.DeserializeObjectAsync<T>(thisText);
            //await Task.Run(() =>
            //{
            //    if (JsonSettingsGlobals.NeedsReferences)
            //    {
            //        thisT = jo.DeserializeObject<T>(thisText, JsonSettingsGlobals._jsonSettingsData)!;
            //    }
            //    else
            //    {
            //        thisT = jo.DeserializeObject<T>(thisText, new JsonDateOnlyConverter())!;
            //    }
            //});
            return thisT!;
        }
        public static T RetrieveSavedObject<T>(string path)
        {
            //JsonSettingsGlobals.PopulateSettings();
            string thisText = ff.AllText(path);
            return js.DeserializeObject<T>(thisText);


            //if (JsonSettingsGlobals.NeedsReferences)
            //{
            //    return jo.DeserializeObject<T>(thisText, JsonSettingsGlobals._jsonSettingsData)!;
            //}
            //else
            //{
            //    return jo.DeserializeObject<T>(thisText, new JsonDateOnlyConverter())!;
            //}
        }
    }
}