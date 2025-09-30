namespace CommonBasicLibraries.AdvancedGeneralFunctionsAndProcesses.CSharpCodeWriters;

public interface IWriter
{
    IWriter Write(object obj);
    IWriter AppendDoubleQuote(Action<IWriter> action);
    IWriter AppendDoubleQuote(object obj);
    IWriter AppendDoubleQuote(); //this means nothing.
    public string GetSingleText(Action<IWriter> action);
}
