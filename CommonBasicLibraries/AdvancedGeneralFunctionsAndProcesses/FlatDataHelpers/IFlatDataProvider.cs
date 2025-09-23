namespace CommonBasicLibraries.AdvancedGeneralFunctionsAndProcesses.FlatDataHelpers;
/// <summary>
/// Provides indexed access to headers and values for flat data objects of type T.
/// Supports reading and optional writing with validation.
/// </summary>
public interface IFlatDataProvider<T>
{
    /// <summary>Number of columns/properties in the flat data.</summary>
    int ColumnCount { get; }

    /// <summary>Gets the header name at the specified index.</summary>
    string GetHeader(int index);
    /// <summary>Gets all header names as a list.</summary>
    BasicList<string> GetHeaders();
    /// <summary>Gets the string representation of the value for the specified property on the given item.</summary>
    string GetValue(T item, string propertyName);

    /// <summary>Gets the string representation of the value at the specified index for the given item.</summary>
    string GetValue(T item, int index);

    /// <summary>Attempts to set the value at the specified index on the given item.</summary>
    /// <param name="item">The item to update (by reference).</param>
    /// <param name="index">The index of the value to set.</param>
    /// <param name="value">The string value to assign.</param>
    /// <param name="errorMessage">Error message if the value is invalid; otherwise null.</param>
    /// <returns>True if the value was set successfully; otherwise false.</returns>
    bool TrySetValue(ref T item, int index, string value, out string? errorMessage);


    /// <summary>Attempts to set the value at the specified index on the given item.</summary>
    /// <param name="item">The item to update (by reference).</param>
    /// <param name="property">The property of the value to set.</param>
    /// <param name="value">The string value to assign.</param>
    /// <param name="errorMessage">Error message if the value is invalid; otherwise null.</param>
    /// <returns>True if the value was set successfully; otherwise false.</returns>
    bool TrySetValue(ref T item, string property, string value, out string? errorMessage);

}