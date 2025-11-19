namespace CommonBasicLibraries.DatabaseHelpers.Extensions;
public static class ConditionExtensions
{
    extension(IList<ICondition> conditions)
    {
        public IList<ICondition> AppendCondition(string property, object value)
        {
            return conditions.AppendCondition(property, co1.Equals, value);
        }
        public IList<ICondition> AppendRangeCondition(string property,
            object lowRange, object highRange)
        {
            return conditions.AppendCondition(property, co1.GreaterOrEqual, lowRange).AppendCondition(property, co1.LessThanOrEqual, highRange);
        }
        public IList<ICondition> AppendCondition(string property, string toperator, object value)
        {
            AndCondition thisCon = new();
            thisCon.Property = property;
            thisCon.Operator = toperator;
            thisCon.Value = value;
            conditions.Add(thisCon);
            return conditions;
        }
        public IList<ICondition> JoinedCondition(string tableCode)
        {
            AndCondition thisCon = (AndCondition)conditions.Last();
            thisCon.Code = tableCode;
            return conditions;
        }
        public IList<ICondition> AppendContains(BasicList<int> containList)
        {
            SpecificListCondition thisCon = new();
            thisCon.ItemList = containList;
            conditions.Add(thisCon);
            return conditions;
        }
        public IList<ICondition> AppendsNot(BasicList<int> notList)
        {
            NotListCondition thiscon = new();
            thiscon.ItemList = notList;
            conditions.Add(thiscon);
            return conditions;
        }
    }
    extension(OrCondition thisOr)
    {
        public OrCondition AppendOr(string property, object? value)
        {
            var thisCon = new AndCondition();
            thisCon.Property = property;
            thisCon.Value = value;
            thisOr.ConditionList.Add(thisCon);
            return thisOr;
        }
        public OrCondition AppendOr(string property, string toperator, object? value)
        {
            var thisCon = new AndCondition();
            thisCon.Property = property;
            thisCon.Operator = toperator;
            thisCon.Value = value;
            thisOr.ConditionList.Add(thisCon);
            return thisOr;
        }
    }
}