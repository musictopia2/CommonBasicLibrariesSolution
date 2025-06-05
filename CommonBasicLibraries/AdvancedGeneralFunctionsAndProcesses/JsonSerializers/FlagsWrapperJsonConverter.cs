namespace CommonBasicLibraries.AdvancedGeneralFunctionsAndProcesses.JsonSerializers;
public class FlagsWrapperJsonConverter<TEnum> : JsonConverter<FlagsWrapper<TEnum>> where TEnum : struct, Enum
{
    public override FlagsWrapper<TEnum> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        ulong rawValue = reader.GetUInt64();
        var wrapper = new FlagsWrapper<TEnum>();
        wrapper.SetRawValue(rawValue);
        return wrapper;
    }
    public override void Write(Utf8JsonWriter writer, FlagsWrapper<TEnum> value, JsonSerializerOptions options)
    {
        writer.WriteNumberValue(value.RawValue);
    }
}