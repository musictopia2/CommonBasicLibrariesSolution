using System.Collections.Generic;

namespace CommonBasicLibraries.AdvancedGeneralFunctionsAndProcesses.BasicExtensions;
public static class TextSerializers
{
    public static async Task SaveTextAsync<T>(this IModel payLoad, string path)
        where T : IModel
    {
        var properties = GetProperties<T>();
        BasicList<string> list = [];
        properties.ForEach(p =>
        {
            list.Add(p.GetValue(payLoad)!.ToString()!);
        });
        await ff1.WriteAllLinesAsync(path, list);
    }
    public static string SerializeText<T>(this BasicList<T> payLoad, string delimiter = ",")
    {
        var table =
            FlatDataHelpers<T>.MasterContext ?? throw new InvalidOperationException("Flat data provider is not registered.");
        int columnCount = table.ColumnCount;
        StrCat cats = new();
        BasicList<string> lines = [];
        foreach (var item in payLoad)
        {
            for (int i = 0; i < columnCount; i++)
            {
                string value = table.GetValue(item, i);
                cats.AddToString(value, delimiter); //for now, comma delimited only
            }

            lines.Add(cats.GetInfo());
            cats.ClearString(); //i think this too (?)
        }
        StrCat fins = new();
        lines.ForEach(x => fins.AddToString(x, Constants.VBCrLf));
        return fins.GetInfo();
    }
    public static void SaveText<T>(this BasicList<T> payLoad, string path, string delimiter = ",")
    {
        string content = payLoad.SerializeText(delimiter);
        ff1.WriteAllText(path, content, Encoding.UTF8);
    }
    public static async Task SaveTextAsync<T>(this BasicList<T> payLoad, string path, string delimiter = ",")
    {
        string content = payLoad.SerializeText<T>(delimiter);
        await ff1.WriteAllTextAsync(path, content, Encoding.UTF8);
    }
    private static BasicList<PropertyInfo> GetProperties<T>()
    {
        Type type = typeof(T);
        BasicList<PropertyInfo> output = type.GetProperties().ToBasicList();
        if (output.Exists(x => x.IsSimpleType()) == false)
        {
            throw new CustomBasicException("There are some properties that are not simple.  This only handles simple types for now");
        }
        return output;
    }
    /// <summary>
    /// this will load a single object
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="path"></param>
    /// <returns></returns>
    public static async Task<T> LoadTextSingleAsync<T>(this string path) where T : new()
    {
        var properties = GetProperties<T>();
        if (ff1.FileExists(path) == false)
        {
            return new T();
        }
        var lines = await ff1.ReadAllLinesAsync(path);
        if (lines.Count != properties.Count)
        {
            throw new CustomBasicException("Text file corrupted because the delimiter count don't match the properties");
        }
        T output = new();
        int x = 0;
        properties.ForEach(p =>
        {
            string item = lines[x];
            PopulateValue(item, output, p);
            x++;
        });
        return output;
    }
    private static object? GetValue(string item, Type type)
    {
        if (type == typeof(int))
        {
            bool rets = int.TryParse(item, out int y);
            if (rets == false)
            {
                throw new CustomBasicException($"When trying to parse dictionary to integer.  Means corruption");
            }
            return y;
        }
        else if (type == typeof(int?))
        {
            if (item == "")
            {
                return null;
            }
            else
            {
                bool rets = int.TryParse(item, out int y);
                if (rets == false)
                {
                    throw new CustomBasicException($"When trying to parse to integer.  Means corruption");
                }
                return y;
            }
        }
        else if (type.IsEnum)
        {
            bool rets = int.TryParse(item, out int y);
            if (rets == false)
            {
                throw new CustomBasicException($"When trying to parse to enum.  Means corruption");
            }
            return y;
        }
        else if (type.IsNullableEnum())
        {
            if (item == "")
            {
                return null;
            }
            else
            {
                bool rets = int.TryParse(item, out int y);
                if (rets == false)
                {
                    throw new CustomBasicException($"When trying to parse to enum.  Means corruption");
                }
                return y;
            }
        }
        else if (type == typeof(bool))
        {
            bool rets = bool.TryParse(item, out bool y);
            if (rets == false)
            {
                throw new CustomBasicException($"When trying to parse to parse to boolean.  Means corruption");
            }
            return y;
        }
        else if (type == typeof(bool?))
        {
            if (item == "")
            {
                return null;
            }
            else
            {
                bool rets = bool.TryParse(item, out bool y);
                if (rets == false)
                {
                    throw new CustomBasicException($"When trying to parse to parse to boolean.  Means corruption");
                }
                return y;
            }
        }
        else if (type == typeof(decimal))
        {
            bool rets = decimal.TryParse(item, out decimal y);
            if (rets == false)
            {
                throw new CustomBasicException($"When trying to parse to parse to decimal.  Means corruption");
            }
            return y;

        }
        else if (type == typeof(decimal?))
        {
            if (item == "")
            {
                return null;
            }
            else
            {
                bool rets = decimal.TryParse(item, out decimal y);
                if (rets == false)
                {
                    throw new CustomBasicException($"When trying to parse to decimal.  Means corruption");
                }
                return y;
            }
        }
        else if (type == typeof(float))
        {
            bool rets = float.TryParse(item, out float y);
            if (rets == false)
            {
                throw new CustomBasicException($"When trying to parse to parse to float.  Means corruption");
            }
            return y;

        }
        else if (type == typeof(float?))
        {
            if (item == "")
            {
                return null;
            }
            else
            {
                bool rets = float.TryParse(item, out float y);
                if (rets == false)
                {
                    throw new CustomBasicException($"When trying to parse to float.  Means corruption");
                }
                return y;
            }
        }
        else if (type == typeof(double))
        {
            bool rets = double.TryParse(item, out double y);
            if (rets == false)
            {
                throw new CustomBasicException($"When trying to parse to double.  Means corruption");
            }
            return y;

        }
        else if (type == typeof(double?))
        {
            if (item == "")
            {
                return null;
            }
            else
            {
                bool rets = double.TryParse(item, out double y);
                if (rets == false)
                {
                    throw new CustomBasicException($"When trying to parse to double.  Means corruption");
                }
                return y;
            }
        }
        else if (type == typeof(DateTime))
        {
            bool rets = DateTime.TryParse(item, out DateTime y);
            if (rets == false)
            {
                throw new CustomBasicException($"When trying to parse to datetime.  Means corruption");
            }
            return y;

        }
        else if (type == typeof(DateTime?))
        {
            if (item == "")
            {
                return null;
            }
            else
            {
                bool rets = DateTime.TryParse(item, out DateTime y);
                if (rets == false)
                {
                    throw new CustomBasicException($"When trying to parse to datetime.  Means corruption");
                }
                return y;
            }
        }
        else if (type == typeof(DateOnly))
        {
            bool rets = DateOnly.TryParse(item, out DateOnly y);
            if (rets == false)
            {
                throw new CustomBasicException($"When trying to parse to datetime.  Means corruption");
            }
            return y;

        }
        else if (type == typeof(DateOnly?))
        {
            if (item == "")
            {
                return null;
            }
            else
            {
                bool rets = DateOnly.TryParse(item, out DateOnly y);
                if (rets == false)
                {
                    throw new CustomBasicException($"When trying to parse to datetime.  Means corruption");
                }
                return y;
            }
        }
        else if (type == typeof(DateTimeOffset)) //no dateonlyoffset though
        {
            bool rets = DateTimeOffset.TryParse(item, out DateTimeOffset y);
            if (rets == false)
            {
                throw new CustomBasicException($"When trying to parse to datetimeoffset.  Means corruption");
            }
            return y;

        }
        else if (type == typeof(DateTimeOffset?))
        {
            if (item == "")
            {
                return null;
            }
            else
            {
                bool rets = DateTimeOffset.TryParse(item, out DateTimeOffset y);
                if (rets == false)
                {
                    throw new CustomBasicException($"When trying to parse to datetimeoffset.  Means corruption");
                }
                return y;
            }
        }
        else if (type == typeof(Guid))
        {
            bool rets = Guid.TryParse(item, out Guid y);
            if (rets == false)
            {
                throw new CustomBasicException($"When trying to parse to guid.  Means corruption");
            }
            return y;
        }
        else if (type == typeof(Guid?))
        {
            if (item == "")
            {
                return null;

            }
            else
            {
                bool rets = Guid.TryParse(item, out Guid y);
                if (rets == false)
                {
                    throw new CustomBasicException($"When trying to parse to guid.  Means corruption");
                }
                return y;
            }
        }
        else if (type == typeof(char))
        {
            bool rets = char.TryParse(item, out char y);
            if (rets == false)
            {
                throw new CustomBasicException($"When trying to parse to char.  Means corruption");
            }
            return y;
        }
        else if (type == typeof(char?))
        {
            if (item == "")
            {
                return null;
            }
            else
            {
                bool rets = char.TryParse(item, out char y);
                if (rets == false)
                {
                    throw new CustomBasicException($"When trying to parse to char.  Means corruption");
                }
                return y;
            }
        }
        else if (type == typeof(short))
        {
            bool rets = short.TryParse(item, out short y);
            if (rets == false)
            {
                throw new CustomBasicException($"When trying to parse to short.  Means corruption");
            }
            return y;
        }
        else if (type == typeof(short?))
        {
            if (item == "")
            {
                return null;
            }
            else
            {
                bool rets = short.TryParse(item, out short y);
                if (rets == false)
                {
                    throw new CustomBasicException($"When trying to parse to short.  Means corruption");
                }
                return y;
            }
        }
        else if (type == typeof(ushort))
        {
            bool rets = ushort.TryParse(item, out ushort y);
            if (rets == false)
            {
                throw new CustomBasicException($"When trying to parse to ushort.  Means corruption");
            }
            return y;
        }
        else if (type == typeof(ushort?))
        {
            if (item == "")
            {
                return null;
            }
            else
            {
                bool rets = ushort.TryParse(item, out ushort y);
                if (rets == false)
                {
                    throw new CustomBasicException($"When trying to parse to ushort.  Means corruption");
                }
                return y;
            }
        }
        else if (type == typeof(uint))
        {
            bool rets = uint.TryParse(item, out uint y);
            if (rets == false)
            {
                throw new CustomBasicException($"When trying to parse to uint.  Means corruption");
            }
            return y;
        }
        else if (type == typeof(uint?))
        {
            if (item == "")
            {
                return null;
            }
            else
            {
                bool rets = uint.TryParse(item, out uint y);
                if (rets == false)
                {
                    throw new CustomBasicException($"When trying to parse to uint.  Means corruption");
                }
                return y;
            }
        }
        else if (type == typeof(long))
        {
            bool rets = long.TryParse(item, out long y);
            if (rets == false)
            {
                throw new CustomBasicException($"When trying to parse to long.  Means corruption");
            }
            return y;
        }
        else if (type == typeof(long?))
        {
            if (item == "")
            {
                return null;
            }
            else
            {
                bool rets = long.TryParse(item, out long y);
                if (rets == false)
                {
                    throw new CustomBasicException($"When trying to parse to long.  Means corruption");
                }
                return y;
            }
        }
        else if (type == typeof(ulong))
        {
            bool rets = ulong.TryParse(item, out ulong y);
            if (rets == false)
            {
                throw new CustomBasicException($"When trying to parse to ulong.  Means corruption");
            }
            return y;
        }
        else if (type == typeof(ulong?))
        {
            if (item == "")
            {
                return null;
            }
            else
            {
                bool rets = ulong.TryParse(item, out ulong y);
                if (rets == false)
                {
                    throw new CustomBasicException($"When trying to parse to ulong.  Means corruption");
                }
                return y;
            }
        }
        else if (type == typeof(string))
        {
            return item;
        }
        else
        {
            throw new CustomBasicException("Rethink");
        }
    }
    public static async Task SaveTextAsync<TKey, TValue>(this Dictionary<TKey, TValue> list, string path)
        where TKey : notnull
    {
        BasicList<string> output = [];
        foreach (var item in list)
        {
            output.Add($"{item.Key},{item.Value}");
        }
        await ff1.WriteAllLinesAsync(path, output, Encoding.UTF8);
    }
    public static async Task<Dictionary<TKey, TValue>> LoadTextDictionaryAsync<TKey, TValue>(this string path, string delimiter = ",")
        where TKey : notnull
    {
        Type key = typeof(TKey);
        bool rets = key.IsSimpleType();
        if (rets == false)
        {
            throw new CustomBasicException("First item is not simple for dictionary");
        }
        Type value = typeof(TValue);
        if (rets == false)
        {
            throw new CustomBasicException("Second item is not simple for dictionary");
        }
        if (ff1.FileExists(path) == false)
        {
            return [];
        }
        var lines = await ff1.ReadAllLinesAsync(path);
        Dictionary<TKey, TValue> output = [];
        foreach (var line in lines)
        {
            var fins = line.Split(delimiter);
            if (fins.Length != 2)
            {
                throw new CustomBasicException("Must have 2 items for a dictionary.  Rethink");
            }
            string firstString = fins[0];
            string secondString = fins[1];
            object? firstValue = GetValue(firstString, key) ?? throw new CustomBasicException("I don't think that key can be null for dictionary");
            object? secondValue = GetValue(secondString, value) ?? throw new CustomBasicException("I don't think the value can be null for dictionary");
            output.Add((TKey)firstValue, (TValue)secondValue);
        }
        return output;
    }
    public static async Task<BasicList<T>> LoadTextListFromResourceAsync<T>(this Assembly assembly, string name, string delimiter = ",")
        where T: new()
    {
        string content = await assembly.ResourcesAllTextFromFileAsync(name);
        return content.DeserializeDelimitedTextList<T>(delimiter);
    }
    public static BasicList<T> LoadTextFromListResource<T>(this Assembly assembly, string name, string delimiter = ",")
        where T: new()
    {
        string content = assembly.ResourcesAllTextFromFile(name);
        return content.DeserializeDelimitedTextList<T>(delimiter);
    }
    //eventually can do source generators but not yet.
    public static BasicList<T> DeserializeDelimitedTextList<T>(this string content, string delimiter = ",")
        where T : new()
    {
        BasicList<string> lines = content.Split(Constants.VBCrLf).ToBasicList();
        BasicList<T> output = [];
        var table =
            FlatDataHelpers<T>.MasterContext ?? throw new InvalidOperationException("Flat data provider is not registered.");
        int columnCount = table.ColumnCount;
        StrCat cats = new();
        foreach (var line in lines)
        {
            T news = new();
            var parts = line.Split(delimiter);
            if (parts.Length != columnCount)
            {
                throw new InvalidOperationException($"The number of columns in the line {line} does not match the expected column count of {columnCount}.");
            }
            int x = 0;
            foreach (var part in parts)
            {
                if (table.TrySetValue(ref news, x, part.Trim(), out string? error) == false)
                {
                    throw new CustomBasicException($"Unable to set data.   Error was {error}");
                }
                x++;
            }
            output.Add(news);
        }
        return output;
    }
    /// <summary>
    /// this will load the text file and return a list of the object you want.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="path"></param>
    /// <param name="delimiter"></param>
    /// <returns></returns>
    public static BasicList<T> LoadTextList<T>(this string path, string delimiter = ",")
        where T : new()
    {
        BasicList<T> output = [];
        if (ff1.FileExists(path) == false)
        {
            return output;
        }
        string content = ff1.AllText(path);
        output = content.DeserializeDelimitedTextList<T>(delimiter);
        return output;
    }

