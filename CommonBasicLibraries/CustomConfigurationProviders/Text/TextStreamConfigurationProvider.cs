namespace CommonBasicLibraries.CustomConfigurationProviders.Text;
public class TextStreamConfigurationProvider(StreamConfigurationSource source, string delimiter) : StreamConfigurationProvider(source)
{
    public override void Load(Stream stream)
    {
        Read(stream, delimiter);
    }
    public static IDictionary<string, string?> Read(Stream stream, string delimiter)
    {
        var data = new Dictionary<string, string?>(StringComparer.OrdinalIgnoreCase);
        using var reader = new StreamReader(stream);
        while (reader.Peek() != -1)
        {
            string rawLine = reader.ReadLine()!; // Since Peak didn't return -1, stream hasn't ended.
            string line = rawLine.Trim();
            // Ignore blank lines
            if (string.IsNullOrWhiteSpace(line))
            {
                continue;
            }
            BasicList<string> list = line.Split(delimiter).ToBasicList();
            if (list.Count < 2)
            {
                continue; //ignore because must have 2 items period.  to attempt to make more stable
            }
            string valueToUse;
            string key;
            key = list.First();
            if (list.Count == 2)
            {
                valueToUse = list[1];
            }
            else
            {
                list.RemoveFirstItem();
                StrCat cats = new();
                foreach (var item in list)
                {
                    cats.AddToString(item, delimiter);
                }
                valueToUse = cats.GetInfo();
            }
            data.Add(key, valueToUse);
        }
        return data;
    }
}