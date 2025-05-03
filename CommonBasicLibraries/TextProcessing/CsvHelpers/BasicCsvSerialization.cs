namespace CommonBasicLibraries.TextProcessing.CsvHelpers;
public static partial class BasicCsvSerialization
{
    public static string Serialize(BasicList<string> inputList)
    {
        BasicList<string> formattedStrings = [];

        foreach (var str in inputList)
        {
            // If the string contains a comma or quotes, wrap it in quotes
            if (str.Contains(',') || str.Contains('"'))
            {
                // Escape internal quotes by doubling them
                formattedStrings.Add($"\"{EscapeQuotes(str)}\"");
            }
            else
            {
                formattedStrings.Add(str); // No need to quote if no special characters
            }
        }
        // Join the list into a single CSV string (separated by commas)
        return string.Join(",", formattedStrings);
    }
    private static string EscapeQuotes(string value)
    {
        return value.Replace("\"", "\"\"");
    }
    public static BasicList<T> Deserialize<T>(string input)
    {
        BasicList<T> result = [];

        // This regex matches either quoted strings or regular unquoted values
        var regex = CsvExpression();

        // Matches all occurrences (quoted or unquoted)
        var matches = regex.Matches(input);

        foreach (Match match in matches)
        {
            // If the match is quoted, remove the quotes and handle escaping
            string value = match.Value.Trim();
            if (value.StartsWith('"') && value.EndsWith('"'))
            {
                value = RemoveQuotes(value.ToString());
            }
            // Convert string to the correct type
            T convertedValue = ConvertToType<T>(value);
            result.Add(convertedValue);
        }

        return result;
    }
    private static T ConvertToType<T>(string value)
    {
        // Nullable handling using pattern matching (no reflection)
        if (typeof(T) == typeof(int?))
        {
            if (string.IsNullOrEmpty(value))
            {
                return default!;
            }

            if (int.TryParse(value, out var i))
            {
                return (T)(object)i;
            }
        }
        else if (typeof(T) == typeof(bool?))
        {
            if (string.IsNullOrEmpty(value))
            {
                return default!;
            }

            if (bool.TryParse(value, out var b))
            {
                return (T)(object)b;
            }
        }
        else if (typeof(T) == typeof(double?))
        {
            if (string.IsNullOrEmpty(value))
            {
                return default!;
            }

            if (double.TryParse(value, out var d))
            {
                return (T)(object)d;
            }
        }
        else if (typeof(T) == typeof(decimal?))
        {
            if (string.IsNullOrEmpty(value))
            {
                return default!;
            }

            if (decimal.TryParse(value, out var dec))
            {
                return (T)(object)dec;
            }
        }
        else if (typeof(T) == typeof(float?))
        {
            if (string.IsNullOrEmpty(value))
            {
                return default!;
            }

            if (float.TryParse(value, out var f))
            {
                return (T)(object)f;
            }
        }
        else if (typeof(T) == typeof(long?))
        {
            if (string.IsNullOrEmpty(value))
            {
                return default!;
            }

            if (long.TryParse(value, out var l))
            {
                return (T)(object)l;
            }
        }
        else if (typeof(T) == typeof(string))
        {
            return (T)(object)value;
        }
        else
        {
            // Non-nullable types
            return ConvertToNonNullable<T>(value);
        }

        throw new InvalidOperationException($"Unsupported or unparseable type: {typeof(T).Name}");
    }

    private static T ConvertToNonNullable<T>(string value)
    {
        if (typeof(T) == typeof(int) && int.TryParse(value, out var i))
        {
            return (T)(object)i;
        }

        if (typeof(T) == typeof(bool) && bool.TryParse(value, out var b))
        {
            return (T)(object)b;
        }

        if (typeof(T) == typeof(double) && double.TryParse(value, out var d))
        {
            return (T)(object)d;
        }

        if (typeof(T) == typeof(decimal) && decimal.TryParse(value, out var dec))
        {
            return (T)(object)dec;
        }

        if (typeof(T) == typeof(float) && float.TryParse(value, out var f))
        {
            return (T)(object)f;
        }

        if (typeof(T) == typeof(long) && long.TryParse(value, out var l))
        {
            return (T)(object)l;
        }

        if (typeof(T) == typeof(string))
        {
            return (T)(object)value;
        }

        throw new InvalidOperationException($"Unsupported or unparseable type: {typeof(T).Name}");
    }

    private static string RemoveQuotes(string value)
    {
        if (value.StartsWith('"') && value.EndsWith('"')) // Updated to use char overload  
        {
            value = value.Substring(1, value.Length - 2); // Remove surrounding quotes  
        }

        // Unescape internal quotes (double quotes in the middle of the value)  
        value = value.Replace("\"\"", "\"");

        return value;
    }

    [GeneratedRegex(@"(?<=^|,)(\""[^\""]*\""|[^,]*)")]
    private static partial Regex CsvExpression();
}