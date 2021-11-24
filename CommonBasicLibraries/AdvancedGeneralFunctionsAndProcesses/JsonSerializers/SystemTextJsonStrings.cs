﻿using tt = System.Text.Json.JsonSerializer;
namespace CommonBasicLibraries.AdvancedGeneralFunctionsAndProcesses.JsonSerializers
{
    public static class SystemTextJsonStrings
    {
        private static JsonSerializerOptions? _options;
        public static JsonSerializerOptions GetCustomJsonSerializerOptions()
        {
            if (_options == null)
            {
                _options = new ();
                _options.WriteIndented = true;
                //i can do all my converters here when ready.
            }
            return _options;
        }
        public static async Task<string> SerializeObjectAsync(object thisObj)
        {
            string thisStr = "";

            await Task.Run(() =>
            {
                thisStr = tt.Serialize(thisObj, GetCustomJsonSerializerOptions());
            });
            //eventually figure out the advanced json stuff.

            //JsonSettingsGlobals.PopulateSettings();
            //if (JsonSettingsGlobals.NeedsReferences)
            //{
            //    await Task.Run(() => thisStr = JsonConvert.SerializeObject(thisObj, settings: JsonSettingsGlobals._jsonSettingsData));
            //}
            //else
            //{
            //    await Task.Run(() => thisStr = JsonConvert.SerializeObject(thisObj, Formatting.Indented, new JsonDateOnlyConverter()));
            //}
            return thisStr;
        }
        public static async Task<T> DeserializeObjectAsync<T>(string thisStr)
        {
            T thisT = default!;
            await Task.Run(() =>
            {
                thisT = tt.Deserialize<T>(thisStr, GetCustomJsonSerializerOptions())!;
            });

            //JsonSettingsGlobals.PopulateSettings();
            //if (JsonSettingsGlobals.NeedsReferences)
            //{
            //    await Task.Run(() => thisT = JsonConvert.DeserializeObject<T>(thisStr, JsonSettingsGlobals._jsonSettingsData)!);
            //}
            //else
            //{
            //    thisT = JsonConvert.DeserializeObject<T>(thisStr, new JsonDateOnlyConverter())!;
            //}
            return thisT!;
        }
        public static T ConvertObject<T>(object thisObj)
        {
            string thisStr = SerializeObject(thisObj);
            return DeserializeObject<T>(thisStr);
        }
        public static string SerializeObject(object thisObj)
        {
            return tt.Serialize(thisObj, GetCustomJsonSerializerOptions());
            //JsonSettingsGlobals.PopulateSettings();
            //if (JsonSettingsGlobals.NeedsReferences)
            //{
            //    return JsonConvert.SerializeObject(thisObj, JsonSettingsGlobals._jsonSettingsData);
            //}
            //return JsonConvert.SerializeObject(thisObj, Formatting.Indented, new JsonDateOnlyConverter());
        }
        public static T DeserializeObject<T>(string thisStr)
        {
            return tt.Deserialize<T>(thisStr, GetCustomJsonSerializerOptions())!;
            //JsonSettingsGlobals.PopulateSettings();
            //if (JsonSettingsGlobals.NeedsReferences)
            //{
            //    return JsonConvert.DeserializeObject<T>(thisStr, JsonSettingsGlobals._jsonSettingsData)!;
            //}
            //else
            //{
            //    return JsonConvert.DeserializeObject<T>(thisStr, new JsonDateOnlyConverter())!;
            //}
        }
        public static async Task<T> ConvertObjectAsync<T>(object thisObj)
        {
            string thisStr = await SerializeObjectAsync(thisObj);
            return await DeserializeObjectAsync<T>(thisStr);
        }
    }
}
