using CommonBasicLibraries.CollectionClasses;
namespace CommonBasicLibraries.DatabaseHelpers.ConditionClasses
{
    public interface IValidateConditions
    {
        bool IsValid(BasicList<ICondition> conditionList, out string message);
    }
}