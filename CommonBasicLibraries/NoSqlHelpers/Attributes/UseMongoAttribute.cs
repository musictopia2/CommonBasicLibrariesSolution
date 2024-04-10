namespace CommonBasicLibraries.NoSqlHelpers.Attributes;
[AttributeUsage(AttributeTargets.Class)]
public class UseMongoAttribute(string collectionName) : Attribute
{
    public string CollectionName { get; set; } = collectionName;
}