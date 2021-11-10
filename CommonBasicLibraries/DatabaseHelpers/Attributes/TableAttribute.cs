namespace CommonBasicLibraries.DatabaseHelpers.Attributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public class TableAttribute : Attribute
    {
        public TableAttribute(string tableName)
        {
            TableName = tableName;
        }
        public string TableName { get; private set; }
    }
}