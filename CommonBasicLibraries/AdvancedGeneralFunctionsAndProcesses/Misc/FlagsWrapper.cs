namespace CommonBasicLibraries.AdvancedGeneralFunctionsAndProcesses.Misc;
public class FlagsWrapper<TEnum> where TEnum : struct, Enum
{
    private ulong _flags;

    // Parameterless constructor for serializers
    public FlagsWrapper()
    {
        _flags = 0;
    }

    // Initialize with a single flag
    public FlagsWrapper(TEnum initialFlag)
    {
        _flags = Convert.ToUInt64(initialFlag);
    }

    // Add a flag
    public void Add(TEnum flag)
    {
        _flags |= Convert.ToUInt64(flag);
    }

    // Remove a flag
    public void Remove(TEnum flag)
    {
        _flags &= ~Convert.ToUInt64(flag);
    }

    // Check if a flag is set
    public bool Has(TEnum flag)
    {
        ulong f = Convert.ToUInt64(flag);
        return (_flags & f) == f;
    }

    // Clear all flags
    public void Clear()
    {
        _flags = 0;
    }

    // Get raw ulong value (for serialization or manual casting)
    public ulong RawValue => _flags;

    public void SetRawValue(ulong raw)
    {
        _flags = raw;
    }

    // Override ToString for convenience (optional)
    public override string ToString() => _flags.ToString();
}