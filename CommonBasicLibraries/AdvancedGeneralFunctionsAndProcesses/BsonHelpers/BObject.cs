namespace CommonBasicLibraries.AdvancedGeneralFunctionsAndProcesses.BsonHelpers;
public class BObject : BValue, IEnumerable
{
    private readonly Dictionary<string, BValue> _map = [];
    public BObject() : base(EnumBValueType.Object)
    {
    }
    public ICollection<string> Keys => _map.Keys;
    public ICollection<BValue> Values => _map.Values;
    public Dictionary<string, BValue> FullList => _map;
    public int Count => _map.Count;
    public override BValue this[string key]
    {
        get
        {
            _map.TryGetValue(key, out BValue? val);
            return val!;
        }
        set => _map[key] = value;
    }
    public override void Clear() => _map.Clear();
    public override void Add(string key, BValue value) => _map.Add(key, value);
    public override bool Contains(BValue v) => _map.ContainsValue(v);
    public override bool ContainsKey(string key) => _map.ContainsKey(key);
    public bool Remove(string key) => _map.Remove(key);
    public bool TryGetValue(string key, out BValue value) => _map.TryGetValue(key, out value!);
    IEnumerator IEnumerable.GetEnumerator()
    {
        return _map.GetEnumerator();
    }
}