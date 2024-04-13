namespace CommonBasicLibraries.DatabaseHelpers.Extensions;
public static class CommandReaderExtensions
{
    public static BasicList<int> GetIntList(this IDbCommand command)
    {
        using DbDataReader? reader = command.ExecuteReader() as DbDataReader ?? throw new CustomBasicException("No reader found");
        BasicList<int> output = [];
        while (reader.Read())
        {
            output.Add(ReadIntItem(reader));
        }
        return output;
    }
    public async static Task<BasicList<int>> GetIntListAsync(this IDbCommand command)
    {
        using DbDataReader? reader = command.ExecuteReader() as DbDataReader ?? throw new CustomBasicException("No reader found");
        BasicList<int> output = [];
        while (await reader.ReadAsync())
        {
            output.Add(ReadIntItem(reader));
        }
        return output;
    }
    private static int ReadIntItem(DbDataReader reader)
    {
        int output = default;
        var list = DbDataReaderExtensions.GetColumnSchema(reader);
        if (list.Count == 0)
        {
            return default;
        }
        if (list.Count > 1)
        {
            throw new CustomBasicException("Cannot have more than one item");
        }
        if (DataReaderExtensions.IsDBNull(reader, list.Single().ColumnName) == false)
        {
            output = DataReaderExtensions.GetInt32(reader, list.Single().ColumnName);
        }
        return output;
    }
    public static BasicList<int?> GetNullableIntList(this IDbCommand command)
    {
        using DbDataReader? reader = command.ExecuteReader() as DbDataReader ?? throw new CustomBasicException("No reader found");
        BasicList<int?> output = [];
        while (reader.Read())
        {
            output.Add(ReadNullableIntItem(reader));
        }
        return output;
    }
    public async static Task<BasicList<int?>> GetNullableIntListAsync(this IDbCommand command)
    {
        using DbDataReader? reader = command.ExecuteReader() as DbDataReader ?? throw new CustomBasicException("No reader found");
        BasicList<int?> output = [];
        while (await reader.ReadAsync())
        {
            output.Add(ReadNullableIntItem(reader));
        }
        return output;
    }
    private static int? ReadNullableIntItem(DbDataReader reader)
    {
        int? output = default;
        var list = DbDataReaderExtensions.GetColumnSchema(reader);
        if (list.Count == 0)
        {
            return output;
        }
        if (list.Count > 1)
        {
            throw new CustomBasicException("Cannot have more than one item");
        }
        if (DataReaderExtensions.IsDBNull(reader, list.Single().ColumnName) == false)
        {
            output = DataReaderExtensions.GetInt32(reader, list.Single().ColumnName);
        }
        return output;
    }
    public static BasicList<string?> GetStringList(this IDbCommand command)
    {
        using DbDataReader? reader = command.ExecuteReader() as DbDataReader ?? throw new CustomBasicException("No reader found");
        BasicList<string?> output = [];
        while (reader.Read())
        {
            output.Add(ReadStringItem(reader));
        }
        return output;
    }
    public async static Task<BasicList<string?>> GetStringListAsync(this IDbCommand command)
    {
        using DbDataReader? reader = command.ExecuteReader() as DbDataReader ?? throw new CustomBasicException("No reader found");
        BasicList<string?> output = [];
        while (await reader.ReadAsync())
        {
            output.Add(ReadStringItem(reader));
        }
        return output;
    }
    private static string? ReadStringItem(DbDataReader reader)
    {
        string? output = default;
        var list = DbDataReaderExtensions.GetColumnSchema(reader);
        if (list.Count == 0)
        {
            return default;
        }
        if (list.Count > 1)
        {
            throw new CustomBasicException("Cannot have more than one item");
        }
        if (DataReaderExtensions.IsDBNull(reader, list.Single().ColumnName) == false)
        {
            output = DataReaderExtensions.GetString(reader, list.Single().ColumnName);
        }
        return output;
    }
    public static BasicList<bool> GetBoolList(this IDbCommand command)
    {
        using DbDataReader? reader = command.ExecuteReader() as DbDataReader ?? throw new CustomBasicException("No reader found");
        BasicList<bool> output = [];
        while (reader.Read())
        {
            output.Add(ReadBoolItem(reader));
        }
        return output;
    }
    public async static Task<BasicList<bool>> GetBoolListAsync(this IDbCommand command)
    {
        using DbDataReader? reader = command.ExecuteReader() as DbDataReader ?? throw new CustomBasicException("No reader found");
        BasicList<bool> output = [];
        while (await reader.ReadAsync())
        {
            output.Add(ReadBoolItem(reader));
        }
        return output;
    }
    private static bool ReadBoolItem(DbDataReader reader)
    {
        bool output = default;
        var list = DbDataReaderExtensions.GetColumnSchema(reader);
        if (list.Count == 0)
        {
            return default;
        }
        if (list.Count > 1)
        {
            throw new CustomBasicException("Cannot have more than one item");
        }
        if (DataReaderExtensions.IsDBNull(reader, list.Single().ColumnName) == false)
        {
            output = DataReaderExtensions.GetBoolean(reader, list.Single().ColumnName);
        }
        return output;
    }
    public static BasicList<bool?> GetNullableBoolList(this IDbCommand command)
    {
        using DbDataReader? reader = command.ExecuteReader() as DbDataReader ?? throw new CustomBasicException("No reader found");
        BasicList<bool?> output = [];
        while (reader.Read())
        {
            output.Add(ReadNullableBoolItem(reader));
        }
        return output;
    }
    public async static Task<BasicList<bool?>> GetNullableBoolListAsync(this IDbCommand command)
    {
        using DbDataReader? reader = command.ExecuteReader() as DbDataReader ?? throw new CustomBasicException("No reader found");
        BasicList<bool?> output = [];
        while (await reader.ReadAsync())
        {
            output.Add(ReadNullableBoolItem(reader));
        }
        return output;
    }
    private static bool? ReadNullableBoolItem(DbDataReader reader)
    {
        bool output = default;
        var list = DbDataReaderExtensions.GetColumnSchema(reader);
        if (list.Count == 0)
        {
            return default;
        }
        if (list.Count > 1)
        {
            throw new CustomBasicException("Cannot have more than one item");
        }
        if (DataReaderExtensions.IsDBNull(reader, list.Single().ColumnName) == false)
        {
            output = DataReaderExtensions.GetBoolean(reader, list.Single().ColumnName);
        }
        return output;
    }
    public static BasicList<decimal> GetDecimalList(this IDbCommand command)
    {
        using DbDataReader? reader = command.ExecuteReader() as DbDataReader ?? throw new CustomBasicException("No reader found");
        BasicList<decimal> output = [];
        while (reader.Read())
        {
            output.Add(ReadDecimalItem(reader));
        }
        return output;
    }
    public async static Task<BasicList<decimal>> GetDecimalListAsync(this IDbCommand command)
    {
        using DbDataReader? reader = command.ExecuteReader() as DbDataReader ?? throw new CustomBasicException("No reader found");
        BasicList<decimal> output = [];
        while (await reader.ReadAsync())
        {
            output.Add(ReadDecimalItem(reader));
        }
        return output;
    }
    private static decimal ReadDecimalItem(DbDataReader reader)
    {
        decimal output = default;
        var list = DbDataReaderExtensions.GetColumnSchema(reader);
        if (list.Count == 0)
        {
            return default;
        }
        if (list.Count > 1)
        {
            throw new CustomBasicException("Cannot have more than one item");
        }
        if (DataReaderExtensions.IsDBNull(reader, list.Single().ColumnName) == false)
        {
            output = DataReaderExtensions.GetDecimal(reader, list.Single().ColumnName);
        }
        return output;
    }
    public static BasicList<decimal?> GetNullableDecimalList(this IDbCommand command)
    {
        using DbDataReader? reader = command.ExecuteReader() as DbDataReader ?? throw new CustomBasicException("No reader found");
        BasicList<decimal?> output = [];
        while (reader.Read())
        {
            output.Add(ReadNullableDecimalItem(reader));
        }
        return output;
    }
    public async static Task<BasicList<decimal?>> GetNullableDecimalListAsync(this IDbCommand command)
    {
        using DbDataReader? reader = command.ExecuteReader() as DbDataReader ?? throw new CustomBasicException("No reader found");
        BasicList<decimal?> output = [];
        while (await reader.ReadAsync())
        {
            output.Add(ReadNullableDecimalItem(reader));
        }
        return output;
    }
    private static decimal? ReadNullableDecimalItem(DbDataReader reader)
    {
        decimal output = default;
        var list = DbDataReaderExtensions.GetColumnSchema(reader);
        if (list.Count == 0)
        {
            return default;
        }
        if (list.Count > 1)
        {
            throw new CustomBasicException("Cannot have more than one item");
        }
        if (DataReaderExtensions.IsDBNull(reader, list.Single().ColumnName) == false)
        {
            output = DataReaderExtensions.GetDecimal(reader, list.Single().ColumnName);
        }
        return output;
    }
    public static BasicList<double> GetDoubleList(this IDbCommand command)
    {
        using DbDataReader? reader = command.ExecuteReader() as DbDataReader ?? throw new CustomBasicException("No reader found");
        BasicList<double> output = [];
        while (reader.Read())
        {
            output.Add(ReadDoubleItem(reader));
        }
        return output;
    }
    public async static Task<BasicList<double>> GetDoubleListAsync(this IDbCommand command)
    {
        using DbDataReader? reader = command.ExecuteReader() as DbDataReader ?? throw new CustomBasicException("No reader found");
        BasicList<double> output = [];
        while (await reader.ReadAsync())
        {
            output.Add(ReadDoubleItem(reader));
        }
        return output;
    }
    private static double ReadDoubleItem(DbDataReader reader)
    {
        double output = default;
        var list = DbDataReaderExtensions.GetColumnSchema(reader);
        if (list.Count == 0)
        {
            return default;
        }
        if (list.Count > 1)
        {
            throw new CustomBasicException("Cannot have more than one item");
        }
        if (DataReaderExtensions.IsDBNull(reader, list.Single().ColumnName) == false)
        {
            output = DataReaderExtensions.GetDouble(reader, list.Single().ColumnName);
        }
        return output;
    }
    public static BasicList<double?> GetNullableDoubleList(this IDbCommand command)
    {
        using DbDataReader? reader = command.ExecuteReader() as DbDataReader ?? throw new CustomBasicException("No reader found");
        BasicList<double?> output = [];
        while (reader.Read())
        {
            output.Add(ReadNullableDoubleItem(reader));
        }
        return output;
    }
    public async static Task<BasicList<double?>> GetNullableDoubleListAsync(this IDbCommand command)
    {
        using DbDataReader? reader = command.ExecuteReader() as DbDataReader ?? throw new CustomBasicException("No reader found");
        BasicList<double?> output = [];
        while (await reader.ReadAsync())
        {
            output.Add(ReadNullableDoubleItem(reader));
        }
        return output;
    }
    private static double? ReadNullableDoubleItem(DbDataReader reader)
    {
        double output = default;
        var list = DbDataReaderExtensions.GetColumnSchema(reader);
        if (list.Count == 0)
        {
            return default;
        }
        if (list.Count > 1)
        {
            throw new CustomBasicException("Cannot have more than one item");
        }
        if (DataReaderExtensions.IsDBNull(reader, list.Single().ColumnName) == false)
        {
            output = DataReaderExtensions.GetDouble(reader, list.Single().ColumnName);
        }
        return output;
    }
    public static BasicList<float> GetFloatList(this IDbCommand command)
    {
        using DbDataReader? reader = command.ExecuteReader() as DbDataReader ?? throw new CustomBasicException("No reader found");
        BasicList<float> output = [];
        while (reader.Read())
        {
            output.Add(ReadFloatItem(reader));
        }
        return output;
    }
    public async static Task<BasicList<float>> GetFloatListAsync(this IDbCommand command)
    {
        using DbDataReader? reader = command.ExecuteReader() as DbDataReader ?? throw new CustomBasicException("No reader found");
        BasicList<float> output = [];
        while (await reader.ReadAsync())
        {
            output.Add(ReadFloatItem(reader));
        }
        return output;
    }
    private static float ReadFloatItem(DbDataReader reader)
    {
        float output = default;
        var list = DbDataReaderExtensions.GetColumnSchema(reader);
        if (list.Count == 0)
        {
            return default;
        }
        if (list.Count > 1)
        {
            throw new CustomBasicException("Cannot have more than one item");
        }
        if (DataReaderExtensions.IsDBNull(reader, list.Single().ColumnName) == false)
        {
            output = DataReaderExtensions.GetFloat(reader, list.Single().ColumnName);
        }
        return output;
    }
    public static BasicList<float?> GetNullableFloatList(this IDbCommand command)
    {
        using DbDataReader? reader = command.ExecuteReader() as DbDataReader ?? throw new CustomBasicException("No reader found");
        BasicList<float?> output = [];
        while (reader.Read())
        {
            output.Add(ReadNullableFloatItem(reader));
        }
        return output;
    }
    public async static Task<BasicList<float?>> GetNullableFloatListAsync(this IDbCommand command)
    {
        using DbDataReader? reader = command.ExecuteReader() as DbDataReader ?? throw new CustomBasicException("No reader found");
        BasicList<float?> output = [];
        while (await reader.ReadAsync())
        {
            output.Add(ReadNullableFloatItem(reader));
        }
        return output;
    }
    private static float? ReadNullableFloatItem(DbDataReader reader)
    {
        float output = default;
        var list = DbDataReaderExtensions.GetColumnSchema(reader);
        if (list.Count == 0)
        {
            return default;
        }
        if (list.Count > 1)
        {
            throw new CustomBasicException("Cannot have more than one item");
        }
        if (DataReaderExtensions.IsDBNull(reader, list.Single().ColumnName) == false)
        {
            output = DataReaderExtensions.GetFloat(reader, list.Single().ColumnName);
        }
        return output;
    }
    public static BasicList<DateTime> GetDateTimeList(this IDbCommand command, EnumDatabaseCategory category)
    {
        using DbDataReader? reader = command.ExecuteReader() as DbDataReader ?? throw new CustomBasicException("No reader found");
        BasicList<DateTime> output = [];
        while (reader.Read())
        {
            output.Add(ReadDateTimeItem(reader, category));
        }
        return output;
    }
    public async static Task<BasicList<DateTime>> GetDateTimeListAsync(this IDbCommand command, EnumDatabaseCategory category)
    {
        using DbDataReader? reader = command.ExecuteReader() as DbDataReader ?? throw new CustomBasicException("No reader found");
        BasicList<DateTime> output = [];
        while (await reader.ReadAsync())
        {
            output.Add(ReadDateTimeItem(reader, category));
        }
        return output;
    }
    private static DateTime ReadDateTimeItem(DbDataReader reader, EnumDatabaseCategory category)
    {
        DateTime output = default;
        var list = DbDataReaderExtensions.GetColumnSchema(reader);
        if (list.Count == 0)
        {
            return default;
        }
        if (list.Count > 1)
        {
            throw new CustomBasicException("Cannot have more than one item");
        }
        if (DataReaderExtensions.IsDBNull(reader, list.Single().ColumnName) == false)
        {
            if (category == EnumDatabaseCategory.SQLite)
            {
                string dateUsed = DataReaderExtensions.GetString(reader, list.Single().ColumnName);
                output = DateTime.Parse(dateUsed);
            }
            else
            {
                output = DataReaderExtensions.GetDateTime(reader, list.Single().ColumnName);
            }
        }
        return output;
    }
    public static BasicList<DateTime?> GetNullableDateTimeList(this IDbCommand command, EnumDatabaseCategory category)
    {
        using DbDataReader? reader = command.ExecuteReader() as DbDataReader ?? throw new CustomBasicException("No reader found");
        BasicList<DateTime?> output = [];
        while (reader.Read())
        {
            output.Add(ReadNullableDateTimeItem(reader, category));
        }
        return output;
    }
    public async static Task<BasicList<DateTime?>> GetNullableDateTimeListAsync(this IDbCommand command, EnumDatabaseCategory category)
    {
        using DbDataReader? reader = command.ExecuteReader() as DbDataReader ?? throw new CustomBasicException("No reader found");
        BasicList<DateTime?> output = [];
        while (await reader.ReadAsync())
        {
            output.Add(ReadNullableDateTimeItem(reader, category));
        }
        return output;
    }
    private static DateTime? ReadNullableDateTimeItem(DbDataReader reader, EnumDatabaseCategory category)
    {
        DateTime output = default;
        var list = DbDataReaderExtensions.GetColumnSchema(reader);
        if (list.Count == 0)
        {
            return default;
        }
        if (list.Count > 1)
        {
            throw new CustomBasicException("Cannot have more than one item");
        }
        if (DataReaderExtensions.IsDBNull(reader, list.Single().ColumnName) == false)
        {
            if (category == EnumDatabaseCategory.SQLite)
            {
                string temp = DataReaderExtensions.GetString(reader, list.Single().ColumnName);
                output = DateTime.Parse(temp);
            }
            else
            {
                output = DataReaderExtensions.GetDateTime(reader, list.Single().ColumnName);
            }

        }
        return output;
    }
    public static BasicList<DateOnly> GetDateOnlyList(this IDbCommand command, EnumDatabaseCategory category)
    {
        using DbDataReader? reader = command.ExecuteReader() as DbDataReader ?? throw new CustomBasicException("No reader found");
        BasicList<DateOnly> output = [];
        while (reader.Read())
        {
            output.Add(ReadDateOnlyItem(reader, category));
        }
        return output;
    }
    public async static Task<BasicList<DateOnly>> GetDateOnlyListAsync(this IDbCommand command, EnumDatabaseCategory category)
    {
        using DbDataReader? reader = command.ExecuteReader() as DbDataReader ?? throw new CustomBasicException("No reader found");
        BasicList<DateOnly> output = [];
        while (await reader.ReadAsync())
        {
            output.Add(ReadDateOnlyItem(reader, category));
        }
        return output;
    }
    private static DateOnly ReadDateOnlyItem(DbDataReader reader, EnumDatabaseCategory category)
    {
        DateOnly output = default;
        var list = DbDataReaderExtensions.GetColumnSchema(reader);
        if (list.Count == 0)
        {
            return default;
        }
        if (list.Count > 1)
        {
            throw new CustomBasicException("Cannot have more than one item");
        }
        if (DataReaderExtensions.IsDBNull(reader, list.Single().ColumnName) == false)
        {
            if (category == EnumDatabaseCategory.SQLite)
            {
                string temp = DataReaderExtensions.GetString(reader, list.Single().ColumnName);
                temp = temp.Replace(" 00:00:00", "");
                output = DateOnly.Parse(temp);
            }
            else
            {
                DateTime date = DataReaderExtensions.GetDateTime(reader, list.Single().ColumnName);
                output = new(date.Year, date.Month, date.Day);
            }
        }
        return output;
    }
    public static BasicList<DateOnly?> GetNullableDateOnlyList(this IDbCommand command, EnumDatabaseCategory category)
    {
        using DbDataReader? reader = command.ExecuteReader() as DbDataReader ?? throw new CustomBasicException("No reader found");
        BasicList<DateOnly?> output = [];
        while (reader.Read())
        {
            output.Add(ReadNullableDateOnlyItem(reader, category));
        }
        return output;
    }
    public async static Task<BasicList<DateOnly?>> GetNullableDateOnlyListAsync(this IDbCommand command, EnumDatabaseCategory category)
    {
        using DbDataReader? reader = command.ExecuteReader() as DbDataReader ?? throw new CustomBasicException("No reader found");
        BasicList<DateOnly?> output = [];
        while (await reader.ReadAsync())
        {
            output.Add(ReadNullableDateOnlyItem(reader, category));
        }
        return output;
    }
    private static DateOnly? ReadNullableDateOnlyItem(DbDataReader reader, EnumDatabaseCategory category)
    {
        DateOnly output = default;
        var list = DbDataReaderExtensions.GetColumnSchema(reader);
        if (list.Count == 0)
        {
            return default;
        }
        if (list.Count > 1)
        {
            throw new CustomBasicException("Cannot have more than one item");
        }
        if (DataReaderExtensions.IsDBNull(reader, list.Single().ColumnName) == false)
        {
            if (category == EnumDatabaseCategory.SQLite)
            {
                string temp = DataReaderExtensions.GetString(reader, list.Single().ColumnName);
                temp = temp.Replace(" 00:00:00", "");
                output = DateOnly.Parse(temp);
            }
            else
            {
                DateTime date = DataReaderExtensions.GetDateTime(reader, list.Single().ColumnName);
                output = new(date.Year, date.Month, date.Day);
            }
            //output = DataReaderExtensions.GetInt32(reader, list.Single().ColumnName);
        }
        return output;
    }
    public static BasicList<TimeOnly> GetTimeOnlyList(this IDbCommand command, EnumDatabaseCategory category)
    {
        using DbDataReader? reader = command.ExecuteReader() as DbDataReader ?? throw new CustomBasicException("No reader found");
        BasicList<TimeOnly> output = [];
        while (reader.Read())
        {
            output.Add(ReadTimeOnlyItem(reader, category));
        }
        return output;
    }
    public async static Task<BasicList<TimeOnly>> GetTimeOnlyListAsync(this IDbCommand command, EnumDatabaseCategory category)
    {
        using DbDataReader? reader = command.ExecuteReader() as DbDataReader ?? throw new CustomBasicException("No reader found");
        BasicList<TimeOnly> output = [];
        while (await reader.ReadAsync())
        {
            output.Add(ReadTimeOnlyItem(reader, category));
        }
        return output;
    }
    private static TimeOnly ReadTimeOnlyItem(DbDataReader reader, EnumDatabaseCategory category)
    {
        TimeOnly output = default;
        var list = DbDataReaderExtensions.GetColumnSchema(reader);
        if (list.Count == 0)
        {
            return default;
        }
        if (list.Count > 1)
        {
            throw new CustomBasicException("Cannot have more than one item");
        }
        if (DataReaderExtensions.IsDBNull(reader, list.Single().ColumnName) == false)
        {
            if (category == EnumDatabaseCategory.SQLite)
            {
                string temp = DataReaderExtensions.GetString(reader, list.Single().ColumnName);
                output = TimeOnly.Parse(temp);
            }
            else
            {
                DateTime date = DataReaderExtensions.GetDateTime(reader, list.Single().ColumnName);
                output = new(date.Hour, date.Minute, date.Second, date.Millisecond);
            }
        }
        return output;
    }
    public static BasicList<TimeOnly?> GetNullableTimeOnlyList(this IDbCommand command, EnumDatabaseCategory category)
    {
        using DbDataReader? reader = command.ExecuteReader() as DbDataReader ?? throw new CustomBasicException("No reader found");
        BasicList<TimeOnly?> output = [];
        while (reader.Read())
        {
            output.Add(ReadNullableTimeOnlyItem(reader, category));
        }
        return output;
    }
    public async static Task<BasicList<TimeOnly?>> GetNullableTimeOnlyListAsync(this IDbCommand command, EnumDatabaseCategory category)
    {
        using DbDataReader? reader = command.ExecuteReader() as DbDataReader ?? throw new CustomBasicException("No reader found");
        BasicList<TimeOnly?> output = [];
        while (await reader.ReadAsync())
        {
            output.Add(ReadNullableTimeOnlyItem(reader, category));
        }
        return output;
    }
    private static TimeOnly? ReadNullableTimeOnlyItem(DbDataReader reader, EnumDatabaseCategory category)
    {
        TimeOnly output = default;
        var list = DbDataReaderExtensions.GetColumnSchema(reader);
        if (list.Count == 0)
        {
            return null;
        }
        if (list.Count > 1)
        {
            throw new CustomBasicException("Cannot have more than one item");
        }
        if (DataReaderExtensions.IsDBNull(reader, list.Single().ColumnName) == false)
        {
            if (category == EnumDatabaseCategory.SQLite)
            {
                string temp = DataReaderExtensions.GetString(reader, list.Single().ColumnName);
                output = TimeOnly.Parse(temp);
            }
            else
            {
                DateTime date = DataReaderExtensions.GetDateTime(reader, list.Single().ColumnName);
                output = new(date.Hour, date.Minute, date.Second, date.Millisecond);
            }
        }
        return output;
    }
    public static BasicList<char> GetCharList(this IDbCommand command)
    {
        using DbDataReader? reader = command.ExecuteReader() as DbDataReader ?? throw new CustomBasicException("No reader found");
        BasicList<char> output = [];
        while (reader.Read())
        {
            output.Add(ReadCharItem(reader));
        }
        return output;
    }
    public async static Task<BasicList<char>> GetCharListAsync(this IDbCommand command)
    {
        using DbDataReader? reader = command.ExecuteReader() as DbDataReader ?? throw new CustomBasicException("No reader found");
        BasicList<char> output = [];
        while (await reader.ReadAsync())
        {
            output.Add(ReadCharItem(reader));
        }
        return output;
    }
    private static char ReadCharItem(DbDataReader reader)
    {
        char output = default;
        var list = DbDataReaderExtensions.GetColumnSchema(reader);
        if (list.Count == 0)
        {
            return default;
        }
        if (list.Count > 1)
        {
            throw new CustomBasicException("Cannot have more than one item");
        }
        if (DataReaderExtensions.IsDBNull(reader, list.Single().ColumnName) == false)
        {
            string temp = DataReaderExtensions.GetString(reader, list.Single().ColumnName);
            output = temp.SingleOrDefault();
        }
        return output;
    }
    public static BasicList<char?> GetNullableCharList(this IDbCommand command)
    {
        using DbDataReader? reader = command.ExecuteReader() as DbDataReader ?? throw new CustomBasicException("No reader found");
        BasicList<char?> output = [];
        while (reader.Read())
        {
            output.Add(ReadNullableCharItem(reader));
        }
        return output;
    }
    public async static Task<BasicList<char?>> GetNullableCharListAsync(this IDbCommand command)
    {
        using DbDataReader? reader = command.ExecuteReader() as DbDataReader ?? throw new CustomBasicException("No reader found");
        BasicList<char?> output = [];
        while (await reader.ReadAsync())
        {
            output.Add(ReadNullableCharItem(reader));
        }
        return output;
    }
    private static char? ReadNullableCharItem(DbDataReader reader)
    {
        char output = default;
        var list = DbDataReaderExtensions.GetColumnSchema(reader);
        if (list.Count == 0)
        {
            return null;
        }
        if (list.Count > 1)
        {
            throw new CustomBasicException("Cannot have more than one item");
        }
        if (DataReaderExtensions.IsDBNull(reader, list.Single().ColumnName) == false)
        {
            string temp = DataReaderExtensions.GetString(reader, list.Single().ColumnName);
            output = temp.SingleOrDefault();
        }
        return output;
    }
}