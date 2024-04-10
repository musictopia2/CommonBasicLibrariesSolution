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
        return int.Parse(results.ToString()!); //hopefully this simple.
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
        return bool.Parse(results.ToString()!); //hopefully this simple.
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
        return decimal.Parse(results.ToString()!);
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
        return double.Parse(results.ToString()!);
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
        return float.Parse(results.ToString()!);
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
        return DateTime.Parse(results.ToString()!);
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
        return DateOnly.Parse(results.ToString()!);
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
        return TimeOnly.Parse(results.ToString()!);
    }
}