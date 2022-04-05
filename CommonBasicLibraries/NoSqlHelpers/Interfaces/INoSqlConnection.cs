namespace CommonBasicLibraries.NoSqlHelpers.Interfaces;
public interface INoSqlConnection
{
    string ConnectionString { get; } //can get any way they want (including using IConfiguration if they want bad enough).
}