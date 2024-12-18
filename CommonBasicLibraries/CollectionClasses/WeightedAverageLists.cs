namespace CommonBasicLibraries.CollectionClasses;
public class WeightedAverageLists<T>
    where T : notnull
{
    private readonly Dictionary<T, int> _possibleList = [];
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
    public void Clear()
    {
        _possibleList.Clear();
    }
    public WeightedAverageLists<T> AddWeightedItem(BasicList<T> list, int weight)
    {
        if (weight == 0)
        {
            return this; //can't be chosen because the weight is 0
        }
        foreach (var item in list)
        {
            _possibleList.Add(item, weight);
        }
        return this; //so i have fluency.
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
    public WeightedAverageLists<T> AddWeightedItemWithChance(T thisItem, int notPassingWeight, int firstWeight, int desiredWeight)
    {
        CaptureRandoms();
        int totalWeight = notPassingWeight + firstWeight;
        int ask = _rs!.GetRandomNumber(totalWeight);
        if (ask <= notPassingWeight)
        {
            return this;
        }
        return AddWeightedItem(thisItem, desiredWeight);
    }
    public T GetRandomWeightedItem()
    {
        if (_possibleList.Count == 0)
        {
            throw new CustomBasicException("Cannot select from an empty list");
        }
        CaptureRandoms();
        int totalWeight = _possibleList.Values.Sum();
        int randomValue = _rs!.GetRandomNumber(totalWeight);
        int cumulativeWeight = 0;
        // Iterate over items and find the one that matches the random value
        foreach (var item in _possibleList)
        {
            cumulativeWeight += item.Value;

            // If the random value falls within the cumulative weight of this item, select it
            if (randomValue <= cumulativeWeight)
            {
                T key = item.Key;
                return key;
                    
                //return new List<T> { item.Key };  // Return a list with the selected item
            }
        }
        throw new CustomBasicException("Nothing selected");
    }
    public int GetExpectedCount => _possibleList.Sum(items => items.Value);
    // Optionally, if the caller wants to see multiple items picked
    public BasicList<T> GetMultipleWeightedItems(int count)
    {
        BasicList<T> selectedItems = [];
        for (int i = 0; i < count; i++)
        {
            selectedItems.Add(GetRandomWeightedItem());  // Assumes list is non-empty after selection
        }
        return selectedItems;
    }


    public BasicList<T> GetMultipleItemsWithDynamicWeights(Action<WeightedAverageLists<T>> dynamicAction, int count)
    {
        BasicList<T> output = [];
        count.Times(x =>
        {
            _possibleList.Clear();  // Clear previous weights
            dynamicAction.Invoke(this);  // Dynamically add weighted items
            output.Add(GetRandomWeightedItem());  // Select the weighted item based on the current conditions
        });
        return output;
    }
}