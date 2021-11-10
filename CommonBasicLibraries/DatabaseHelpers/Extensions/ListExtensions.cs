namespace CommonBasicLibraries.DatabaseHelpers.Extensions
{
    public static class ListExtensions
    {
        public static string GetNewValue(this Dictionary<string, int> thisDict, string parameter)
        {
            bool rets;
            rets = thisDict.TryGetValue(parameter, out int Value);
            if (rets == true)
            {
                Value++;
                thisDict[parameter] = Value;
                return $"{parameter}{Value}";
            }
            thisDict.Add(parameter, 1);
            return $"{parameter}1";
        }
        public static BasicList<int> GetIDList<E>(this BasicList<E> thisList) where E : ISimpleDapperEntity
        {
            return thisList.Select(Items => Items.ID).ToBasicList();
        }
        public static void InitalizeAll<E>(this BasicList<E> thisList) where E : IUpdatableEntity
        {
            thisList.ForEach(Items => Items.Initialize());
        }
        public static BasicList<SortInfo> Append(this BasicList<SortInfo> sortList, string property)
        {
            return sortList.Append(property, EnumOrderBy.Ascending);
        }
        public static BasicList<SortInfo> Append(this BasicList<SortInfo> sortList, string property, EnumOrderBy orderBy)
        {
            sortList.Add(new SortInfo() { Property = property, OrderBy = orderBy });
            return sortList;
        }
        public static BasicList<UpdateFieldInfo> Append(this BasicList<UpdateFieldInfo> tempList, string thisProperty)
        {
            tempList.Add(new UpdateFieldInfo(thisProperty));
            return tempList;
        }

        public static BasicList<UpdateFieldInfo> Append(this BasicList<UpdateFieldInfo> tempList, BasicList<string> propList)
        {
            propList.ForEach(items =>
            {
                tempList.Add(new UpdateFieldInfo(items));
            });
            return tempList;
        }
        public static BasicList<UpdateEntity> Append(this BasicList<UpdateEntity> tempList, string thisProperty, object value)
        {
            tempList.Add(new UpdateEntity(thisProperty, value));
            return tempList;
        }
    }
}