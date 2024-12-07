namespace CommonBasicLibraries.CrudRepositoryHelpers;
/// <summary>
/// Specify the compare operator
/// </summary>
public enum EnumFilterOperator
{
    Equals,
    NotEquals,
    //StartsWith,
    //EndsWith,
    Contains,
    LessThan,
    GreaterThan,
    LessThanOrEqual,
    GreaterThanOrEqual
    //for now, don't support startswith or endswith.  if i decide to support later, a new improvement
}