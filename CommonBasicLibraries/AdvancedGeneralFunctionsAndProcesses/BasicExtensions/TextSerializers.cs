namespace CommonBasicLibraries.AdvancedGeneralFunctionsAndProcesses.BasicExtensions;
public static class TextSerializers
{
    public static async Task SaveTextAsync<T>(this T payLoad, string path)
        where T : IModel
    {
        var table =
            FlatDataHelpers<T>.MasterContext ?? throw new InvalidOperationException("Flat data provider is not registered.");
        int columnCount = table.ColumnCount;
        BasicList<string> list = [];
        for (int i = 0; i < columnCount; i++)
        {
            string value = table.GetValue(payLoad, i);
            list.Add(value);
        }
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
    /// <summary>
    /// this will load a single object
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="path"></param>
    /// <returns></returns>
    public static async Task<T> LoadTextSingleAsync<T>(this string path) where T : new()
    {
        if (ff1.FileExists(path) == false)
        {
            return new T();
        }

        T output = new();

        var table =
            FlatDataHelpers<T>.MasterContext ?? throw new InvalidOperationException("Flat data provider is not registered.");
        int columnCount = table.ColumnCount;
        var lines = await ff1.ReadAllLinesAsync(path);
        if (lines.Count != columnCount)
        {
            throw new CustomBasicException("Text file corrupted because the delimiter count don't match the properties");
        }
        int x = 0;
        foreach (var line in lines)
        {
            if (table.TrySetValue(ref output, x, line, out string? error) == false)
            {
                throw new CustomBasicException($"Unable to set data.   Error was {error}");
            }
            x++;
        }
        return output;
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
}