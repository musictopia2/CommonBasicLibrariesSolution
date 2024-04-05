namespace CommonBasicLibraries.DatabaseHelpers.MiscClasses;
public static class StaticHelpers
{
    public static BasicList<ICondition> StartConditionWithID(int id)
    {
        BasicList<ICondition> list = [];
        AndCondition condition = new();
        condition.Property = nameof(ISimpleDatabaseEntity.ID);
        condition.Value = id;
        list.Add(condition);
        return list;
    }
    public static BasicList<ICondition> StartWithOrConditions(Action<OrCondition> others)
    {
        BasicList<ICondition> list = [];
        OrCondition condition = new();
        others.Invoke(condition);
        list.Add(condition);
        return list;
    }
    public static BasicList<ICondition> StartWithOrConditions<T>(string property, BasicList<T> values)
    {
        BasicList<ICondition> list = [];
        OrCondition condition = new();
        foreach (var item in values)
        {
            condition.AppendOr(property, item);
        }
        list.Add(condition);
        return list;
    }
    public static BasicList<ICondition> StartWithOrConditions<T>(string property, string operation, BasicList<T> values)
    {
        BasicList<ICondition> list = [];
        OrCondition condition = new();
        foreach (var item in values)
        {
            condition.AppendOr(property, operation, item);
        }
        list.Add(condition);
        return list;
    }
    public static BasicList<ICondition> StartWithOneCondition(string property, object value)
    {
        BasicList<ICondition> list = [];
        AndCondition condition = new();
        condition.Property = property;
        condition.Value = value;
        list.Add(condition);
        return list;
    }
    public static BasicList<ICondition> StartWithNullCondition(string property, string operation)
    {
        BasicList<ICondition> list = [];
        AndCondition condition = new();
        condition.Property = property;
        if (operation != co1.IsNotNull && operation != co1.IsNull)
        {
            throw new CustomBasicException("Only null or is not null is allowed when starting with null conditions");
        }
        //this was needed for the tv shows.
        condition.Operator = operation;
        list.Add(condition);
        return list;
    }
    public static BasicList<ICondition> StartWithOneCondition(string property, string operation, object value)
    {
        BasicList<ICondition> list = [];
        AndCondition condition = new();
        condition.Property = property;
        condition.Value = value;
        condition.Operator = operation;
        list.Add(condition);
        return list;
    }
    public static BasicList<SortInfo> StartSorting(string property)
    {
        SortInfo sort = new();
        sort.Property = property;
        return [sort];
    }
    public static BasicList<SortInfo> StartSorting(string property, EnumOrderBy order)
    {
        SortInfo sort = new();
        sort.Property = property;
        sort.OrderBy = order;
        return [sort];
    }
    public static BasicList<UpdateEntity> StartUpdate(string property, object value)
    {
        BasicList<UpdateEntity> output =
        [
            new UpdateEntity(property, value)
        ];
        return output;
    }
}