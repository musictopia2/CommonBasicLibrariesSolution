using CommonBasicLibraries.CollectionClasses;
namespace CommonBasicLibraries.DatabaseHelpers.EntityInterfaces
{
    public interface IUpdatableEntity : ISimpleDapperEntity
    {
        void Initialize(); //if i initialize before reaching client, then it would have to send the dictionary to the client.
        BasicList<string> GetChanges(); //only needs to know the fields that changed.
    }
}