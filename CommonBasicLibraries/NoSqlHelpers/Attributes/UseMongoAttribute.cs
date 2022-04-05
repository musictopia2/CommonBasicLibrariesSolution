namespace CommonBasicLibraries.NoSqlHelpers.Attributes;
[AttributeUsage(AttributeTargets.Class)]
public class UseMongoAttribute : Attribute
{
    public UseMongoAttribute(string collectionName)
    {
        CollectionName = collectionName;
    }
    public string CollectionName { get; set; }
}