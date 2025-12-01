namespace CommonBasicLibraries.DatabaseHelpers.Extensions;
public static class ListExtensions
{
    extension(Dictionary<string, int> list)
    {
        public string GetNewValue(string parameter)
        {
            bool rets;
            rets = list.TryGetValue(parameter, out int Value);
            if (rets == true)
            {
                Value++;
                list[parameter] = Value;
                return $"{parameter}{Value}";
            }
            list.Add(parameter, 1);
            return $"{parameter}1";
        }
    }
    extension<E>(BasicList<E> list)
        where E: ISimpleDatabaseEntity
    {
        public BasicList<int> GetIDList()
        {
            return list.Select(xx => xx.ID).ToBasicList();
        }
    }
    extension<E>(IList<E> list)
        where E: IChangeTracker
    {
        public void InitalizeAll()
        {
            foreach (E e in list)
            {
                e.Initialize();
            }
        }
    }
    extension(BasicList<SortInfo> sortList)
    {
        public BasicList<SortInfo> Append(string property)
        {
            return sortList.Append(property, EnumOrderBy.Ascending);
        }
        public BasicList<SortInfo> Append(string property, EnumOrderBy orderBy)
        {
            sortList.Add(new SortInfo() { Property = property, OrderBy = orderBy });
            return sortList;
        }
    }
    extension(BasicList<UpdateFieldInfo> list)
    {
        public BasicList<UpdateFieldInfo> Append(string thisProperty)
        {
            list.Add(new UpdateFieldInfo(thisProperty));
            return list;
        }
        public BasicList<UpdateFieldInfo> Append(IEnumerable<string> propList)
        {
            foreach (string prop in propList)
            {
                list.Add(new UpdateFieldInfo(prop));
            }
            return list;
        }
    }
    extension(BasicList<UpdateEntity> list)
    {
        public BasicList<UpdateEntity> Append(string thisProperty, object value)
        {
            list.Add(new UpdateEntity(thisProperty, value));
            return list;
        }
    }
}