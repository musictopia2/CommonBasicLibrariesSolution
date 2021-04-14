using CommonBasicLibraries.AdvancedGeneralFunctionsAndProcesses.FileFunctions;
using CommonBasicLibraries.AdvancedGeneralFunctionsAndProcesses.Misc;
using CommonBasicLibraries.BasicDataSettingsAndProcesses;
using CommonBasicLibraries.CollectionClasses;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
namespace CommonBasicLibraries.AdvancedGeneralFunctionsAndProcesses.BasicExtensions
{
    public static class TextSerializers
    {
        public static async Task SaveTextAsync<T>(this IModel payLoad, string path)
            where T : IModel
        {
            var properties = GetProperties<T>();
            BasicList<string> list = new();
            properties.ForEach(p =>
            {
                list.Add(p.GetValue(payLoad)!.ToString()!);
            });
            await File.WriteAllLinesAsync(path, list);
        }
        public static string SerializeText<T>(this BasicList<T> payLoad, string delimiter = ",")
        {
            var properties = GetProperties<T>();
            BasicList<string> list = new();
            foreach (var item in payLoad)
            {
                StrCat cats = new();
                properties.ForEach(p =>
                {
                    //if item is null, then will be empty.
                    var value = p.GetValue(item);
                    if (value != null)
                    {
                        cats.AddToString(value.ToString()!, delimiter);
                    }
                    else
                    {
                        cats.AddToString("", delimiter);
                    }
                });
                list.Add(cats.GetInfo());
            }

            StrCat fins = new();
            list.ForEach(x => fins.AddToString(x, Constants.VBCrLf));
            return fins.GetInfo();
        }

        public static void SaveText<T>(this BasicList<T> payLoad, string path, string delimiter = ",")
        {
            string content = payLoad.SerializeText(delimiter);
            File.WriteAllText(path, content, Encoding.UTF8);
        }

        public static async Task SaveTextAsync<T>(this BasicList<T> payLoad, string path, string delimiter = ",")
        {
            //will be utf8
            string content = payLoad.SerializeText<T>(delimiter);
            await File.WriteAllTextAsync(path, content, Encoding.UTF8);

        }

        private static BasicList<PropertyInfo> GetProperties<T>()
        {
            Type type = typeof(T);
            //first check to see if there is anything i can't handle.
            BasicList<PropertyInfo> output = type.GetProperties().ToBasicList();
            if (output.Exists(x => x.IsSimpleType()) == false)
            {
                throw new CustomBasicException("There are some properties that are not simple.  This only handles simple types for now");
            }
            return output;
        }
        /// <summary>
        /// this will load a single object
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="path"></param>
        /// <returns></returns>
        public static async Task<T> LoadTextSingleAsync<T>(this string path) where T : new()
        {
            var properties = GetProperties<T>();
            if (File.Exists(path) == false)
            {
                return new T();
            }
            var lines = await File.ReadAllLinesAsync(path);
            if (lines.Length != properties.Count)
            {
                throw new CustomBasicException("Text file corrupted because the delimiter count don't match the properties");
            }
            T output = new();
            int x = 0;
            properties.ForEach(p =>
            {
                string item = lines[x];
                PopulateValue(item, output, p);
                x++;
            });
            return output;
        }

        private static object? GetValue(string item, Type type)
        {
            if (type == typeof(int))
            {
                //try to parse to integers.
                bool rets = int.TryParse(item, out int y);
                if (rets == false)
                {
                    throw new CustomBasicException($"When trying to parse dictionary to integer.  Means corruption");
                }
                return y;
                //p.SetValue(row, y); //hopefully this simple.
            }
            else if (type == typeof(int?))
                if (item == "")
                    return null;
                else
                {
                    bool rets = int.TryParse(item, out int y);
                    if (rets == false)
                    {
                        throw new CustomBasicException($"When trying to parse to integer.  Means corruption");
                    }
                    return y;
                }
            else if (type.IsEnum)
            {
                bool rets = int.TryParse(item, out int y);
                if (rets == false)
                {
                    throw new CustomBasicException($"When trying to parse to enum.  Means corruption");
                }
                return y;
            }

