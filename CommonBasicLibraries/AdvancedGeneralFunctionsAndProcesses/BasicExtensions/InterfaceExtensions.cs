namespace CommonBasicLibraries.AdvancedGeneralFunctionsAndProcesses.BasicExtensions;
public static class InterfaceExtensions
{
    extension<T>(IMappable payLoad)
        where T: new()
    {
        public T AutoMap()
        {
            ICustomMapContext<T>? context = CustomMapperHelpers<T>.MasterContext ?? throw new CustomBasicException($"There was no mappings found at {typeof(T)}.  Try to use source generators to create it");
            return context.AutoMap(payLoad);
        }
    }
    extension(IChangeTracker entity)
    {
        public bool IsUpdate(object? oldValue, object? newValue)
        {
            if (entity is null)
            {
                throw new CustomBasicException("Entity was null");
            }
            if (oldValue == null && newValue == null)
            {
                return false;
            }
            if (oldValue == null)
            {
                return true;
            }
            return oldValue.Equals(newValue) == false;
        }
    }
}