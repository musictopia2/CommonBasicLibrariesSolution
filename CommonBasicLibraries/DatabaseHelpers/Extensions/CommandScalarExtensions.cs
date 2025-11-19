namespace CommonBasicLibraries.DatabaseHelpers.Extensions;
public static class CommandScalarExtensions
{
    extension<T>(object? results)
        where T: IParsable<T>
    {
        internal T Parse()
        {
            if (results is null)
            {
                return default!;
            }
            //hopefully i never need dateonly where parts was hosed.  if so, then needs to run a process to fix.
            return T.Parse(results.ToString()!, null); //hopefully this simple.
        }
    }
    extension<T>(IDbCommand command)
        where T : IParsable<T>
    {
        public  T Parse()
        {
            object? results = command.ExecuteScalar();
            return results.Parse<T>();
        }
    }
    extension (IDbCommand command)
    {
        public int? ParseNullableInt()
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
        public string? ParseString()
        {
            object? results = command.ExecuteScalar();
            if (results is null)
            {
                return null;
            }
            return results.ToString();
        }
        public char? ParseNullabelChar()
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
        public bool? ParseNullableBool()
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
        public decimal? ParseNullableDecimal()
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
        public double? ParseNullableDouble()
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
        public float? ParseNullableFloat()
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
        public DateTime? ParseNullableDateTime()
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
        public DateOnly? ParseNullableDateOnly()
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
        public TimeOnly? ParseNullableTimeOnly()
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
}