namespace CommonBasicLibraries.AdvancedGeneralFunctionsAndProcesses.CSharpCodeWriters;
public interface ICodeBlock
{
    ICodeBlock WriteLine(string text);
    ICodeBlock WriteLine(Action<IWriter> action);
    ICodeBlock WriteCodeBlock(Action<ICodeBlock> action, bool endSemi = false);
    ICodeBlock WriteLambaBlock(Action<ICodeBlock> action); //this will end with semi and the )
    public string GetSingleText(Action<IWriter> action);
    ICodeBlock WriteListBlock(BasicList<string> items);
}