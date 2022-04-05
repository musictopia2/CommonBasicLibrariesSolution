namespace CommonBasicLibraries.NoSqlHelpers.Interfaces;
public interface INoSqlModel
{
    string Id { get; set; }
    string CollectionName { get; } //most no sql databases require collection names
}
