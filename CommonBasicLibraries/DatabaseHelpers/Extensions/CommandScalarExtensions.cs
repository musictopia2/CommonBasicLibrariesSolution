namespace CommonBasicLibraries.DatabaseHelpers.Extensions;
public static class CommandScalarExtensions
{
    public static T Parse<T>(this IDbCommand command)
        where T : IParsable<T>
    {
        object? results = command.ExecuteScalar();
        return results.Parse<T>();
    }
    private static T Parse<T>(this object? results)
        where T : IParsable<T>
    {
        if (results is null)
        {
            return default!;
        }
        //hopefully i never need dateonly where parts was hosed.  if so, then needs to run a process to fix.
        return T.Parse(results.ToString()!, null); //hopefully this simple.
    }
    public static int? ParseNullableInt(this IDbCommand command)
    {
        object? results = command.ExecuteScalar();
        if (results is null)
        {
            return null;
        }
        string input = results.ToString()!;
        if (string.IsNullOrEmpty(input))
        {
            return null;
        }
        return int.Parse(input); //hopefully this simple.
    }
    public static string? ParseString(this IDbCommand command)
    {
        object? results = command.ExecuteScalar();
        if (results is null)
        {
            return null;
        }
        return results.ToString();
    }
    public static char? ParseNullabelChar(this IDbCommand command)
    {
        object? results = command.ExecuteScalar();
        if (results is null)
        {
            return null;
        }
        string input = results.ToString()!;
        if (string.IsNullOrEmpty(input))
        {
            return null;
        }
        return input.First();
    }
    public static bool? ParseNullableBool(this IDbCommand command)
    {
        object? results = command.ExecuteScalar();
        if (results is null)
        {
            return null;
        }
        string input = results.ToString()!;
        if (string.IsNullOrEmpty(input))
        {
            return null;
        }
        return bool.Parse(input); //hopefully this simple.
    }
    public static decimal? ParseNullableDecimal(this IDbCommand command)
    {
        object? results = command.ExecuteScalar();
        if (results is null)
        {
            return null;
        }
        string input = results.ToString()!;
        if (string.IsNullOrEmpty(input))
        {
            return null;
        }
        return decimal.Parse(input);
    }
    public static double? ParseNullableDouble(this IDbCommand command)
    {
        object? results = command.ExecuteScalar();
        if (results is null)
        {
            return null;
        }
        string input = results.ToString()!;
        if (string.IsNullOrEmpty(input))
        {
            return null;
        }
        return double.Parse(input);
    }
    public static float? ParseNullableFloat(this IDbCommand command)
    {
        object? results = command.ExecuteScalar();
        if (results is null)
        {
            return null;
        }
        string input = results.ToString()!;
        if (string.IsNullOrEmpty(input))
        {
            return null;
        }
        return float.Parse(input);
    }
    //public static TimeOnly
    public static DateTime? ParseNullableDateTime(this IDbCommand command)
    {
        object? results = command.ExecuteScalar();
        if (results is null)
        {
            return null;
        }
        string input = results.ToString()!;
        if (string.IsNullOrEmpty(input))
        {
            return null;
        }
        return DateTime.Parse(input);
    }
    public static DateOnly? ParseNullableDateOnly(this IDbCommand command)
    {
        object? results = command.ExecuteScalar();
        if (results is null)
        {
            return null;
        }
        string input = results.ToString()!;
        if (string.IsNullOrEmpty(input))
        {
            return null;
        }
        input = input.Replace(" 00:00:00", "");
        return DateOnly.Parse(input);
    }
    public static TimeOnly? ParseNullableTimeOnly(this IDbCommand command)
    {
        object? results = command.ExecuteScalar();
        if (results is null)
        {
            return null;
        }
        string input = results.ToString()!;
        if (string.IsNullOrEmpty(input))
        {
            return null;
        }
        return TimeOnly.Parse(input);
    }
}