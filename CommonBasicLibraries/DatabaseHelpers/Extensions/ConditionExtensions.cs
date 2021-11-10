namespace CommonBasicLibraries.DatabaseHelpers.Extensions
{
    public static class ConditionExtensions
    {
        public static BasicList<ICondition> AppendCondition(this BasicList<ICondition> tempList, string property, object value)
        {
            return tempList.AppendCondition(property, co.Equals, value);
        }
        public static BasicList<ICondition> AppendRangeCondition(this BasicList<ICondition> tempList, string property,
            object lowRange, object highRange)
        {
            return tempList.AppendCondition(property, co.GreaterOrEqual, lowRange).AppendCondition(property, co.LessThanOrEqual, highRange);
        }
        public static BasicList<ICondition> AppendCondition(this BasicList<ICondition> tempList, string property, string toperator, object value)
        {
            AndCondition thisCon = new();
            thisCon.Property = property;
            thisCon.Operator = toperator;
            thisCon.Value = value;
            tempList.Add(thisCon);
            return tempList;
        }
        public static BasicList<ICondition> JoinedCondition(this BasicList<ICondition> tempList, string tableCode)
        {
            AndCondition thisCon = (AndCondition)tempList.Last();
            thisCon.Code = tableCode;
            return tempList;
        }

        public static BasicList<ICondition> AppendContains(this BasicList<ICondition> tempList, BasicList<int> containList)
        {
            SpecificListCondition thisCon = new();
            thisCon.ItemList = containList;
            tempList.Add(thisCon);
            return tempList;
        }
        public static BasicList<ICondition> AppendsNot(this BasicList<ICondition> tempList, BasicList<int> notList)
        {
            NotListCondition thiscon = new();
            thiscon.ItemList = notList;
            tempList.Add(thiscon);
            return tempList;
        }
        public static OrCondition AppendOr(this OrCondition thisOr, string property, object? value)
        {
            var thisCon = new AndCondition();
            thisCon.Property = property;
            thisCon.Value = value;
            thisOr.ConditionList.Add(thisCon);
            return thisOr;
        }
        public static OrCondition AppendOr(this OrCondition thisOr, string property, string toperator, object? value)
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