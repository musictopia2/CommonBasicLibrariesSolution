namespace CommonBasicLibraries.DatabaseHelpers.MiscClasses;
public class SortInfo : IProperty
{
    public enum EnumOrderBy
    {
        Ascending, Descending
    }
    public string Property { get; set; } = "";
    public EnumOrderBy OrderBy { get; set; } = EnumOrderBy.Ascending;
}