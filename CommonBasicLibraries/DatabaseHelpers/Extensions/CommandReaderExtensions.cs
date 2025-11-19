namespace CommonBasicLibraries.DatabaseHelpers.Extensions;
public static class CommandReaderExtensions
{
    
    extension (DbDataReader reader)
    {
        //since there are related items that has parameters, does not make sense to make thse properties.
        //forced to make it internal or ui does not look right.
        internal int ReadIntItem()
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
        internal int? ReadNullableIntItem()
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
        internal string? ReadStringItem()
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
        internal bool ReadBoolItem()
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
        internal bool? ReadNullableBoolItem()
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
        internal decimal ReadDecimalItem()
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
        internal decimal? ReadNullableDecimalItem()
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
        internal double ReadDoubleItem()
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
        internal double? ReadNullableDoubleItem()
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
        internal float ReadFloatItem()
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
        internal float? ReadNullableFloatItem()
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
        internal DateTime ReadDateTimeItem(EnumDatabaseCategory category)
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
        internal DateTime? ReadNullableDateTimeItem(EnumDatabaseCategory category)
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
        internal DateOnly ReadDateOnlyItem(EnumDatabaseCategory category)
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
        internal DateOnly? ReadNullableDateOnlyItem(EnumDatabaseCategory category)
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
        internal TimeOnly ReadTimeOnlyItem(EnumDatabaseCategory category)
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
        internal TimeOnly? ReadNullableTimeOnlyItem(EnumDatabaseCategory category)
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
        internal char ReadCharItem()
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
        internal char? ReadNullableCharItem()
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
    extension(IDbCommand command)
    {
        public BasicList<int> GetIntList()
        {
            using DbDataReader? reader = command.ExecuteReader() as DbDataReader ?? throw new CustomBasicException("No reader found");
            BasicList<int> output = [];
            while (reader.Read())
            {
                output.Add(ReadIntItem(reader));
            }
            return output;
        }
        public async Task<BasicList<int>> GetIntListAsync()
        {
            using DbDataReader? reader = command.ExecuteReader() as DbDataReader ?? throw new CustomBasicException("No reader found");
            BasicList<int> output = [];
            while (await reader.ReadAsync())
            {
                output.Add(ReadIntItem(reader));
            }
            return output;
        }
        public BasicList<int?> GetNullableIntList()
        {
            using DbDataReader? reader = command.ExecuteReader() as DbDataReader ?? throw new CustomBasicException("No reader found");
            BasicList<int?> output = [];
            while (reader.Read())
            {
                output.Add(ReadNullableIntItem(reader));
            }
            return output;
        }
        public async Task<BasicList<int?>> GetNullableIntListAsync()
        {
            using DbDataReader? reader = command.ExecuteReader() as DbDataReader ?? throw new CustomBasicException("No reader found");
            BasicList<int?> output = [];
            while (await reader.ReadAsync())
            {
                output.Add(ReadNullableIntItem(reader));
            }
            return output;
        }
        public BasicList<string?> GetStringList()
        {
            using DbDataReader? reader = command.ExecuteReader() as DbDataReader ?? throw new CustomBasicException("No reader found");
            BasicList<string?> output = [];
            while (reader.Read())
            {
                output.Add(ReadStringItem(reader));
            }
            return output;
        }
        public async Task<BasicList<string?>> GetStringListAsync()
        {
            using DbDataReader? reader = command.ExecuteReader() as DbDataReader ?? throw new CustomBasicException("No reader found");
            BasicList<string?> output = [];
            while (await reader.ReadAsync())
            {
                output.Add(ReadStringItem(reader));
            }
            return output;
        }
        public BasicList<bool> GetBoolList()
        {
            using DbDataReader? reader = command.ExecuteReader() as DbDataReader ?? throw new CustomBasicException("No reader found");
            BasicList<bool> output = [];
            while (reader.Read())
            {
                output.Add(ReadBoolItem(reader));
            }
            return output;
        }
        public async Task<BasicList<bool>> GetBoolListAsync()
        {
            using DbDataReader? reader = command.ExecuteReader() as DbDataReader ?? throw new CustomBasicException("No reader found");
            BasicList<bool> output = [];
            while (await reader.ReadAsync())
            {
                output.Add(ReadBoolItem(reader));
            }
            return output;
        }
        public BasicList<bool?> GetNullableBoolList()
        {
            using DbDataReader? reader = command.ExecuteReader() as DbDataReader ?? throw new CustomBasicException("No reader found");
            BasicList<bool?> output = [];
            while (reader.Read())
            {
                output.Add(ReadNullableBoolItem(reader));
            }
            return output;
        }
        public async Task<BasicList<bool?>> GetNullableBoolListAsync()
        {
            using DbDataReader? reader = command.ExecuteReader() as DbDataReader ?? throw new CustomBasicException("No reader found");
            BasicList<bool?> output = [];
            while (await reader.ReadAsync())
            {
                output.Add(ReadNullableBoolItem(reader));
            }
            return output;
        }
        public BasicList<decimal> GetDecimalList()
        {
            using DbDataReader? reader = command.ExecuteReader() as DbDataReader ?? throw new CustomBasicException("No reader found");
            BasicList<decimal> output = [];
            while (reader.Read())
            {
                output.Add(ReadDecimalItem(reader));
            }
            return output;
        }
        public async Task<BasicList<decimal>> GetDecimalListAsync()
        {
            using DbDataReader? reader = command.ExecuteReader() as DbDataReader ?? throw new CustomBasicException("No reader found");
            BasicList<decimal> output = [];
            while (await reader.ReadAsync())
            {
                output.Add(ReadDecimalItem(reader));
            }
            return output;
        }
        public BasicList<decimal?> GetNullableDecimalList()
        {
            using DbDataReader? reader = command.ExecuteReader() as DbDataReader ?? throw new CustomBasicException("No reader found");
            BasicList<decimal?> output = [];
            while (reader.Read())
            {
                output.Add(ReadNullableDecimalItem(reader));
            }
            return output;
        }
        public async Task<BasicList<decimal?>> GetNullableDecimalListAsync()
        {
            using DbDataReader? reader = command.ExecuteReader() as DbDataReader ?? throw new CustomBasicException("No reader found");
            BasicList<decimal?> output = [];
            while (await reader.ReadAsync())
            {
                output.Add(ReadNullableDecimalItem(reader));
            }
            return output;
        }
        public BasicList<double> GetDoubleList()
        {
            using DbDataReader? reader = command.ExecuteReader() as DbDataReader ?? throw new CustomBasicException("No reader found");
            BasicList<double> output = [];
            while (reader.Read())
            {
                output.Add(ReadDoubleItem(reader));
            }
            return output;
        }
        public async Task<BasicList<double>> GetDoubleListAsync()
        {
            using DbDataReader? reader = command.ExecuteReader() as DbDataReader ?? throw new CustomBasicException("No reader found");
            BasicList<double> output = [];
            while (await reader.ReadAsync())
            {
                output.Add(ReadDoubleItem(reader));
            }
            return output;
        }
        public BasicList<double?> GetNullableDoubleList()
        {
            using DbDataReader? reader = command.ExecuteReader() as DbDataReader ?? throw new CustomBasicException("No reader found");
            BasicList<double?> output = [];
            while (reader.Read())
            {
                output.Add(ReadNullableDoubleItem(reader));
            }
            return output;
        }
        public async Task<BasicList<double?>> GetNullableDoubleListAsync()
        {
            using DbDataReader? reader = command.ExecuteReader() as DbDataReader ?? throw new CustomBasicException("No reader found");
            BasicList<double?> output = [];
            while (await reader.ReadAsync())
            {
                output.Add(ReadNullableDoubleItem(reader));
            }
            return output;
        }
        public BasicList<float> GetFloatList()
        {
            using DbDataReader? reader = command.ExecuteReader() as DbDataReader ?? throw new CustomBasicException("No reader found");
            BasicList<float> output = [];
            while (reader.Read())
            {
                output.Add(ReadFloatItem(reader));
            }
            return output;
        }
        public async Task<BasicList<float>> GetFloatListAsync()
        {
            using DbDataReader? reader = command.ExecuteReader() as DbDataReader ?? throw new CustomBasicException("No reader found");
            BasicList<float> output = [];
            while (await reader.ReadAsync())
            {
                output.Add(ReadFloatItem(reader));
            }
            return output;
        }
        public BasicList<float?> GetNullableFloatList()
        {
            using DbDataReader? reader = command.ExecuteReader() as DbDataReader ?? throw new CustomBasicException("No reader found");
            BasicList<float?> output = [];
            while (reader.Read())
            {
                output.Add(ReadNullableFloatItem(reader));
            }
            return output;
        }
        public async Task<BasicList<float?>> GetNullableFloatListAsync()
        {
            using DbDataReader? reader = command.ExecuteReader() as DbDataReader ?? throw new CustomBasicException("No reader found");
            BasicList<float?> output = [];
            while (await reader.ReadAsync())
            {
                output.Add(ReadNullableFloatItem(reader));
            }
            return output;
        }
        public BasicList<DateTime> GetDateTimeList(EnumDatabaseCategory category)
        {
            using DbDataReader? reader = command.ExecuteReader() as DbDataReader ?? throw new CustomBasicException("No reader found");
            BasicList<DateTime> output = [];
            while (reader.Read())
            {
                output.Add(ReadDateTimeItem(reader, category));
            }
            return output;
        }
        public async Task<BasicList<DateTime>> GetDateTimeListAsync(EnumDatabaseCategory category)
        {
            using DbDataReader? reader = command.ExecuteReader() as DbDataReader ?? throw new CustomBasicException("No reader found");
            BasicList<DateTime> output = [];
            while (await reader.ReadAsync())
            {
                output.Add(ReadDateTimeItem(reader, category));
            }
            return output;
        }
        public BasicList<DateTime?> GetNullableDateTimeList(EnumDatabaseCategory category)
        {
            using DbDataReader? reader = command.ExecuteReader() as DbDataReader ?? throw new CustomBasicException("No reader found");
            BasicList<DateTime?> output = [];
            while (reader.Read())
            {
                output.Add(ReadNullableDateTimeItem(reader, category));
            }
            return output;
        }
        public async Task<BasicList<DateTime?>> GetNullableDateTimeListAsync(EnumDatabaseCategory category)
        {
            using DbDataReader? reader = command.ExecuteReader() as DbDataReader ?? throw new CustomBasicException("No reader found");
            BasicList<DateTime?> output = [];
            while (await reader.ReadAsync())
            {
                output.Add(ReadNullableDateTimeItem(reader, category));
            }
            return output;
        }
        public BasicList<DateOnly> GetDateOnlyList(EnumDatabaseCategory category)
        {
            using DbDataReader? reader = command.ExecuteReader() as DbDataReader ?? throw new CustomBasicException("No reader found");
            BasicList<DateOnly> output = [];
            while (reader.Read())
            {
                output.Add(ReadDateOnlyItem(reader, category));
            }
            return output;
        }
        public async Task<BasicList<DateOnly>> GetDateOnlyListAsync(EnumDatabaseCategory category)
        {
            using DbDataReader? reader = command.ExecuteReader() as DbDataReader ?? throw new CustomBasicException("No reader found");
            BasicList<DateOnly> output = [];
            while (await reader.ReadAsync())
            {
                output.Add(ReadDateOnlyItem(reader, category));
            }
            return output;
        }
        public BasicList<DateOnly?> GetNullableDateOnlyList(EnumDatabaseCategory category)
        {
            using DbDataReader? reader = command.ExecuteReader() as DbDataReader ?? throw new CustomBasicException("No reader found");
            BasicList<DateOnly?> output = [];
            while (reader.Read())
            {
                output.Add(ReadNullableDateOnlyItem(reader, category));
            }
            return output;
        }
        public async Task<BasicList<DateOnly?>> GetNullableDateOnlyListAsync(EnumDatabaseCategory category)
        {
            using DbDataReader? reader = command.ExecuteReader() as DbDataReader ?? throw new CustomBasicException("No reader found");
            BasicList<DateOnly?> output = [];
            while (await reader.ReadAsync())
            {
                output.Add(ReadNullableDateOnlyItem(reader, category));
            }
            return output;
        }
        public BasicList<TimeOnly> GetTimeOnlyList(EnumDatabaseCategory category)
        {
            using DbDataReader? reader = command.ExecuteReader() as DbDataReader ?? throw new CustomBasicException("No reader found");
            BasicList<TimeOnly> output = [];
            while (reader.Read())
            {
                output.Add(ReadTimeOnlyItem(reader, category));
            }
            return output;
        }
        public async Task<BasicList<TimeOnly>> GetTimeOnlyListAsync(EnumDatabaseCategory category)
        {
            using DbDataReader? reader = command.ExecuteReader() as DbDataReader ?? throw new CustomBasicException("No reader found");
            BasicList<TimeOnly> output = [];
            while (await reader.ReadAsync())
            {
                output.Add(ReadTimeOnlyItem(reader, category));
            }
            return output;
        }