            else if (type.IsNullableEnum())
                if (item == "")
                    return null;
                else
                {
                    bool rets = int.TryParse(item, out int y);
                    if (rets == false)
                    {
                        throw new CustomBasicException($"When trying to parse to enum.  Means corruption");
                    }
                    return y;
                }

            else if (type == typeof(bool))
            {
                bool rets = bool.TryParse(item, out bool y);
                if (rets == false)
                {
                    throw new CustomBasicException($"When trying to parse to parse to boolean.  Means corruption");
                }
                return y;

            }
            else if (type == typeof(bool?))
                if (item == "")
                {
                    return null;
                }
                else
                {
                    bool rets = bool.TryParse(item, out bool y);
                    if (rets == false)
                    {
                        throw new CustomBasicException($"When trying to parse to parse to boolean.  Means corruption");
                    }
                    return y;
                }
            else if (type == typeof(decimal))
            {
                bool rets = decimal.TryParse(item, out decimal y);
                if (rets == false)
                {
                    throw new CustomBasicException($"When trying to parse to parse to decimal.  Means corruption");
                }
                return y;

            }
            else if (type == typeof(decimal?))
                if (item == "")
                {
                    return null;
                }
                else
                {
                    bool rets = decimal.TryParse(item, out decimal y);
                    if (rets == false)
                    {
                        throw new CustomBasicException($"When trying to parse to decimal.  Means corruption");
                    }
                    return y;
                }
            else if (type == typeof(float))
            {
                bool rets = float.TryParse(item, out float y);
                if (rets == false)
                {
                    throw new CustomBasicException($"When trying to parse to parse to float.  Means corruption");
                }
                return y;

            }
            else if (type == typeof(float?))
                if (item == "")
                    return null;
                else
                {
                    bool rets = float.TryParse(item, out float y);
                    if (rets == false)
                    {
                        throw new CustomBasicException($"When trying to parse to float.  Means corruption");
                    }
                    return y;
                }
            else if (type == typeof(double))
            {
                bool rets = double.TryParse(item, out double y);
                if (rets == false)
                {
                    throw new CustomBasicException($"When trying to parse to double.  Means corruption");
                }
                return y;

            }
            else if (type == typeof(double?))
                if (item == "")
                {
                    return null;
                }
                else
                {
                    bool rets = double.TryParse(item, out double y);
                    if (rets == false)
                    {
                        throw new CustomBasicException($"When trying to parse to double.  Means corruption");
                    }
                    return y;
                }
            else if (type == typeof(DateTime))
            {
                bool rets = DateTime.TryParse(item, out DateTime y);
                if (rets == false)
                {
                    throw new CustomBasicException($"When trying to parse to datetime.  Means corruption");
                }
                return y;

            }
            else if (type == typeof(DateTime?))
                if (item == "")
                    return null;
                else
                {
                    bool rets = DateTime.TryParse(item, out DateTime y);
                    if (rets == false)
                    {
                        throw new CustomBasicException($"When trying to parse to datetime.  Means corruption");
                    }
                    return y;
                }
            else if (type == typeof(DateTimeOffset))
            {
                bool rets = DateTimeOffset.TryParse(item, out DateTimeOffset y);
                if (rets == false)
                {
                    throw new CustomBasicException($"When trying to parse to datetimeoffset.  Means corruption");
                }
                return y;

            }
            else if (type == typeof(DateTimeOffset?))
                if (item == "")
                    return null;
                else
                {
                    bool rets = DateTimeOffset.TryParse(item, out DateTimeOffset y);
                    if (rets == false)
                    {
                        throw new CustomBasicException($"When trying to parse to datetimeoffset.  Means corruption");
                    }
                    return y;
                }
            else if (type == typeof(Guid))
            {
                bool rets = Guid.TryParse(item, out Guid y);
                if (rets == false)
                {
                    throw new CustomBasicException($"When trying to parse to guid.  Means corruption");
                }
                return y;
            }
            else if (type == typeof(Guid?))
                if (item == "")
                    return null;
                else
                {
                    bool rets = Guid.TryParse(item, out Guid y);
                    if (rets == false)
                    {
                        throw new CustomBasicException($"When trying to parse to guid.  Means corruption");
                    }
                    return y;
                }
            else if (type == typeof(char))
            {
                bool rets = char.TryParse(item, out char y);
                if (rets == false)
                {
                    throw new CustomBasicException($"When trying to parse to char.  Means corruption");
                }
                return y;
            }
            else if (type == typeof(char?))
                if (item == "")
                    return null;
                else
                {
                    bool rets = char.TryParse(item, out char y);
                    if (rets == false)
                    {
                        throw new CustomBasicException($"When trying to parse to char.  Means corruption");
                    }
                    return y;
                }
            else if (type == typeof(short))
            {
                bool rets = short.TryParse(item, out short y);
                if (rets == false)
                {
                    throw new CustomBasicException($"When trying to parse to short.  Means corruption");
                }
                return y;
            }
            else if (type == typeof(short?))
                if (item == "")
                    return null;
                else
                {
                    bool rets = short.TryParse(item, out short y);
                    if (rets == false)
                    {
                        throw new CustomBasicException($"When trying to parse to short.  Means corruption");
                    }
                    return y;
                }
            else if (type == typeof(ushort))
            {
                bool rets = ushort.TryParse(item, out ushort y);
                if (rets == false)
                {
                    throw new CustomBasicException($"When trying to parse to ushort.  Means corruption");
                }
                return y;
            }
            else if (type == typeof(ushort?))
                if (item == "")
                {
                    return null;
                }
                else
                {
                    bool rets = ushort.TryParse(item, out ushort y);
                    if (rets == false)
                    {
                        throw new CustomBasicException($"When trying to parse to ushort.  Means corruption");
                    }
                    return y;
                }
            else if (type == typeof(uint))
            {
                bool rets = uint.TryParse(item, out uint y);
                if (rets == false)
                {
                    throw new CustomBasicException($"When trying to parse to uint.  Means corruption");
                }
                return y;
            }
            else if (type == typeof(uint?))
                if (item == "")
                {
                    return null;
                }
                else
                {
                    bool rets = uint.TryParse(item, out uint y);
                    if (rets == false)
                    {
                        throw new CustomBasicException($"When trying to parse to uint.  Means corruption");
                    }
                    return y;
                }
            else if (type == typeof(long))
            {
                bool rets = long.TryParse(item, out long y);
                if (rets == false)
                {
                    throw new CustomBasicException($"When trying to parse to long.  Means corruption");
                }
                return y;
            }
            else if (type == typeof(long?))
                if (item == "")
                    return null;
                else
                {
                    bool rets = long.TryParse(item, out long y);
                    if (rets == false)
                    {
                        throw new CustomBasicException($"When trying to parse to long.  Means corruption");
                    }
                    return y;
                }
            else if (type == typeof(ulong))
            {
                bool rets = ulong.TryParse(item, out ulong y);
                if (rets == false)
                {
                    throw new CustomBasicException($"When trying to parse to ulong.  Means corruption");
                }
                return y;
            }
            else if (type == typeof(ulong?))
                if (item == "")
                {
                    return null;
                }
                else
                {
                    bool rets = ulong.TryParse(item, out ulong y);
                    if (rets == false)
                    {
                        throw new CustomBasicException($"When trying to parse to ulong.  Means corruption");
                    }
                    return y;
                }
            else if (type == typeof(string))
            {
                return item;
            }
            else
            {
                throw new CustomBasicException("Rethink");
            }
        }
        public static async Task SaveTextAsync<TKey, TValue>(this Dictionary<TKey, TValue> list, string path)
            where TKey : notnull
        {
            BasicList<string> output = new();
            foreach (var item in list)
            {
                output.Add($"{item.Key},{item.Value}");
            }
            await File.WriteAllLinesAsync(path, output, Encoding.UTF8);
        }
        public static async Task<Dictionary<TKey, TValue>> LoadTextDictionaryAsync<TKey, TValue>(this string path, string delimiter = ",")
            where TKey : notnull
        {
            Type key = typeof(TKey);
            bool rets = key.IsSimpleType();
            if (rets == false)
            {
                throw new CustomBasicException("First item is not simple for dictionary");
            }
            Type value = typeof(TValue);
            if (rets == false)
            {
                throw new CustomBasicException("Second item is not simple for dictionary");
            }
            if (File.Exists(path) == false)
            {
                return new Dictionary<TKey, TValue>();
            }
            var lines = await File.ReadAllLinesAsync(path);

            Dictionary<TKey, TValue> output = new();
            foreach (var line in lines)
            {
                var fins = line.Split(delimiter);
                if (fins.Length != 2)
                {
                    throw new CustomBasicException("Must have 2 items for a dictionary.  Rethink");
                }
                string firstString = fins[0];
                string secondString = fins[1];
                object? firstValue = GetValue(firstString, key);
                if (firstValue == null)
                {
                    throw new CustomBasicException("I don't think that key can be null for dictionary");
                }
                object? secondValue = GetValue(secondString, value);
                if (secondValue == null)
                {
                    throw new CustomBasicException("I don't think the value can be null for dictionary");
                }
                output.Add((TKey)firstValue, (TValue)secondValue);
            }
            return output;
        }

