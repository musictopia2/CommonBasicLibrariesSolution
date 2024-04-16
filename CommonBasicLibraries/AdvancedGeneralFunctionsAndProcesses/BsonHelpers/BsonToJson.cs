namespace CommonBasicLibraries.AdvancedGeneralFunctionsAndProcesses.BsonHelpers;
public static class BsonToJson
{
    private static void WriteList(BObject payLoad, Utf8JsonWriter writer)
    {
        writer.WriteStartObject();
        foreach (var item1 in payLoad.FullList)
        {
            if (item1.Value.ValueType == EnumBValueType.String)
            {
                writer.WriteString(item1.Key, item1.Value.StringValue);
            }
            else if (item1.Value.ValueType == EnumBValueType.CustomList)
            {
                writer.WriteStartArray(item1.Key);
                var list = (BCustomList)item1.Value;
                foreach (var item2 in list.FullList)
                {
                    if (item2.ValueType == EnumBValueType.Object)
                    {
                        WriteList((BObject)item2, writer);
                    }
                    else if (item2.ValueType == EnumBValueType.String)
                    {
                        writer.WriteStringValue(item2.StringValue);
                    }
                    else
                    {
                        throw new CustomBasicException("Not Supported");
                    }
                }
                writer.WriteEndArray();
            }
            else if (item1.Value.ValueType == EnumBValueType.Int32)
            {
                writer.WriteNumber(item1.Key, item1.Value.Int32Value);
            }
            else if (item1.Value.ValueType == EnumBValueType.Boolean)
            {
                writer.WriteBoolean(item1.Key, item1.Value.BoolValue);
            }
            else
            {
                throw new CustomBasicException("Not Supported");
            }
        }
        writer.WriteEndObject();
    }
    private static void WriteObject(string key, BObject payLoad, Utf8JsonWriter writer)
    {
        writer.WriteStartObject(key);
        foreach (var item1 in payLoad.FullList)
        {
            if (item1.Value.ValueType == EnumBValueType.Object)
            {
                WriteObject(item1.Key, (BObject)item1.Value, writer);
            }
            else if (item1.Value.ValueType == EnumBValueType.CustomList)
            {
                writer.WriteStartArray(item1.Key);
                var list = (BCustomList)item1.Value;
                foreach (var item2 in list.FullList)
                {
                    if (item2.ValueType == EnumBValueType.Object)
                    {
                        WriteList((BObject)item2, writer);
                    }
                    else if (item2.ValueType == EnumBValueType.String)
                    {
                        writer.WriteStringValue(item2.StringValue);
                    }
                    else
                    {
                        throw new CustomBasicException("Not Supported");
                    }
                }
                writer.WriteEndArray();
            }
            else
            {
                throw new CustomBasicException("Not Supported");
            }
        }
        writer.WriteEndObject();
    }
    public static string WriteBackToJson(BObject payLoad)
    {
        using var ms = new MemoryStream();
        using var writer = new Utf8JsonWriter(ms, new JsonWriterOptions()
        {
            Indented = true
        });
        writer.WriteStartObject();
        foreach (var item in payLoad.FullList)
        {
            if (item.Value.ValueType == EnumBValueType.Object)
            {
                WriteObject(item.Key, (BObject)item.Value, writer);
            }
            else if (item.Value.ValueType == EnumBValueType.String)
            {
                writer.WriteString(item.Key, item.Value.StringValue);
            }
            else
            {
                throw new CustomBasicException("Not Supported");
            }
        }
        writer.WriteEndObject();
        writer.Flush();
        string output = Encoding.UTF8.GetString(ms.ToArray());
        ms.Close();
        return output;
    }
}