        public BasicList<TimeOnly?> GetNullableTimeOnlyList(EnumDatabaseCategory category)
        {
            using DbDataReader? reader = command.ExecuteReader() as DbDataReader ?? throw new CustomBasicException("No reader found");
            BasicList<TimeOnly?> output = [];
            while (reader.Read())
            {
                output.Add(ReadNullableTimeOnlyItem(reader, category));
            }
            return output;
        }
        public async Task<BasicList<TimeOnly?>> GetNullableTimeOnlyListAsync(EnumDatabaseCategory category)
        {
            using DbDataReader? reader = command.ExecuteReader() as DbDataReader ?? throw new CustomBasicException("No reader found");
            BasicList<TimeOnly?> output = [];
            while (await reader.ReadAsync())
            {
                output.Add(ReadNullableTimeOnlyItem(reader, category));
            }
            return output;
        }
        public BasicList<char> GetCharList()
        {
            using DbDataReader? reader = command.ExecuteReader() as DbDataReader ?? throw new CustomBasicException("No reader found");
            BasicList<char> output = [];
            while (reader.Read())
            {
                output.Add(ReadCharItem(reader));
            }
            return output;
        }
        public async Task<BasicList<char>> GetCharListAsync()
        {
            using DbDataReader? reader = command.ExecuteReader() as DbDataReader ?? throw new CustomBasicException("No reader found");
            BasicList<char> output = [];
            while (await reader.ReadAsync())
            {
                output.Add(ReadCharItem(reader));
            }
            return output;
        }
        public BasicList<char?> GetNullableCharList()
        {
            using DbDataReader? reader = command.ExecuteReader() as DbDataReader ?? throw new CustomBasicException("No reader found");
            BasicList<char?> output = [];
            while (reader.Read())
            {
                output.Add(ReadNullableCharItem(reader));
            }
            return output;
        }
        public async Task<BasicList<char?>> GetNullableCharListAsync()
        {
            using DbDataReader? reader = command.ExecuteReader() as DbDataReader ?? throw new CustomBasicException("No reader found");
            BasicList<char?> output = [];
            while (await reader.ReadAsync())
            {
                output.Add(ReadNullableCharItem(reader));
            }
            return output;
        }
    }
}