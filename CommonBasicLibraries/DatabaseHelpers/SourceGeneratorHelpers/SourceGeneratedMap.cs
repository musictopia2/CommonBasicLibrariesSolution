namespace CommonBasicLibraries.DatabaseHelpers.SourceGeneratorHelpers;
public class SourceGeneratedMap
{
    public string TableName { get; set; } = ""; //this is needed in order to generate any sql statement.
    //hopefully not needed to figure out if autoincremented or not (?)
    //this is still needed because something else will 
    public BasicList<ColumnModel> Columns { get; set; } = [];
}