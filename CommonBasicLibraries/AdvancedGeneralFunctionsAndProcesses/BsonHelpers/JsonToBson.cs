namespace CommonBasicLibraries.AdvancedGeneralFunctionsAndProcesses.BsonHelpers;
public static class JsonToBson
{
    private static BObject GetFirstArrayOfObjects(JsonElement element)
    {
        var list1 = element.EnumerateObject().ToBasicList();
        BObject output = [];
        foreach (var item1 in list1)
        {
            if (item1.Value.ValueKind == JsonValueKind.String)
            {
                output.Add(item1.Name, new(item1.Value.GetString()!));
            }
            else if (item1.Value.ValueKind == JsonValueKind.Array)
            {
                var list2 = item1.Value.EnumerateArray().ToBasicList();
                BCustomList arrays = [];
                foreach (var item2 in list2)
                {
                    if (item2.ValueKind == JsonValueKind.Object)
                    {
                        arrays.Add(GetFirstArrayOfObjects(item2));
                    }
                    else if (item2.ValueKind == JsonValueKind.String)
                    {
                        arrays.Add(new(item2.GetString()!));
                    }
                    else
                    {
                        throw new CustomBasicException("Not Supported");
                    }
                }
                output.Add(item1.Name, arrays);
            }
            else if (item1.Value.ValueKind == JsonValueKind.Number)
            {
                output.Add(item1.Name, new(item1.Value.GetInt32()));
            }
            else if (item1.Value.ValueKind == JsonValueKind.True)
            {
                bool rets = true;
                output.Add(item1.Name, new(rets));
            }
            else if (item1.Value.ValueKind == JsonValueKind.False)
            {
                bool rets = false;
                output.Add(item1.Name, new(rets));
            }
            else
            {
                throw new CustomBasicException("Not Supported");
            }
        }
        return output;
    }
    private static BObject GetFirstSubObject(JsonElement element)
    {

        var list1 = element.EnumerateObject().ToBasicList();
        BObject output = [];
        foreach (var item1 in list1)
        {
            if (item1.Value.ValueKind == JsonValueKind.Object)
            {
                var bb = GetFirstSubObject(item1.Value);
                output.Add(item1.Name, bb); //hopefully repeating is fine here (?)
            }
            else if (item1.Value.ValueKind == JsonValueKind.Array)
            {
                var list2 = item1.Value.EnumerateArray().ToBasicList();
                BCustomList arrays = [];
                foreach (var item2 in list2)
                {
                    if (item2.ValueKind == JsonValueKind.Object)
                    {
                        arrays.Add(GetFirstArrayOfObjects(item2));
                    }
                    else if (item2.ValueKind == JsonValueKind.String)
                    {
                        arrays.Add(new(item2.GetString()!));
                    }
                    else
                    {
                        throw new CustomBasicException("Not Supported");
                    }
                }
                output.Add(item1.Name, arrays);
            }
            else
            {
                throw new CustomBasicException("Not supported");
            }
        }
        return output;
    }
    public static BObject GetBsonFromJson(string text)
    {
        using var doc = JsonDocument.Parse(text);
        var element = doc.RootElement;
        if (element.ValueKind != JsonValueKind.Object)
        {
            throw new CustomBasicException("Must start out with object");
        }
        var list = element.EnumerateObject().ToBasicList();
        BObject output = [];
        foreach (var item in list)
        {
            if (item.Value.ValueKind == JsonValueKind.String)
            {
                output.Add(item.Name, new(item.Value.GetString()!));
            }
            else if (item.Value.ValueKind == JsonValueKind.Object)
            {
                var bb = GetFirstSubObject(item.Value);
                output.Add(item.Name, bb);
            }
            else
            {
                throw new CustomBasicException("Not Supported");
            }
        }
        return output;
    }
}