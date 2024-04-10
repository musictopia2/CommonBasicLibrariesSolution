namespace CommonBasicLibraries.DatabaseHelpers.ConditionClasses;
public class OrCondition : ICondition
{
    EnumConditionCategory ICondition.ConditionCategory { get => EnumConditionCategory.Or; }
    public BasicList<AndCondition> ConditionList = [];
}