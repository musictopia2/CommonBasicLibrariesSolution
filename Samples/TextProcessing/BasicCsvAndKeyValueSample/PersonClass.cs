namespace BasicCsvAndKeyValueSample;
public class Person
{
    public string Name { get; set; } = "";
    public string Address { get; set; } = "";
    public int Age { get; set; }
    public BasicList<int> SampleList { get; set; } = [];

    // Serialize the object
    public override string ToString()
    {
        string text = string.Join(",", SampleList);

        Dictionary<string, string> pairs = [];
        pairs.Add("Name", Name);
        pairs.Add("Address", Address);
        pairs.Add("Age", Age.ToString());
        pairs.Add("SampleList", text);
        return kk1.Serialize(pairs);
        
    }

    // Deserialize the object from string
    public static Person FromString(string input)
    {
        var person = new Person();
        var keyValuePairs = kk1.Deserialize(input);

        //var keyValuePairs = KeyValueParser.ParseKeyValuePairs(input);
        // Populate the object manually based on the key-value pairs
        foreach (var pair in keyValuePairs)
        {
            switch (pair.Key)
            {
                case "Name":
                    person.Name = pair.Value;
                    break;
                case "Address":
                    person.Address = pair.Value;
                    break;
                case "Age":
                    person.Age = int.Parse(pair.Value);
                    break;
                case "SampleList":
                    person.SampleList = cc1.Deserialize<int>(pair.Value);
                    break;
                default:
                    throw new FormatException($"Unknown property: {pair.Key}");
            }
        }
        return person;
    }
}