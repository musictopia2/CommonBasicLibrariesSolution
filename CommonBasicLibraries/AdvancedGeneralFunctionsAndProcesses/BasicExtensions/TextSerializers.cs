namespace CommonBasicLibraries.AdvancedGeneralFunctionsAndProcesses.BasicExtensions;
public static class TextSerializers
{
    extension<T>(T payLoad)
        where T : IModel
    {
        public async Task SaveTextAsync(string path)
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
    }
    extension<T>(BasicList<T> payLoad)
    {
        public string SerializeText(string delimiter = ",")
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
            lines.ForEach(x => fins.AddToString(x, dd1.VBCrLf));
            return fins.GetInfo();
        }
        public void SaveText(string path, string delimiter = ",")
        {
            string content = payLoad.SerializeText(delimiter);
            ff1.WriteAllText(path, content, Encoding.UTF8);
        }
        public async Task SaveTextAsync(string path, string delimiter = ",")
        {
            string content = payLoad.SerializeText(delimiter);
            await ff1.WriteAllTextAsync(path, content, Encoding.UTF8);
        }
    }
    extension(string payLoad)
    {
        public async Task<T> LoadTextSingleAsync<T>()
            where T : new()
        {
            if (ff1.FileExists(payLoad) == false)
            {
                return new T();
            }

            T output = new();

            var table =
                FlatDataHelpers<T>.MasterContext ?? throw new InvalidOperationException("Flat data provider is not registered.");
            int columnCount = table.ColumnCount;
            var lines = await ff1.ReadAllLinesAsync(payLoad);
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
        public BasicList<T> DeserializeDelimitedTextList<T>(string delimiter = ",")
            where T : new()
        {
            BasicList<string> lines = payLoad.Split(dd1.VBCrLf).ToBasicList();
            BasicList<T> output = [];
            var table =
                FlatDataHelpers<T>.MasterContext ?? throw new InvalidOperationException("Flat data provider is not registered.");
            int columnCount = table.ColumnCount;
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
        public BasicList<T> LoadTextList<T>(string delimiter = ",")
            where T : new()
        {
            BasicList<T> output = [];
            if (ff1.FileExists(payLoad) == false)
            {
                return output;
            }
            string content = ff1.AllText(payLoad);
            output = content.DeserializeDelimitedTextList<T>(delimiter);
            return output;
        }
        public async Task<BasicList<T>> LoadTextListAsync<T>(string delimiter = ",")
            where T : new()
        {
            BasicList<T> output = [];
            if (ff1.FileExists(payLoad) == false)
            {
                return output;
            }
            string content = await ff1.AllTextAsync(payLoad);
            output = content.DeserializeDelimitedTextList<T>(delimiter);
            return output;
        }
    }
}