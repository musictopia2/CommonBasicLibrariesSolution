namespace CommonBasicLibraries.CustomConfigurationProviders;
public class TextStreamConfigurationProvider(StreamConfigurationSource source, string delimiter) : StreamConfigurationProvider(source)
{
    public override void Load(Stream stream)
    {
        Read(stream, delimiter);
        //this is what focuses on the stream.
        //this has a dictionary (good).  which is where it gets the values of the key/value pairs for the iconfiguration system.
        //this version will only focus on simple tab delimited.
        //will try connection strings (needs :) though.

    }
    //this is where i have to figure out how to parse this.  comes across as a stream.

    public static IDictionary<string, string?> Read(Stream stream, string delimiter)
    {
        var data = new Dictionary<string, string?>(StringComparer.OrdinalIgnoreCase);
        //figure out how to read.
        //stream.ReadEx

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
                    //valueToUse = valueToUse && item;
                }
                valueToUse = cats.GetInfo();
            }
            data.Add(key, valueToUse);
            //data.Add(list[0], list[1]);
        }

        return data; //for now.
    }
}
