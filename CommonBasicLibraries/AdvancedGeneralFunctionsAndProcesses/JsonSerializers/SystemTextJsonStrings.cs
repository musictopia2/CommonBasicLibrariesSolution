using tt = System.Text.Json.JsonSerializer;
namespace CommonBasicLibraries.AdvancedGeneralFunctionsAndProcesses.JsonSerializers
{
    public static class SystemTextJsonStrings
    {
        public static void AddContext<T>()
            where T: JsonSerializerContext, new()
        {
            JsonSerializerOptions options = GetCustomJsonSerializerOptions();
            options.AddContext<T>();
        }

        private static JsonSerializerOptions? _options;
        public static JsonSerializerOptions GetCustomJsonSerializerOptions()
        {
            if (_options == null)
            {
                _options = new ();
                _options.WriteIndented = true;
                _options.Converters.Add(new JsonDateOnlyConverter());
                _options.Converters.Add(new LimitedMappableConverter());
                _options.Converters.Add(new JsonTimeOnlyConverter());
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
            return thisStr;
        }
        public static async Task<T> DeserializeObjectAsync<T>(string thisStr)
        {
            T thisT = default!;
            await Task.Run(() =>
            {
                thisT = tt.Deserialize<T>(thisStr, GetCustomJsonSerializerOptions())!;
            });
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
        }
        public static T DeserializeObject<T>(string thisStr)
        {
            return tt.Deserialize<T>(thisStr, GetCustomJsonSerializerOptions())!;
        }
        public static async Task<T> ConvertObjectAsync<T>(object thisObj)
        {
            string thisStr = await SerializeObjectAsync(thisObj);
            return await DeserializeObjectAsync<T>(thisStr);
        }
    }
}