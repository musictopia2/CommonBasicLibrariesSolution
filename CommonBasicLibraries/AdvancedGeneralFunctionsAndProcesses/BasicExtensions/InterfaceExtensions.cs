﻿namespace CommonBasicLibraries.AdvancedGeneralFunctionsAndProcesses.BasicExtensions;
public static class InterfaceExtensions
{
    public static T AutoMap<T>(this IMappable payLoad)
        where T: new()
    {
        ICustomMapContext<T>? context = CustomMapperHelpers<T>.MasterContext ?? throw new CustomBasicException($"There was no mappings found at {typeof(T)}.  Try to use source generators to create it");
        return context.AutoMap(payLoad); //decided to do away with the dictionary way of doing it to improve performance.
    }
    public static bool IsUpdate(this IUpdatableEntity entity, object? thisValue, object? newValue)
    {
        if (entity is null)
        {
            throw new CustomBasicException("Entity was null");
        }
        if (thisValue == null && newValue == null)
        {
            return false;
        }
        if (thisValue == null)
        {
            return true;
        }
        return thisValue.Equals(newValue) == false;
    }
}