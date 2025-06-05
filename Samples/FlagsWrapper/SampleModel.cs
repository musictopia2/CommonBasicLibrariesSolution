namespace FlagsWrapper;
public class SampleModel
{
    public string Name { get; set; } = "";

    public FlagsWrapper<EnumSampleFlags> Flags { get; set; } = new();


    public ulong FlagsRaw() => Flags.RawValue;

    // For serialization (if needed)
    //public ulong FlagsRaw
    //{
    //    get => Flags.RawValue;
    //    set => Flags.SetRawValue(value);
    //}
}