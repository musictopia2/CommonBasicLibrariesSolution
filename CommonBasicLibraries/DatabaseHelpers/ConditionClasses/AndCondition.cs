namespace CommonBasicLibraries.DatabaseHelpers.ConditionClasses
{
    public class AndCondition : ICondition, IProperty
    {
        public string Code { get; set; } = "";//this is used in cases where we have multiple tables.
        public string Property { get; set; } = "";
        public string Operator { get; set; } = "="; //decided to do it this way so a person has a choice.  if they use the strongly typed, should work.  if not, will be runtime error.
        public object? Value { get; set; } //can be nothing.
        EnumConditionCategory ICondition.ConditionCategory { get => EnumConditionCategory.And; }
    }
}