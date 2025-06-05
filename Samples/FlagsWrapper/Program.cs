jc1.AddEnumFlagSerializationOptions<EnumSampleFlags>();
SampleModel model = new()
{
    Name = "Test Model"
};

model.Flags.Add(EnumSampleFlags.OptionA);
model.Flags.Add(EnumSampleFlags.OptionC);

Console.WriteLine($"Model Name: {model.Name}");
Console.WriteLine($"Has OptionA? {model.Flags.Has(EnumSampleFlags.OptionA)}"); // True
Console.WriteLine($"Has OptionB? {model.Flags.Has(EnumSampleFlags.OptionB)}"); // False
Console.WriteLine($"Raw Flags Value: {model.FlagsRaw()}"); // Should print 5 (1 + 4)

// Remove OptionA
model.Flags.Remove(EnumSampleFlags.OptionA);
Console.WriteLine($"Has OptionA after removal? {model.Flags.Has(EnumSampleFlags.OptionA)}"); // False
Console.WriteLine($"Raw Flags Value after removal: {model.FlagsRaw()}"); // Should print 4


string text = await jj1.SerializeObjectAsync(model);
Console.WriteLine(text);
SampleModel fins = await jj1.DeserializeObjectAsync<SampleModel>(text);
Console.WriteLine(fins.FlagsRaw());
Console.WriteLine($"Has OptionC? {fins.Flags.Has(EnumSampleFlags.OptionC)}"); // True

Console.WriteLine("Check");
