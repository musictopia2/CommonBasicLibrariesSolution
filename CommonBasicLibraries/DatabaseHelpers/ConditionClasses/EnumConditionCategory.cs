namespace CommonBasicLibraries.DatabaseHelpers.ConditionClasses
{
    public enum EnumConditionCategory
    {
        And,
        Or,
        ListInclude, //i think by doing it this way, then it can use for validation.
        ListNot
    }
}