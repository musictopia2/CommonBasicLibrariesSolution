using CommonBasicLibraries.DatabaseHelpers.ConditionClasses;
using CommonBasicLibraries.CollectionClasses;
using CommonBasicLibraries.DatabaseHelpers.EntityInterfaces;
using static CommonBasicLibraries.DatabaseHelpers.MiscClasses.SortInfo;
using cs = CommonBasicLibraries.DatabaseHelpers.ConditionClasses.ConditionOperators;
using CommonBasicLibraries.BasicDataSettingsAndProcesses;
namespace CommonBasicLibraries.DatabaseHelpers.MiscClasses
{
    public static class StaticHelpers
    {
        public static BasicList<ICondition> StartConditionWithID(int id)
        {
            BasicList<ICondition> list = new ();
            AndCondition condition = new ();
            condition.Property = nameof(ISimpleDapperEntity.ID);
            condition.Value = id;
            list.Add(condition);
            return list;
        }

        public static BasicList<ICondition> StartWithOneCondition(string property, object value)
        {
            BasicList<ICondition> list = new ();
            AndCondition condition = new ();
            condition.Property = property;
            condition.Value = value;
            list.Add(condition);
            return list;
        }
        public static BasicList<ICondition> StartWithNullCondition(string property, string operation)
        {
            BasicList<ICondition> list = new ();
            AndCondition condition = new ();
            condition.Property = property;
            if (operation != cs.IsNotNull && operation != cs.IsNull)
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
            BasicList<ICondition> list = new ();
            AndCondition condition = new ();
            condition.Property = property;
            condition.Value = value;
            condition.Operator = operation;
            list.Add(condition);
            return list;
        }

        public static BasicList<SortInfo> StartSorting(string property)
        {
            SortInfo sort = new ();
            sort.Property = property;
            return new () { sort };
        }
        public static BasicList<SortInfo> StartSorting(string property, EnumOrderBy order)
        {
            SortInfo sort = new ();
            sort.Property = property;
            sort.OrderBy = order;
            return new () { sort };
        }
        public static BasicList<UpdateEntity> StartUpdate(string property, object value)
        {
            BasicList<UpdateEntity> output = new ()
            {
                new UpdateEntity(property, value)
            };
            return output;
        }
    }
}