    /// <summary>
    /// this will load the text file and return a list of the object you want.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="path"></param>
    /// <param name="delimiter"></param>
    /// <returns></returns>
    public static async Task<BasicList<T>> LoadTextListAsync<T>(this string path, string delimiter = ",")
        where T : new()
    {
        BasicList<T> output = [];
        if (ff1.FileExists(path) == false)
        {
            return output;
        }
        string content = await ff1.AllTextAsync(path);
        output = content.DeserializeDelimitedTextList<T>(delimiter);
        return output;
    }
    private static void PopulateValue<T>(string item, T row, PropertyInfo p)
    {
        if (p.PropertyType == typeof(int))
        {
            bool rets = int.TryParse(item, out int y);
            if (rets == false)
            {
                throw new CustomBasicException($"When trying to parse column {p.Name}, failed to parse to integer.  Means corruption");
            }
            p.SetValue(row, y);
        }
        else if (p.PropertyType == typeof(int?))
        {
            if (item == "")
            {
                p.SetValue(row, null);
            }
            else
            {
                bool rets = int.TryParse(item, out int y);
                if (rets == false)
                {
                    throw new CustomBasicException($"When trying to parse column {p.Name}, failed to parse to integer.  Means corruption");
                }
                p.SetValue(row, y);
            }
        }
        else if (p.PropertyType.IsEnum)
        {
            bool rets = int.TryParse(item, out int y);
            if (rets == false)
            {
                throw new CustomBasicException($"When trying to parse column {p.Name}, failed to parse to enum.  Means corruption");
            }
            p.SetValue(row, y);
        }
        else if (p.PropertyType.IsNullableEnum())
        {
            if (item == "")
            {
                p.SetValue(row, null);
            }
            else
            {
                bool rets = int.TryParse(item, out int y);
                if (rets == false)
                {
                    throw new CustomBasicException($"When trying to parse column {p.Name}, failed to parse to enum.  Means corruption");
                }
                p.SetValue(row, y);
            }
        }
        else if (p.PropertyType == typeof(bool))
        {
            bool rets = bool.TryParse(item, out bool y);
            if (rets == false)
            {
                throw new CustomBasicException($"When trying to parse column {p.Name}, failed to parse to boolean.  Means corruption");
            }
            p.SetValue(row, y);
        }
        else if (p.PropertyType == typeof(bool?))
        {
            if (item == "")
            {
                p.SetValue(row, null);
            }
            else
            {
                bool rets = bool.TryParse(item, out bool y);
                if (rets == false)
                {
                    throw new CustomBasicException($"When trying to parse column {p.Name}, failed to parse to boolean.  Means corruption");
                }
                p.SetValue(row, y);
            }
        }
        else if (p.PropertyType == typeof(decimal))
        {
            bool rets = decimal.TryParse(item, out decimal y);
            if (rets == false)
            {
                throw new CustomBasicException($"When trying to parse column {p.Name}, failed to parse to decimal.  Means corruption");
            }
            p.SetValue(row, y);
        }
        else if (p.PropertyType == typeof(decimal?))
        {
            if (item == "")
            {
                p.SetValue(row, null);
            }
            else
            {
                bool rets = decimal.TryParse(item, out decimal y);
                if (rets == false)
                {
                    throw new CustomBasicException($"When trying to parse column {p.Name}, failed to parse to decimal.  Means corruption");
                }
                p.SetValue(row, y);
            }
        }
        else if (p.PropertyType == typeof(float))
        {
            bool rets = float.TryParse(item, out float y);
            if (rets == false)
            {
                throw new CustomBasicException($"When trying to parse column {p.Name}, failed to parse to float.  Means corruption");
            }
            p.SetValue(row, y);
        }
        else if (p.PropertyType == typeof(float?))
        {
            if (item == "")
            {
                p.SetValue(row, null);
            }
            else
            {
                bool rets = float.TryParse(item, out float y);
                if (rets == false)
                {
                    throw new CustomBasicException($"When trying to parse column {p.Name}, failed to parse to float.  Means corruption");
                }
                p.SetValue(row, y);
            }
        }
        else if (p.PropertyType == typeof(double))
        {
            bool rets = double.TryParse(item, out double y);
            if (rets == false)
            {
                throw new CustomBasicException($"When trying to parse column {p.Name}, failed to parse to double.  Means corruption");
            }
            p.SetValue(row, y);
        }
        else if (p.PropertyType == typeof(double?))
        {
            if (item == "")
            {
                p.SetValue(row, null);
            }
            else
            {
                bool rets = double.TryParse(item, out double y);
                if (rets == false)
                {
                    throw new CustomBasicException($"When trying to parse column {p.Name}, failed to parse to double.  Means corruption");
                }
                p.SetValue(row, y);
            }
        }
        else if (p.PropertyType == typeof(DateTime))
        {
            bool rets = DateTime.TryParse(item, out DateTime y);
            if (rets == false)
            {
                throw new CustomBasicException($"When trying to parse column {p.Name}, failed to parse to datetime.  Means corruption");
            }
            p.SetValue(row, y);

        }
        else if (p.PropertyType == typeof(DateTime?))
        {
            if (item == "")
            {
                p.SetValue(row, null);
            }
            else
            {
                bool rets = DateTime.TryParse(item, out DateTime y);
                if (rets == false)
                {
                    throw new CustomBasicException($"When trying to parse column {p.Name}, failed to parse to datetime.  Means corruption");
                }
                p.SetValue(row, y);
            }
        }
        else if (p.PropertyType == typeof(DateTimeOffset))
        {
            bool rets = DateTimeOffset.TryParse(item, out DateTimeOffset y);
            if (rets == false)
            {
                throw new CustomBasicException($"When trying to parse column {p.Name}, failed to parse to datetimeoffset.  Means corruption");
            }
            p.SetValue(row, y);
        }
        else if (p.PropertyType == typeof(DateTimeOffset?))
        {
            if (item == "")
            {
                p.SetValue(row, null);
            }
            else
            {
                bool rets = DateTimeOffset.TryParse(item, out DateTimeOffset y);
                if (rets == false)
                {
                    throw new CustomBasicException($"When trying to parse column {p.Name}, failed to parse to datetimeoffset.  Means corruption");
                }
                p.SetValue(row, y);
            }
        }
        else if (p.PropertyType == typeof(Guid))
        {
            bool rets = Guid.TryParse(item, out Guid y);
            if (rets == false)
            {
                throw new CustomBasicException($"When trying to parse column {p.Name}, failed to parse to guid.  Means corruption");
            }
            p.SetValue(row, y);
        }
        else if (p.PropertyType == typeof(Guid?))
        {
            if (item == "")
            {
                p.SetValue(row, null);
            }
            else
            {
                bool rets = Guid.TryParse(item, out Guid y);
                if (rets == false)
                {
                    throw new CustomBasicException($"When trying to parse column {p.Name}, failed to parse to guid.  Means corruption");
                }
                p.SetValue(row, y);
            }
        }
        else if (p.PropertyType == typeof(char))
        {
            bool rets = char.TryParse(item, out char y);
            if (rets == false)
            {
                throw new CustomBasicException($"When trying to parse column {p.Name}, failed to parse to char.  Means corruption");
            }
            p.SetValue(row, y);
        }
        else if (p.PropertyType == typeof(char?))
        {
            if (item == "")
            {
                p.SetValue(row, null);
            }
            else
            {
                bool rets = char.TryParse(item, out char y);
                if (rets == false)
                {
                    throw new CustomBasicException($"When trying to parse column {p.Name}, failed to parse to char.  Means corruption");
                }
                p.SetValue(row, y);
            }
        }
        else if (p.PropertyType == typeof(short))
        {
            bool rets = short.TryParse(item, out short y);
            if (rets == false)
            {
                throw new CustomBasicException($"When trying to parse column {p.Name}, failed to parse to short.  Means corruption");
            }
            p.SetValue(row, y);
        }
        else if (p.PropertyType == typeof(short?))
        {
            if (item == "")
            {
                p.SetValue(row, null);
            }
            else
            {
                bool rets = short.TryParse(item, out short y);
                if (rets == false)
                {
                    throw new CustomBasicException($"When trying to parse column {p.Name}, failed to parse to short.  Means corruption");
                }
                p.SetValue(row, y);
            }
        }
        else if (p.PropertyType == typeof(ushort))
        {
            bool rets = ushort.TryParse(item, out ushort y);
            if (rets == false)
            {
                throw new CustomBasicException($"When trying to parse column {p.Name}, failed to parse to ushort.  Means corruption");
            }
            p.SetValue(row, y);
        }
        else if (p.PropertyType == typeof(ushort?))
        {
            if (item == "")
            {
                p.SetValue(row, null);
            }
            else
            {
                bool rets = ushort.TryParse(item, out ushort y);
                if (rets == false)
                {
                    throw new CustomBasicException($"When trying to parse column {p.Name}, failed to parse to ushort.  Means corruption");
                }
                p.SetValue(row, y);
            }
        }
        else if (p.PropertyType == typeof(uint))
        {
            bool rets = uint.TryParse(item, out uint y);
            if (rets == false)
            {
                throw new CustomBasicException($"When trying to parse column {p.Name}, failed to parse to uint.  Means corruption");
            }
            p.SetValue(row, y);
        }
        else if (p.PropertyType == typeof(uint?))
        {
            if (item == "")
            {
                p.SetValue(row, null);
            }
            else
            {
                bool rets = uint.TryParse(item, out uint y);
                if (rets == false)
                {
                    throw new CustomBasicException($"When trying to parse column {p.Name}, failed to parse to uint.  Means corruption");
                }
                p.SetValue(row, y);
            }
        }
        else if (p.PropertyType == typeof(long))
        {
            bool rets = long.TryParse(item, out long y);
            if (rets == false)
            {
                throw new CustomBasicException($"When trying to parse column {p.Name}, failed to parse to long.  Means corruption");
            }
            p.SetValue(row, y);
        }
        else if (p.PropertyType == typeof(long?))
        {
            if (item == "")
            {
                p.SetValue(row, null);
            }
            else
            {
                bool rets = long.TryParse(item, out long y);
                if (rets == false)
                {
                    throw new CustomBasicException($"When trying to parse column {p.Name}, failed to parse to long.  Means corruption");
                }
                p.SetValue(row, y);
            }
        }
        else if (p.PropertyType == typeof(ulong))
        {
            bool rets = ulong.TryParse(item, out ulong y);
            if (rets == false)
            {
                throw new CustomBasicException($"When trying to parse column {p.Name}, failed to parse to ulong.  Means corruption");
            }
            p.SetValue(row, y);
        }
        else if (p.PropertyType == typeof(ulong?))
        {
            if (item == "")
            {
                p.SetValue(row, null);
            }
            else
            {
                bool rets = ulong.TryParse(item, out ulong y);
                if (rets == false)
                {
                    throw new CustomBasicException($"When trying to parse column {p.Name}, failed to parse to ulong.  Means corruption");
                }
                p.SetValue(row, y);
            }
        }
        else if (p.PropertyType == typeof(string))
        {
            p.SetValue(row, item);
        }
        else
        {
            throw new CustomBasicException($"Property with name of {p.Name} has unsupported property type of {p.PropertyType.Name}.  Rethink");
        }
    }
}