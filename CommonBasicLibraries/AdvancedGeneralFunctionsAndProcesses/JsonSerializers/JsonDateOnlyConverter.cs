namespace CommonBasicLibraries.AdvancedGeneralFunctionsAndProcesses.JsonSerializers
{
    public class JsonDateOnlyConverter : JsonConverter<DateOnly> //will use the new converter.
    {
        public override DateOnly ReadJson(JsonReader reader, Type objectType, DateOnly existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            BasicList<string> list = reader.Value!.ToString()!.Split("/").ToBasicList();
            if (list.Count is not 3)
            {
                throw new CustomBasicException("Failed to parse DateOnly");
            }
            int month = int.Parse(list.First());
            int day = int.Parse(list[1]);
            int year = int.Parse(list.Last());
            return new DateOnly(year, month, day);
        }
        public override void WriteJson(JsonWriter writer, DateOnly value, JsonSerializer serializer)
        {
            writer.WriteValue(value.ToString());
        }
    }
}