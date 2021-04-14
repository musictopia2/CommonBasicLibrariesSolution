namespace CommonBasicLibraries.DatabaseHelpers.ConditionClasses
{
    public class NotListCondition : BaseListCondition, ICondition
    {
        EnumConditionCategory ICondition.ConditionCategory => EnumConditionCategory.ListNot;
    }
}