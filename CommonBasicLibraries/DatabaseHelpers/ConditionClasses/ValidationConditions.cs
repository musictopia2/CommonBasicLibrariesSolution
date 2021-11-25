namespace CommonBasicLibraries.DatabaseHelpers.ConditionClasses;
public class ValidationConditions : IValidateConditions
{
    //this is default implementation.  anybody else can have a different way to validate if necessary
    public bool IsValid(BasicList<ICondition> conditionList, out string message)
    {
        //should be only one of 2 kinds of lists.
        if (conditionList.Count(Items => Items.ConditionCategory == EnumConditionCategory.ListInclude) > 1)
        {
            message = "The Include Condition List Contains More Than One List";
            return false;
        }
        if (conditionList.Count(Items => Items.ConditionCategory == EnumConditionCategory.ListNot) > 1)
        {
            message = "The Exclude Condition List Contains More Than One List";
            return false;
        }
        message = ""; //since i am forced to pass back the message
        return true;
    }
}