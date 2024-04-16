namespace CommonBasicLibraries.AdvancedGeneralFunctionsAndProcesses.BsonHelpers;
public class BCustomList : BValue, IEnumerable
{
    private readonly BasicList<BValue> _items = [];
    public BCustomList() : base(EnumBValueType.CustomList)
    {
    }
    public override BValue this[int index]
    {
        get => _items[index];
        set => _items[index] = value;
    }
    public BasicList<BValue> FullList => _items;
    public bool HasValues => _items.Count > 0;
    public int Count => _items.Count;
    public override void Add(BValue v) => _items.Add(v);
    public int IndexOf(BValue item) => _items.IndexOf(item);
    public void Insert(int index, BValue item) => _items.InsertMiddle(index, item);
    public bool Remove(BValue v) => _items.RemoveSpecificItem(v);
    public void RemoveAt(int index) => _items.RemoveAt(index);
    public override void Clear() => _items.Clear();
    public override bool Contains(BValue v) => _items.Contains(v);
    IEnumerator IEnumerable.GetEnumerator()
    {
        return _items.GetEnumerator();
    }
}