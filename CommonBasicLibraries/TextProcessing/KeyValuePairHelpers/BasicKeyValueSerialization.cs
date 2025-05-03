namespace CommonBasicLibraries.TextProcessing.KeyValuePairHelpers;
public static partial class BasicKeyValueSerialization
{
    public static string Serialize(Dictionary<string, string> keyPairs)
    {
        //decided this is best to use a dictionary.
        var sb = new StringBuilder();

        foreach (var pair in keyPairs)
        {
            // Escape quotes for the value
            var value = EscapeQuotes(pair.Value);

            // Append key and value to the StringBuilder in the desired format
            sb.AppendFormat("{0}: \"{1}\", ", pair.Key, value);
        }

        // Remove the last comma and space
        if (sb.Length > 0)
        {
            sb.Length -= 2;  // Remove the trailing ", "
        }

        return sb.ToString();
    }
    public static Dictionary<string, string> Deserialize(string input)
    {
        //var keyValuePairs = new List<KeyValuePair<string, string>>();

        Dictionary<string, string> keyValuePairs = [];

        // Regular expression to match key-value pairs
        var regex = KeyValuePairExpression();

        // Perform the matching
        var matches = regex.Matches(input);

        // Iterate through the matches and build the key-value pairs
        foreach (Match match in matches)
        {
            if (match.Groups[1].Success) // For Name and Address fields (string values)
            {
                var key = match.Groups[1].Value;
                var value = match.Groups[2].Value;
                keyValuePairs.Add(key, value);
            }
            else if (match.Groups[3].Success) // For Age or other numeric fields
            {
                var key = match.Groups[3].Value;
                var value = match.Groups[4].Value;
                keyValuePairs.Add(key, value);
            }
        }
        return keyValuePairs;
    }

    private static string EscapeQuotes(string value)
    {
        if (value.Contains('"'))
        {
            value = value.Replace("\"", "\"\"");
        }
        return value;
    }

    [GeneratedRegex(@"(\w+):\s*""(.*?)""|(\w+):\s*(\d+)")]
    private static partial Regex KeyValuePairExpression();
}