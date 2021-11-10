namespace CommonBasicLibraries.DatabaseHelpers.ConditionClasses
{
    public abstract class BaseListCondition
    {
        public BasicList<int> ItemList { get; set; } = new(); //so i can send in a list and things will be done to specific items.
    }
}