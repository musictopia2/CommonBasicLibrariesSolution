namespace CommonBasicLibraries.AdvancedGeneralFunctionsAndProcesses.BsonHelpers;
#pragma warning disable CS0660 // Type defines operator == or operator != but does not override Object.Equals(object o)
#pragma warning disable CS0661 // Type defines operator == or operator != but does not override Object.GetHashCode()
public class BValue
#pragma warning restore CS0661 // Type defines operator == or operator != but does not override Object.GetHashCode()
#pragma warning restore CS0660 // Type defines operator == or operator != but does not override Object.Equals(object o)
{
    private readonly EnumBValueType _valueType;
    private readonly double _double;
    private readonly string _string = ""; //try this.
    private readonly bool _bool;
    private readonly int _int32;
    public EnumBValueType ValueType => _valueType;
    public int Int32Value
    {
        get
        {
            return _valueType switch
            {
                EnumBValueType.Int32 => _int32,
                EnumBValueType.Double => (int)_double,
                _ => throw new Exception($"Original type is {_valueType}. Cannot convert from {_valueType} to Int32"),
            };
        }
    }
    public string StringValue
    {
        get
        {
            if (_valueType == EnumBValueType.Int32)
            {
                return Convert.ToString(_int32);
            }
            if (_valueType == EnumBValueType.Double)
            {
                return Convert.ToString(_double);
            }
            if (_valueType == EnumBValueType.String)
            {
                return _string?.TrimEnd((char)0)!;
            }
            throw new CustomBasicException($"Original type is {_valueType}. Cannot convert from {_valueType} to string");
        }
    }
    public bool BoolValue
    {
        get
        {
            return _valueType switch
            {
                EnumBValueType.Boolean => _bool,
                _ => throw new Exception($"Original type is {_valueType}. Cannot convert from {_valueType} to bool"),
            };
        }
    }
    public bool IsNone => _valueType == EnumBValueType.None;
    public virtual BValue this[string key]
    {
        get { return null!; }
        set { }
    }
    public virtual BValue this[int index]
    {
        get { return null!; }
        set { }
    }
    public virtual void Clear() { }
    public virtual void Add(string key, BValue value) { }
    public virtual void Add(BValue value) { }
    public virtual bool Contains(BValue v) { return false; }
    public virtual bool ContainsKey(string key) { return false; }
    public static implicit operator BValue(double v) => new(v);
    public static implicit operator BValue(int v) => new(v);
    public static implicit operator BValue(string v) => new(v);
    public static implicit operator int(BValue v) => v.Int32Value;
    public static implicit operator string(BValue v) => v.StringValue;
    protected BValue(EnumBValueType valueType)
    {
        _valueType = valueType;
    }
    public BValue()
    {
        _valueType = EnumBValueType.None;
    }
    public BValue(double v)
    {
        _valueType = EnumBValueType.Double;
        _double = v;
    }
    public BValue(string v)
    {
        _valueType = EnumBValueType.String;
        _string = v;
    }
    public BValue(bool v)
    {
        _valueType = EnumBValueType.Boolean;
        _bool = v;
    }
    public BValue(int v)
    {
        _valueType = EnumBValueType.Int32;
        _int32 = v;
    }
    public static bool operator ==(BValue a, object b) => ReferenceEquals(a, b);
    public static bool operator !=(BValue a, object b) => !(a == b);
}