        public static async Task<BasicList<T>> LoadTextListFromResourceAsync<T>(this Assembly assembly, string name, string delimiter = ",")
        {
            string content = await assembly.ResourcesAllTextFromFileAsync(name);
            return content.DeserializeDelimitedTextList<T>(delimiter);
        }

        public static BasicList<T> LoadTextFromListResource<T>(this Assembly assembly, string name, string delimiter = ",")
        {
            string content = assembly.ResourcesAllTextFromFile(name);
            return content.DeserializeDelimitedTextList<T>(delimiter);
        }

        //return Activator.CreateInstance(thisType, args);

        public static BasicList<T> DeserializeDelimitedTextList<T>(this string content, string delimiter = ",")
        {
            //has to convert to a list first.
            BasicList<T> output;
            Type key = typeof(T); //if this works, then i can have a list of numbers, etc.
            if (key.IsSimpleType())
            {
                var temps = content.Split(delimiter).ToBasicList();
                output = temps.ToCastedList<T>();
                return output;
            }
            output = new();
            BasicList<string> lines = content.Split(Constants.VBCrLf).ToBasicList();
            var properties = GetProperties<T>();
            foreach (var line in lines)
            {
                var items = line.Split(delimiter).ToBasicList();
                if (items.Count != properties.Count)
                {
                    throw new CustomBasicException("Text file corrupted because the delimiter count don't match the properties");
                }
                //if i decide to ignore, then won't be a problem.  don't worry for now.
                int x = 0;
                object temp = Activator.CreateInstance(typeof(T))!;
                T row = (T)temp;
                //T row = new T();
                properties.ForEach(p =>
                {
                    string item = items[x];
                    PopulateValue(item, row, p);
                    x++;
                });
                output.Add(row);
            }
            return output;
        }

