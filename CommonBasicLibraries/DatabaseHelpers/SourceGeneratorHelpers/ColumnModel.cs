namespace CommonBasicLibraries.DatabaseHelpers.SourceGeneratorHelpers;
public class ColumnModel
{
    public string TableName { get; set; } = ""; //may decide it wants to use this information to get the table names like for updating.
    public string ColumnName { get; set; } = "";
    public bool IsIdentity { get; set; }
    public string ObjectName { get; set; } = "";
    public object? Value { get; set; }
    public bool CommonForUpdating { get; set; }
    public string Prefix { get; set; } = "";
    public DbType ColumnType { get; set; } //this is needed.
    //most likely the source generator has to figure out if there is a match or not.
    public bool HasMatch { get; set; }
    public bool Like { get; set; } //this can be set later (not by the source generator)
}