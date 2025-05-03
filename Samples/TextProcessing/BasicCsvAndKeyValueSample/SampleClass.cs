namespace BasicCsvAndKeyValueSample;
public static class SampleClass
{
    public static void RunTest()
    {
        // Creating and initializing a Person object with key-value pairs
        var person = new Person
        {
            Name = "Taylor: Johnson, Jr.",
            Address = "123 Maple Ave: Suite 5, Springfield",
            Age = 34,
            SampleList = [8, 15, 23, 42, 108]
        };
        // Convert the Person object to a key-value pair formatted string
        string text = person.ToString();
        Console.WriteLine("Person Text (Key-Value Pairs):");
        Console.WriteLine(text);
        // Deserialize the string back into a Person object
        Person newPerson = Person.FromString(text);
        Console.WriteLine("New Person:");
        Console.WriteLine($"Name: {newPerson.Name}");
        Console.WriteLine($"Age: {newPerson.Age}");
        // Convert the new Person object back to a key-value pair format
        text = newPerson.ToString();
        Console.WriteLine("New Person (ToString result):");
        Console.WriteLine(text);
        // Serialize the list to a CSV string
        BasicList<string> firstList =
         [
            "John Doe, Sr.",
            "Los Angeles, California",
            "Software Developer",
            "1234 Oak Street, Apt 56",
            "Summer Vacation, 2025"
         ];
        text = cc1.Serialize(firstList);
        Console.WriteLine("Serialized First List:");
        Console.WriteLine(text);
        // Deserialize the list back into a BasicList<string>
        firstList = cc1.Deserialize<string>(text);
        Console.WriteLine("First Item from Deserialized List:");
        Console.WriteLine(firstList.First());
        // Example of serializing and deserializing a BasicList<int>
        BasicList<int> intList =
        [
            27,
            54,
            13,
            89,
            42
        ];
        // Serialize the integer list
        text = cc1.Serialize(intList);
        Console.WriteLine("Serialized Integer List:");
        Console.WriteLine(text);
        // Deserialize the integer list
        var fins = cc1.Deserialize<int>(text);
        Console.WriteLine("Third Item from Deserialized List (Expected 13):");
        Console.WriteLine(fins[2]); // Should be 13.
        // End of sample
        Console.WriteLine("Sample test completed.");
    }
}