        /// <summary>
        /// this will load the text file and return a list of the object you want.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="path"></param>
        /// <param name="delimiter"></param>
        /// <returns></returns>
        public static async Task<BasicList<T>> LoadTextListAsync<T>(this string path, string delimiter = ",")
        {
            BasicList<T> output = new();
            if (File.Exists(path) == false)
            {
                return output;
            }
            string content = await File.ReadAllTextAsync(path);
            output = content.DeserializeDelimitedTextList<T>(delimiter);
            return output;
        }

        private static void PopulateValue<T>(string item, T row, PropertyInfo p)
        {
            if (p.PropertyType == typeof(int))
            {
                //try to parse to integers.
                bool rets = int.TryParse(item, out int y);
                if (rets == false)
                {
                    throw new CustomBasicException($"When trying to parse column {p.Name}, failed to parse to integer.  Means corruption");
                }
                p.SetValue(row, y); //hopefully this simple.
            }
            else if (p.PropertyType == typeof(int?))
            {
                if (item == "")
                {
                    p.SetValue(row, null);
                }
                else
                {
                    bool rets = int.TryParse(item, out int y);
                    if (rets == false)
                    {
                        throw new CustomBasicException($"When trying to parse column {p.Name}, failed to parse to integer.  Means corruption");
                    }
                    p.SetValue(row, y); //hopefully this simple.
                }
            }

            else if (p.PropertyType.IsEnum)
            {
                bool rets = int.TryParse(item, out int y);
                if (rets == false)
                {
                    throw new CustomBasicException($"When trying to parse column {p.Name}, failed to parse to enum.  Means corruption");
                }
                p.SetValue(row, y); //hopefully this simple (?)
            }

            else if (p.PropertyType.IsNullableEnum())
            {
                if (item == "")
                {
                    p.SetValue(row, null);
                }
                else
                {
                    bool rets = int.TryParse(item, out int y);
                    if (rets == false)
                    {
                        throw new CustomBasicException($"When trying to parse column {p.Name}, failed to parse to enum.  Means corruption");
                    }
                    p.SetValue(row, y); //hopefully this simple (?)
                }
            }
            else if (p.PropertyType == typeof(bool))
            {
                bool rets = bool.TryParse(item, out bool y);
                if (rets == false)
                {
                    throw new CustomBasicException($"When trying to parse column {p.Name}, failed to parse to boolean.  Means corruption");
                }
                p.SetValue(row, y); //hopefully this simple (?)

            }
            else if (p.PropertyType == typeof(bool?))
            {
                if (item == "")
                {
                    p.SetValue(row, null);
                }
                else
                {
                    bool rets = bool.TryParse(item, out bool y);
                    if (rets == false)
                    {
                        throw new CustomBasicException($"When trying to parse column {p.Name}, failed to parse to boolean.  Means corruption");
                    }
                    p.SetValue(row, y); //hopefully this simple (?)
                }
            }

            else if (p.PropertyType == typeof(decimal))
            {
                bool rets = decimal.TryParse(item, out decimal y);
                if (rets == false)
                {
                    throw new CustomBasicException($"When trying to parse column {p.Name}, failed to parse to decimal.  Means corruption");
                }
                p.SetValue(row, y); //hopefully this simple (?)

            }
            else if (p.PropertyType == typeof(decimal?))
            {
                if (item == "")
                    p.SetValue(row, null);
                else
                {
                    bool rets = decimal.TryParse(item, out decimal y);
                    if (rets == false)
                    {
                        throw new CustomBasicException($"When trying to parse column {p.Name}, failed to parse to decimal.  Means corruption");
                    }
                    p.SetValue(row, y); //hopefully this simple (?)
                }
            }
            else if (p.PropertyType == typeof(float))
            {
                bool rets = float.TryParse(item, out float y);
                if (rets == false)
                {
                    throw new CustomBasicException($"When trying to parse column {p.Name}, failed to parse to float.  Means corruption");
                }
                p.SetValue(row, y); //hopefully this simple (?)

            }
            else if (p.PropertyType == typeof(float?))
            {
                if (item == "")
                {
                    p.SetValue(row, null);
                }
                else
                {
                    bool rets = float.TryParse(item, out float y);
                    if (rets == false)
                    {
                        throw new CustomBasicException($"When trying to parse column {p.Name}, failed to parse to float.  Means corruption");
                    }
                    p.SetValue(row, y); //hopefully this simple (?)
                }
            }

            else if (p.PropertyType == typeof(double))
            {
                bool rets = double.TryParse(item, out double y);
                if (rets == false)
                {
                    throw new CustomBasicException($"When trying to parse column {p.Name}, failed to parse to double.  Means corruption");
                }
                p.SetValue(row, y); //hopefully this simple (?)

            }
            else if (p.PropertyType == typeof(double?))
                if (item == "")
                {
                    p.SetValue(row, null);
                }
                else
                {
                    bool rets = double.TryParse(item, out double y);
                    if (rets == false)
                    {
                        throw new CustomBasicException($"When trying to parse column {p.Name}, failed to parse to double.  Means corruption");
                    }
                    p.SetValue(row, y); //hopefully this simple (?)
                }
            else if (p.PropertyType == typeof(DateTime))
            {
                bool rets = DateTime.TryParse(item, out DateTime y);
                if (rets == false)
                {
                    throw new CustomBasicException($"When trying to parse column {p.Name}, failed to parse to datetime.  Means corruption");
                }
                p.SetValue(row, y); //hopefully this simple (?)

            }
            else if (p.PropertyType == typeof(DateTime?))
                if (item == "")
                {
                    p.SetValue(row, null);
                }
                else
                {
                    bool rets = DateTime.TryParse(item, out DateTime y);
                    if (rets == false)
                    {
                        throw new CustomBasicException($"When trying to parse column {p.Name}, failed to parse to datetime.  Means corruption");
                    }
                    p.SetValue(row, y); //hopefully this simple (?)
                }
            else if (p.PropertyType == typeof(DateTimeOffset))
            {
                bool rets = DateTimeOffset.TryParse(item, out DateTimeOffset y);
                if (rets == false)
                {
                    throw new CustomBasicException($"When trying to parse column {p.Name}, failed to parse to datetimeoffset.  Means corruption");
                }
                p.SetValue(row, y); //hopefully this simple (?)

            }
            else if (p.PropertyType == typeof(DateTimeOffset?))
                if (item == "")
                {
                    p.SetValue(row, null);
                }
                else
                {
                    bool rets = DateTimeOffset.TryParse(item, out DateTimeOffset y);
                    if (rets == false)
                    {
                        throw new CustomBasicException($"When trying to parse column {p.Name}, failed to parse to datetimeoffset.  Means corruption");
                    }
                    p.SetValue(row, y); //hopefully this simple (?)
                }
            else if (p.PropertyType == typeof(Guid))
            {
                bool rets = Guid.TryParse(item, out Guid y);
                if (rets == false)
                {
                    throw new CustomBasicException($"When trying to parse column {p.Name}, failed to parse to guid.  Means corruption");
                }
                p.SetValue(row, y); //hopefully this simple (?)
            }
            else if (p.PropertyType == typeof(Guid?))
                if (item == "")
                {
                    p.SetValue(row, null);
                }
                else
                {
                    bool rets = Guid.TryParse(item, out Guid y);
                    if (rets == false)
                    {
                        throw new CustomBasicException($"When trying to parse column {p.Name}, failed to parse to guid.  Means corruption");
                    }
                    p.SetValue(row, y); //hopefully this simple (?)
                }
            else if (p.PropertyType == typeof(char))
            {
                bool rets = char.TryParse(item, out char y);
                if (rets == false)
                {
                    throw new CustomBasicException($"When trying to parse column {p.Name}, failed to parse to char.  Means corruption");
                }
                p.SetValue(row, y); //hopefully this simple (?)
            }
            else if (p.PropertyType == typeof(char?))
                if (item == "")
                {
                    p.SetValue(row, null);
                }
                else
                {
                    bool rets = char.TryParse(item, out char y);
                    if (rets == false)
                    {
                        throw new CustomBasicException($"When trying to parse column {p.Name}, failed to parse to char.  Means corruption");
                    }
                    p.SetValue(row, y); //hopefully this simple (?)
                }
            else if (p.PropertyType == typeof(short))
            {
                bool rets = short.TryParse(item, out short y);
                if (rets == false)
                {
                    throw new CustomBasicException($"When trying to parse column {p.Name}, failed to parse to short.  Means corruption");
                }
                p.SetValue(row, y); //hopefully this simple (?)
            }
            else if (p.PropertyType == typeof(short?))
                if (item == "")
                {
                    p.SetValue(row, null);
                }
                else
                {
                    bool rets = short.TryParse(item, out short y);
                    if (rets == false)
                    {
                        throw new CustomBasicException($"When trying to parse column {p.Name}, failed to parse to short.  Means corruption");
                    }
                    p.SetValue(row, y); //hopefully this simple (?)
                }
            else if (p.PropertyType == typeof(ushort))
            {
                bool rets = ushort.TryParse(item, out ushort y);
                if (rets == false)
                {
                    throw new CustomBasicException($"When trying to parse column {p.Name}, failed to parse to ushort.  Means corruption");
                }
                p.SetValue(row, y); //hopefully this simple (?)
            }
            else if (p.PropertyType == typeof(ushort?))
                if (item == "")
                {
                    p.SetValue(row, null);
                }
                else
                {
                    bool rets = ushort.TryParse(item, out ushort y);
                    if (rets == false)
                    {
                        throw new CustomBasicException($"When trying to parse column {p.Name}, failed to parse to ushort.  Means corruption");
                    }
                    p.SetValue(row, y); //hopefully this simple (?)
                }
            else if (p.PropertyType == typeof(uint))
            {
                bool rets = uint.TryParse(item, out uint y);
                if (rets == false)
                {
                    throw new CustomBasicException($"When trying to parse column {p.Name}, failed to parse to uint.  Means corruption");
                }
                p.SetValue(row, y); //hopefully this simple (?)
            }
            else if (p.PropertyType == typeof(uint?))
            {
                if (item == "")
                {
                    p.SetValue(row, null);
                }
                else
                {
                    bool rets = uint.TryParse(item, out uint y);
                    if (rets == false)
                    {
                        throw new CustomBasicException($"When trying to parse column {p.Name}, failed to parse to uint.  Means corruption");
                    }
                    p.SetValue(row, y); //hopefully this simple (?)
                }
            }
            else if (p.PropertyType == typeof(long))
            {
                bool rets = long.TryParse(item, out long y);
                if (rets == false)
                {
                    throw new CustomBasicException($"When trying to parse column {p.Name}, failed to parse to long.  Means corruption");
                }
                p.SetValue(row, y); //hopefully this simple (?)
            }
            else if (p.PropertyType == typeof(long?))
                if (item == "")
                {
                    p.SetValue(row, null);
                }
                else
                {
                    bool rets = long.TryParse(item, out long y);
                    if (rets == false)
                    {
                        throw new CustomBasicException($"When trying to parse column {p.Name}, failed to parse to long.  Means corruption");
                    }
                    p.SetValue(row, y); //hopefully this simple (?)
                }
            else if (p.PropertyType == typeof(ulong))
            {
                bool rets = ulong.TryParse(item, out ulong y);
                if (rets == false)
                {
                    throw new CustomBasicException($"When trying to parse column {p.Name}, failed to parse to ulong.  Means corruption");
                }
                p.SetValue(row, y); //hopefully this simple (?)
            }
            else if (p.PropertyType == typeof(ulong?))
            {
                if (item == "")
                {
                    p.SetValue(row, null);
                }
                else
                {
                    bool rets = ulong.TryParse(item, out ulong y);
                    if (rets == false)
                    {
                        throw new CustomBasicException($"When trying to parse column {p.Name}, failed to parse to ulong.  Means corruption");
                    }
                    p.SetValue(row, y); //hopefully this simple (?)
                }
            }
            else if (p.PropertyType == typeof(string))
            {
                p.SetValue(row, item); //easiest part
            }
            else
            {
                throw new CustomBasicException($"Property with name of {p.Name} has unsupported property type of {p.PropertyType.Name}.  Rethink");
            }
        }
    }
}