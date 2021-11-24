namespace CommonBasicLibraries.AdvancedGeneralFunctionsAndProcesses.JsonSerializers
{
    //public static class NewtonJsonStrings
    //{
    //    public static async Task<string> SerializeObjectAsync(object thisObj)
    //    {
    //        string thisStr = "";
    //        JsonSettingsGlobals.PopulateSettings();
    //        if (JsonSettingsGlobals.NeedsReferences)
    //        {
    //            await Task.Run(() => thisStr = JsonConvert.SerializeObject(thisObj, settings: JsonSettingsGlobals._jsonSettingsData));
    //        }
    //        else
    //        {
    //            await Task.Run(() => thisStr = JsonConvert.SerializeObject(thisObj, Formatting.Indented, new JsonDateOnlyConverter()));
    //        }
    //        return thisStr;
    //    }
    //    public static async Task<T> DeserializeObjectAsync<T>(string thisStr)
    //    {
    //        T thisT = default!;
    //        JsonSettingsGlobals.PopulateSettings();
    //        if (JsonSettingsGlobals.NeedsReferences)
    //        {
    //            await Task.Run(() => thisT = JsonConvert.DeserializeObject<T>(thisStr, JsonSettingsGlobals._jsonSettingsData)!);
    //        }
    //        else
    //        {
    //            thisT = JsonConvert.DeserializeObject<T>(thisStr, new JsonDateOnlyConverter())!;
    //        }
    //        return thisT!;
    //    }
    //    public static T ConvertObject<T>(object thisObj)
    //    {
    //        string thisStr = SerializeObject(thisObj);
    //        return DeserializeObject<T>(thisStr);
    //    }
    //    public static string SerializeObject(object thisObj)
    //    {
    //        JsonSettingsGlobals.PopulateSettings();
    //        if (JsonSettingsGlobals.NeedsReferences)
    //        {
    //            return JsonConvert.SerializeObject(thisObj, JsonSettingsGlobals._jsonSettingsData);
    //        }
    //        return JsonConvert.SerializeObject(thisObj, Formatting.Indented, new JsonDateOnlyConverter());
    //    }
    //    public static T DeserializeObject<T>(string thisStr)
    //    {
    //        JsonSettingsGlobals.PopulateSettings();
    //        if (JsonSettingsGlobals.NeedsReferences)
    //        {
    //            return JsonConvert.DeserializeObject<T>(thisStr, JsonSettingsGlobals._jsonSettingsData)!;
    //        }
    //        else
    //        {
    //            return JsonConvert.DeserializeObject<T>(thisStr, new JsonDateOnlyConverter())!;
    //        }
    //    }
    //    public static async Task<T> ConvertObjectAsync<T>(object thisObj)
    //    {
    //        string thisStr = await SerializeObjectAsync(thisObj);
    //        return await DeserializeObjectAsync<T>(thisStr);
    //    }
    //}
}