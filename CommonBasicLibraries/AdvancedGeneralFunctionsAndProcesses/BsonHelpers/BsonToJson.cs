namespace CommonBasicLibraries.AdvancedGeneralFunctionsAndProcesses.BsonHelpers;
public static class BsonToJson
{
    private static void WriteList(BObject payLoad, Utf8JsonWriter writer)
    {
        writer.WriteStartObject();
        foreach (var item in payLoad.FullList)
        {
            if (item.Value.ValueType == EnumBValueType.String)
            {
                writer.WriteString(item.Key, item.Value.StringValue);
            }
            else if (item.Value.ValueType == EnumBValueType.CustomList)
            {
                FinishArray(item.Key, (BCustomList)item.Value, writer);
            }
            else if (item.Value.ValueType == EnumBValueType.Int32)
            {
                writer.WriteNumber(item.Key, item.Value.Int32Value);
            }
            else if (item.Value.ValueType == EnumBValueType.Boolean)
            {
                writer.WriteBoolean(item.Key, item.Value.BoolValue);
            }
            else
            {
                throw new CustomBasicException("Not Supported");
            }
        }
        writer.WriteEndObject();
    }
    private static void FinishArray(string key, BCustomList list, Utf8JsonWriter writer)
    {
        writer.WriteStartArray(key);
        foreach (var item in list.FullList)
        {
            if (item.ValueType == EnumBValueType.Object)
            {
                WriteList((BObject)item, writer);
            }
            else if (item.ValueType == EnumBValueType.String)
            {
                writer.WriteStringValue(item.StringValue);
            }
            else
            {
                throw new CustomBasicException("Not Supported");
            }
        }

        writer.WriteEndArray();
    }
    private static void WriteObject(string key, BObject payLoad, Utf8JsonWriter writer)
    {
        writer.WriteStartObject(key);
        foreach (var item in payLoad.FullList)
        {
            if (item.Value.ValueType == EnumBValueType.Object)
            {
                WriteObject(item.Key, (BObject)item.Value, writer);
            }
            else if (item.Value.ValueType == EnumBValueType.CustomList)
            {
                FinishArray(item.Key, (BCustomList)item.Value, writer);
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
            else if (item.Value.ValueType == EnumBValueType.CustomList)
            {
                //try to do the list.  needed because sometimes i need a section of it.
                FinishArray(item.Key, (BCustomList)item.Value, writer);
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