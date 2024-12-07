namespace CommonBasicLibraries.CrudRepositoryHelpers;

// A simple class to represent a filter property
public class FilterProperty
{
    public string Name { get; set; } = "";
    public object? Value { get; set; }
    public EnumFilterOperator Operator { get; set; }
}