namespace CommonBasicLibraries.CollectionClasses;
public class WeightedAverageLists<T>
    where T : notnull
{
    private readonly Dictionary<T, int> _possibleList = [];
    private readonly Dictionary<int, int> _subList = [];
    private IRandomNumberList? _rs;
    //needs the possibility of another process like a testing framework to send the randoms.
    public void SendRandoms(IRandomNumberList? rs)
    {
        _rs = rs;
    }
    private void CaptureRandoms()
    {
        _rs ??= RandomHelpers.GetRandomGenerator();
    }
    public WeightedAverageLists<T> AddSubItem(int numberPossible, int weight)
    {
        _subList.Add(numberPossible, weight);
        return this;
    }
    public BasicList<int> GetSubList(bool needsToClear = true)
    {
        if (_subList.Count == 0)
        {
            throw new CustomBasicException("You never used the sublist");
        }
        BasicList<int> output = [];
        foreach (var item in _subList.Keys)
        {
            int howMany = _subList[item];
            howMany.Times(items => output.Add(item));
        }
        if (needsToClear == true)
        {
            _subList.Clear(); //so i can use next time.
        }
        return output;
    }
    public void FillExtraSubItems(int lowRange, int highRange)
    {
        for (int i = lowRange; i <= highRange; i++)
        {
            if (_subList.ContainsKey(i) == false)
            {
                _subList.Add(i, 1);
            }
        }
    }
    public int GetSubCount => GetSubList(false).Count;
    public WeightedAverageLists<T> AddWeightedItem(T thisItem)
    {
        var output = GetSubList();
        return AddWeightedItem(thisItem, output);
    }
    public WeightedAverageLists<T> AddWeightedItem(T thisItem, int weight)
    {
        if (weight == 0)
        {
            return this; //can't be chosen because the weight is 0
        }
        _possibleList.Add(thisItem, weight); //so if you can't runtime error because its already done.
        return this; //so i have fluency.
    }
    public WeightedAverageLists<T> AddWeightedItem(T thisItem, int lowRange, int highRange)
    {
        CaptureRandoms();
        int chosen = _rs!.GetRandomNumber(highRange, lowRange);
        return AddWeightedItem(thisItem, chosen);
    }
    public WeightedAverageLists<T> AddWeightedItem(T thisItem, BasicList<int> possList)
    {
        CaptureRandoms();
        possList.SendRandoms(_rs); //whatever is here will be transferred
        int chosen = possList.GetRandomItem();
        return AddWeightedItem(thisItem, chosen);
    }
    public BasicList<T> GetWeightedList()
    {
        BasicList<T> output = [];
        foreach (var thisItem in _possibleList.Keys)
        {
            int howMany = _possibleList[thisItem];
            howMany.Times(Items =>
            {
                output.Add(thisItem);
            });
        }
        output.SendRandoms(_rs); //i think that the randoms here should be sent to the other.
        return output;
    }
    public int GetExpectedCount => _possibleList.Sum(items => items.Value);
}